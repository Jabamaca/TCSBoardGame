using System.Collections;
using System.Collections.Generic;
using GameUtils;
using MixedReality.Toolkit;
using Monopoly.Define;
using Monopoly.Events;
using Monopoly.Gameplay.Data;
using Monopoly.Gameplay.Model;
using UnityEngine;

namespace Monopoly.Server {
    public class ClientSideServer : GameRuleHost {

        private const long GAME_DURATION_SEC = 1200;
        private const int PLAYER_STARTING_MONEY = 5000;

        #region Properties

        [SerializeField]
        private float _serverPing = 0.05f;
        [SerializeField]
        private TextAsset _mapDataJSON;

        // Class behavioural properties.
        private Coroutine _serverSimulation;
        private bool _didMBStart = false;

        // Gameplay properties.
        private GameBoardMapData _mapData;
        public GameBoardMapData MapData => _mapData;
        private GameState _gameState;
        private bool _gameStarted = false;

        private readonly List<int> _joinedPlayerIDs = new ();
        // TODO: TO EDIT Single Player Test
        private int _singlePlayer = -1;

        #endregion

        #region Unity Internal Methods

        private void OnEnable () {
            if (!_didMBStart)
                return;

            StartServerSimulation ();
        }

        private void OnDisable () {
            StopServerSimulation ();
        }

        private void Awake () {
            _mapData = JsonUtility.FromJson<GameBoardMapData> (_mapDataJSON.text);

            // TODO: TEMP CODE Setup
            int randPLayerID = 1000;
            for (int i = 0; i < 3; i++) {
                randPLayerID += Random.Range (1, 101);
                _joinedPlayerIDs.Add (randPLayerID);
            }
        }

        private void Start () {
            StartServerSimulation ();
            _didMBStart = true;
        }

        #endregion

        #region Methods

        #region Gameplay Methods

        private void TryStartGame () {
            int playerCountForStart = 4; // TODO: TEMP VALUE
            if (_joinedPlayerIDs.Count == playerCountForStart) {
                SetupGame ();
            }
        }

        private void SetupGame () {
            int startTileID = _mapData.startingTileID;
            int startMoney = PLAYER_STARTING_MONEY;
            long startTime = GAME_DURATION_SEC;

            // Prompt start of game event with initial data.
            RandomizePlayerColorAndOrder (out List<int> playerOrder, out var colorAssign);
            StartGameEvent startGameEvent = new () {
                startingTime = startTime,
                startingMoney = startMoney,
                startingTileID = startTileID,
                playerIDOrder = playerOrder,
                assignedColors = colorAssign
            };
            OnEventFetched (startGameEvent);

            // Initialize first GameState.
            GameStateData.GenerateInitialPlayerDataDict (_joinedPlayerIDs, startTileID, out var pdDict);
            GameStateData.GenerateInitialTileStateDataDict (_mapData, out var tdDict);
            GameStateData gsd = new () {
                gameTimeLeft = startTime,
                currentTurnPlayerID = playerOrder[0],
                bankCashAmount = 0,
                playerIDTurnOrder = playerOrder,
                playerStateDataDict = pdDict,
                tileStateDataDict = tdDict
            };
            _gameState = new GameState (gsd);

            foreach (int playerID in _joinedPlayerIDs) {
                _gameState.PlayerEarnCash (playerID, startMoney);
                _gameState.PlayerMoveToTileID (playerID, startTileID);
            }

            _gameStarted = true;
        }

        private void RandomizePlayerColorAndOrder (out List<int> playerOrder, out SerializableDictionary<int, PlayerColorEnum> colorAssign) {
            playerOrder = new ();
            colorAssign = new ();

            // Randomize player order and color.
            ShufflingList<int> randPlayerOrder = new ();
            randPlayerOrder.AddItemsToList (_joinedPlayerIDs, shuffle: true);
            ShufflingList<PlayerColorEnum> randColors = new ();
            randColors.AddItemsToList (new PlayerColorEnum[] {
                PlayerColorEnum.Red,
                PlayerColorEnum.Blue,
                PlayerColorEnum.Green,
                PlayerColorEnum.Black
            }, shuffle: true);

            // Assign colors to players.
            int pCount = randPlayerOrder.ShuffledList.Count;
            int cCount = randColors.ShuffledList.Count;
            for (int i = 0; i < pCount && i < cCount; i++) {
                colorAssign.Add (randPlayerOrder.ShuffledList[i], randColors.ShuffledList[i]);
            }

            // Assign player ordering.
            playerOrder.AddRange (randPlayerOrder.ShuffledList);
        }

        private void ProceedToNextTurn () {
            _gameState.GoNextTurnPlayer ();
            OnEventFetched (new TurnChangeGameEvent {
                playerID = _gameState.CurrentTurnPlayerID
            });
        }

        private void RollDice (int amount, int playerID, out List<int> results) {
            results = new List<int> ();
            for (int i = 0; i < amount; i++) {
                results.Add (Random.Range (1, 7));
            }
            OnEventFetched (new PlayerDiceRollEvent {
                playerID = playerID,
                diceResults = results
            });
        }

        private void MovePlayer (int playerID, List<int> diceResults) {
            int spaceMoved = 0;
            foreach (int result in diceResults)
                spaceMoved += result;

            PlayerState ps = _gameState.GetPlayerState (playerID);
            int oldTileID = ps.PlacementTileID;
            int newTileID = _mapData.GetTileIDSpaceAwayFromTileID (spaceMoved, oldTileID);

            _gameState.PlayerMoveToTileID (playerID, newTileID);
            OnEventFetched (new PlayerMoveEvent {
                playerID = playerID,
                tileID = newTileID
            });
        }

        #endregion

        #region Server Simulation Methods

        private void StartServerSimulation () {
            if (_serverSimulation != null) {
                return;
            }

            _serverSimulation = StartCoroutine (SimulateServer ());
        }

        private IEnumerator SimulateServer () {
            while (this.enabled) {
                if (_gameStarted) {
                    // TODO: TO EDIT Single Player Test
                    if (_gameState.CurrentTurnPlayerID != _singlePlayer) {
                        SimulateNextAIPlayer (_gameState.CurrentTurnPlayerID);
                    }

                    OnHostGameStateFetched?.Invoke (_gameState);
                }

                yield return new WaitForSeconds (_serverPing);
            }
        }

        private void StopServerSimulation () {
            if (_serverSimulation == null) {
                return;
            }

            StopCoroutine (_serverSimulation);
            _serverSimulation = null;
        }

        // TODO: TO EDIT Single Player Test
        private void SimulateNextAIPlayer (int aiPlayer) {
            GameInputRollDice (aiPlayer);
        }

        #endregion

        #endregion

        #region Override GameRuleHost

        #region Connectivity Methods

        public override void RequestJoinPlayerID (int playerID) {
            if (_joinedPlayerIDs.Contains (playerID)) {
                return;
            }

            // TODO: TO EDIT Single Player Test
            _singlePlayer = playerID;

            _joinedPlayerIDs.Add (playerID);
            PlayerJoinEvent joinEvent = new () {
                playerID = playerID
            };
            OnEventFetched (joinEvent);

            TryStartGame ();
        }

        public override void RequestLeavePlayerID (int playerID) {
            if (!_joinedPlayerIDs.Contains (playerID)) {
                return;
            }

            _joinedPlayerIDs.Remove (playerID);
            PlayerLeaveEvent leaveEvent = new () {
                playerID = playerID
            };
            OnEventFetched (leaveEvent);
        }

        #endregion

        #region Game Input Methods

        public override void GameInputRollDice (int playerID) {
            if (_gameState.CurrentTurnPlayerID != playerID)
                return;

            int diceCount = MonopolyConst.DICE_AMOUNT;
            RollDice (diceCount, playerID, out var diceResults);
            MovePlayer (playerID, diceResults);

            ProceedToNextTurn ();
        }

        #endregion

        #endregion

    }
}