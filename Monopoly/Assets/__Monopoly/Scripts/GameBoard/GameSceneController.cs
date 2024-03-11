using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GameUtils.Globals.Observing;
using Monopoly.Server.Data;
using Monopoly.Gameplay.Data;
using Monopoly.Gameplay.Model;
using Monopoly.Gameplay;
using Monopoly.UI;
using Monopoly.Events;
using Monopoly.Define;

namespace Monopoly {
    public class GameSceneController : MonoBehaviour {

        #region Properties

        [SerializeField]
        private GameBoardController _gameBoardController;
        [SerializeField]
        private GameplayUIController _gameplayUIController;

        [SerializeField]
        private int _singlePlayerID; // TODO: TO EDIT TEMP CODE for connecting Player ID.

        // In-game model data properties.
        private GameState _gameState;
        private GameBoardMapData _gameBoardMapData;

        // Animation sequence properties.
        public bool IsSequenceAnimating => _animSequenceCoroutine != null;
        private readonly List<GameEvent> _animatingEventQueue = new ();
        private Coroutine _animSequenceCoroutine;

        #endregion

        #region Unity Internal Methods

        private void OnEnable () {
            // Register UI delegates.
            _gameplayUIController.OnRollDiceButtonPress += OnUIRollDicePressed;

            AddEventObserving ();
        }

        private void OnDisable () {
            // Unregister UI delegates
            _gameplayUIController.OnRollDiceButtonPress -= OnUIRollDicePressed;

            InterruptAnimationSequence ();
            RemoveEventObserving ();
        }

        private void Start () {
            // TODO: TO EDIT TEMP SETUP
            GlobalObserver.NotifyObservers (new RequestPlayerJoinSignal {
                playerID = _singlePlayerID
            });
        }

        #endregion

        #region Methods

        public GameStateData GetCurrentClientGameStateData () {
            return _gameState.ConvertToData ();
        }

        private void UpdateWithNewGameStateData (GameStateData gsd) {
            _gameState = new GameState (gsd);
            _gameBoardController.UpdateWithGameState (_gameState);
            _gameplayUIController.UpdateWithGameState (_gameState);
            // TODO: Temporary Setup
            _gameplayUIController.SetPlayerUIAsYou (_singlePlayerID); // Refresh YOU in player panels.
        }

        #region Animating Sequence Methods

        private void AddEventToSequence (GameEvent evnt) {
            _animatingEventQueue.Add (evnt);

            AnimateSequence ();
        }

        private void AnimateSequence () {
            if (_animSequenceCoroutine != null)
                return;

            _animSequenceCoroutine = StartCoroutine (AnimateSequenceImpl ());
        }

        private IEnumerator AnimateSequenceImpl () {
            while (_animatingEventQueue.Count > 0) {
                yield return ProcessAnimatingEvent (_animatingEventQueue[0]);

                _animatingEventQueue.RemoveAt (0);
            }

            _animSequenceCoroutine = null;
        }

        private IEnumerator ProcessAnimatingEvent (GameEvent evnt) {
            switch (evnt.EventType) {
                case GameEventType.GAME_START:
                    yield return AnimateStartGameEvent (evnt as StartGameEvent);
                    break;
                case GameEventType.GAME_TURN_CHANGE:
                    yield return AnimateTurnChangeGameEvent (evnt as TurnChangeGameEvent);
                    break;
                case GameEventType.PLAYER_DICEROLL:
                    yield return AnimatePlayerDiceRollGameEvent (evnt as PlayerDiceRollEvent);
                    break;
                case GameEventType.PLAYER_MOVE:
                    yield return AnimatePlayerMoveGameEvent (evnt as PlayerMoveEvent);
                    break;
                default:
                    yield break;
            }
        }

        private void InterruptAnimationSequence () {
            if (_animSequenceCoroutine == null)
                return;

            StopCoroutine (_animSequenceCoroutine);
            _animSequenceCoroutine = null;
        }

        #region Event Animation Methods

        private IEnumerator AnimateStartGameEvent (StartGameEvent evnt) {
            yield return _gameplayUIController.AnimateGameStartMessage ();

            _gameplayUIController.ShowPlayerUIs ();
            foreach (int playerID in evnt.playerIDOrder) {
                _gameBoardController.MovePlayerIDToTileID (playerID, evnt.startingTileID, animated: true);
                _gameplayUIController.SetPlayerUICash (playerID, evnt.startingMoney, animated: true);
            }
            yield return new WaitForSeconds (MonopolyConst.SINGLE_SPACE_MOVE_ANIM_TIME);

            yield return AnimatePlayerTurnTransition (evnt.playerIDOrder[0]);
        }

        private IEnumerator AnimateTurnChangeGameEvent (TurnChangeGameEvent evnt) {
            yield return AnimatePlayerTurnTransition (evnt.playerID);
        }

        private IEnumerator AnimatePlayerTurnTransition (int playerID) {
            _gameplayUIController.HideTurnUI ();

            _gameplayUIController.SetPlayerUITurning (playerID, true);

            if (playerID == _singlePlayerID) {
                yield return _gameplayUIController.AnimateYourTurnMessage ();
                _gameplayUIController.ShowSelfTurnUI ();
            } else {
                yield return _gameplayUIController.AnimatePlayerIDTurnMessage (playerID);
                _gameplayUIController.ShowOtherTurnUI (playerID);
            }
        }

        private IEnumerator AnimatePlayerDiceRollGameEvent (PlayerDiceRollEvent evnt) {
            if (evnt.playerID == _singlePlayerID)
                _gameplayUIController.HideSelfTurnUIInput ();

            yield return _gameBoardController.AnimateDiceRoll (evnt.diceResults);
        }

        private IEnumerator AnimatePlayerMoveGameEvent (PlayerMoveEvent evnt) {
            yield return _gameBoardController.AnimateMovePlayerIDToTileID (evnt.playerID, evnt.tileID, sequential: true);
        }

        #endregion

        #endregion

        #region Observing Methods

        private void AddEventObserving () {
            // Game State Update
            GlobalObserver.AddObserver<GameStateUpdate> (OnGameStateUpdate);

            // Game Events
            GlobalObserver.AddObserver<PlayerJoinEvent> (OnEventPlayerJoin);
            GlobalObserver.AddObserver<PlayerLeaveEvent> (OnEventPlayerLeave);
            GlobalObserver.AddObserver<StartGameEvent> (OnEventGameStart);
            GlobalObserver.AddObserver<TurnChangeGameEvent> (OnEventGameTurnChange);
            GlobalObserver.AddObserver<PlayerDiceRollEvent> (OnEventPlayerDiceRoll);
            GlobalObserver.AddObserver<PlayerMoveEvent> (OnEventPlayerMove);
        }

        private void RemoveEventObserving () {
            // Game State Update
            GlobalObserver.RemoveObserver<GameStateUpdate> (OnGameStateUpdate);

            // Game Events
            GlobalObserver.RemoveObserver<PlayerJoinEvent> (OnEventPlayerJoin);
            GlobalObserver.RemoveObserver<PlayerLeaveEvent> (OnEventPlayerLeave);
            GlobalObserver.RemoveObserver<StartGameEvent> (OnEventGameStart);
            GlobalObserver.RemoveObserver<TurnChangeGameEvent> (OnEventGameTurnChange);
            GlobalObserver.RemoveObserver<PlayerDiceRollEvent> (OnEventPlayerDiceRoll);
            GlobalObserver.RemoveObserver<PlayerMoveEvent> (OnEventPlayerMove);
        }

        #endregion

        #endregion

        #region Delegates

        private void OnUIRollDicePressed () {
            GlobalObserver.NotifyObservers (new GameInputPlayerDiceRollSignal {
                playerID = _singlePlayerID
            });
        }

        #endregion

        #region Event Observing

        // Game State Update
        private void OnGameStateUpdate (GameStateUpdate updt) {
            if (IsSequenceAnimating) {
                return;
            }

            GameStateData thisGSD = _gameState.ConvertToData ();
            GameStateData fetchedGSD = updt.gameState.ConvertToData ();
            if (!thisGSD.Equals (fetchedGSD)) {
#if UNITY_EDITOR
                Debug.Log ("SERVER ERROR: FOUND DISCREPANCY IN GAMESTATE FROM SERVER");
#endif
                UpdateWithNewGameStateData (fetchedGSD);
            }
        }

        private void OnEventGameStart (StartGameEvent evnt) {
            // Assign PlayerIDs to objects of associated colors.
            int playerCount = evnt.playerIDOrder.Count;
            for (int i = 0; i < playerCount; i++) {
                int playerID = evnt.playerIDOrder[i];
                PlayerColorEnum color = evnt.assignedColors[playerID];
                _gameBoardController.AssignPlayerIDToColor (playerID, color);
                _gameplayUIController.AssignPlayerIDAndColorToUIIndex (playerID, color, i);
            }
            // TODO: Temporary Setup
            _gameplayUIController.SetPlayerUIAsYou (_singlePlayerID);

            // Initialize first GameState.
            _gameBoardMapData = _gameBoardController.GenerateBoardMapData ();
            GameStateData.GenerateInitialPlayerDataDict (evnt.playerIDOrder, evnt.startingTileID, out var pdDict);
            GameStateData.GenerateInitialTileStateDataDict (_gameBoardMapData, out var tdDict);
            GameStateData gsd = new () {
                gameTimeLeft = evnt.startingTime,
                currentTurnPlayerID = evnt.playerIDOrder[0],
                bankCashAmount = 0,
                playerIDTurnOrder = evnt.playerIDOrder,
                playerStateDataDict = pdDict,
                tileStateDataDict = tdDict
            };
            _gameState = new GameState (gsd);

            // Initial Game State Modification
            foreach (int playerID in evnt.playerIDOrder) {
                _gameState.PlayerEarnCash (playerID, evnt.startingMoney);
                _gameState.PlayerMoveToTileID (playerID, evnt.startingTileID);
            }

            AddEventToSequence (evnt);
        }

        private void OnEventGameTurnChange (TurnChangeGameEvent evnt) {
            // Modify Game State
            _gameState.ChangeTurnPlayer (evnt.playerID);

            AddEventToSequence (evnt);
        }

        private void OnEventPlayerDiceRoll (PlayerDiceRollEvent evnt) {
            AddEventToSequence (evnt);
        }

        private void OnEventPlayerMove (PlayerMoveEvent evnt) {
            // Modify Game State
            _gameState.PlayerMoveToTileID (evnt.playerID, evnt.tileID);

            AddEventToSequence (evnt);
        }

        private void OnEventPlayerJoin (PlayerJoinEvent evnt) {
#if UNITY_EDITOR
            Debug.Log ("PLAYER JOINED: PlayerID = " + evnt.playerID);
#endif
        }

        private void OnEventPlayerLeave (PlayerLeaveEvent evnt) {
#if UNITY_EDITOR
            Debug.Log ("PLAYER LEFT: PlayerID = " + evnt.playerID);
#endif
        }

        #endregion

    }
}