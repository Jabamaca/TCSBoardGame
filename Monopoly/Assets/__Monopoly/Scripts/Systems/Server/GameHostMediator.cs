using Monopoly.Events;
using Monopoly.Gameplay.Model;
using Monopoly.Server.Data;
using UnityEngine;
using GameUtils.Globals.Observing;

namespace Monopoly.Server {

    public class GameHostMediator : MonoBehaviour {

        #region Properties

        [SerializeField]
        [Tooltip ("DON'T CHANGE THIS WHILE IN EDITOR PLAY MODE!!!")]
        private bool _isRemote = false;
        [SerializeField]
        private GameRuleHost _localGameRuleHost;
        [SerializeField]
        private GameRuleHost _remoteGameRuleHost;

        private GameRuleHost UsedHost => _isRemote ? _remoteGameRuleHost : _localGameRuleHost;
        private bool _isDelegatedForRemote = false;
        private bool _isDelegatedForLocal = false;

        #endregion

        #region Unity Internal Methods

        private void OnEnable () {
            UpdateHostDelegates ();

            AddSignalObserving ();
        }

        private void OnDisable () {
            RemoveDelegatesForRemote ();
            RemoveDelegatesForLocal ();

            RemoveSignalObserving ();
        }

        #endregion

        #region Methods

        public void SetToRemoteHosting (bool toRemote) {
            _isRemote = toRemote;
            UpdateHostDelegates ();
        }

        private void UpdateHostDelegates () {
            if (_isRemote) {
                RemoveDelegatesForLocal ();
                AddDelegatesForRemote ();
            } else {
                RemoveDelegatesForRemote ();
                AddDelegatesForLocal ();
            }
        }

        private void ProcessGameEvent (GameEvent evnt) {
            switch (evnt.EventType) {
                case GameEventType.PLAYER_JOIN:
                    GlobalObserver.NotifyObservers (evnt as PlayerJoinEvent);
                    break;
                case GameEventType.PLAYER_LEAVE:
                    GlobalObserver.NotifyObservers (evnt as PlayerLeaveEvent);
                    break;
                case GameEventType.GAME_START:
                    GlobalObserver.NotifyObservers (evnt as StartGameEvent);
                    break;
                case GameEventType.GAME_TURN_CHANGE:
                    GlobalObserver.NotifyObservers (evnt as TurnChangeGameEvent);
                    break;
                case GameEventType.PLAYER_DICEROLL:
                    GlobalObserver.NotifyObservers (evnt as PlayerDiceRollEvent);
                    break;
                case GameEventType.PLAYER_MOVE:
                    GlobalObserver.NotifyObservers (evnt as PlayerMoveEvent);
                    break;

            }
        }

        #region Delegate Setup Methods

        public void AddDelegatesForRemote () {
            if (_isDelegatedForRemote)
                return;

            _remoteGameRuleHost.OnHostGameStateFetched += OnHostGameStateFetched;
            _remoteGameRuleHost.OnEventFetched += OnEventFetched;
            _isDelegatedForRemote = true;
        }

        public void RemoveDelegatesForRemote () {
            if (!_isDelegatedForRemote)
                return;

            _remoteGameRuleHost.OnHostGameStateFetched -= OnHostGameStateFetched;
            _remoteGameRuleHost.OnEventFetched -= OnEventFetched;
            _isDelegatedForRemote = false;
        }

        public void AddDelegatesForLocal () {
            if (_isDelegatedForLocal)
                return;

            _localGameRuleHost.OnHostGameStateFetched += OnHostGameStateFetched;
            _localGameRuleHost.OnEventFetched += OnEventFetched;
            _isDelegatedForLocal = true;
        }

        public void RemoveDelegatesForLocal () {
            if (!_isDelegatedForLocal)
                return;

            _localGameRuleHost.OnHostGameStateFetched -= OnHostGameStateFetched;
            _localGameRuleHost.OnEventFetched -= OnEventFetched;
            _isDelegatedForLocal = false;
        }

        #endregion

        #region Observing Methods

        private void AddSignalObserving () {
            // Connectivity Signals
            GlobalObserver.AddObserver<RequestPlayerJoinSignal> (OnRequestPlayerJoinSignal);
            GlobalObserver.AddObserver<RequestPlayerLeaveSignal> (OnRequestPlayerLeaveSignal);

            // Game Input Signals
            GlobalObserver.AddObserver<GameInputPlayerDiceRollSignal> (OnGameInputPlayerDiceRollSignal);
        }

        private void RemoveSignalObserving () {
            // Connectivity Signals
            GlobalObserver.RemoveObserver<RequestPlayerJoinSignal> (OnRequestPlayerJoinSignal);
            GlobalObserver.RemoveObserver<RequestPlayerLeaveSignal> (OnRequestPlayerLeaveSignal);

            // Game Input Signals
            GlobalObserver.RemoveObserver<GameInputPlayerDiceRollSignal> (OnGameInputPlayerDiceRollSignal);
        }

        #endregion

        #endregion

        #region Delegates

        private void OnHostGameStateFetched (GameState gs) {
            GlobalObserver.NotifyObservers (new GameStateUpdate {
                gameState = gs
            });
        }

        private void OnEventFetched (GameEvent evnt) {
            ProcessGameEvent (evnt);
        }

        #endregion

        #region Signal Observing

        #region Connectivity Signals

        private void OnRequestPlayerJoinSignal (RequestPlayerJoinSignal sig) {
            UsedHost.RequestJoinPlayerID (sig.playerID);
        }

        private void OnRequestPlayerLeaveSignal (RequestPlayerLeaveSignal sig) {
            UsedHost.RequestLeavePlayerID (sig.playerID);
        }

        #endregion

        #region Game Input Signals

        private void OnGameInputPlayerDiceRollSignal (GameInputPlayerDiceRollSignal sig) {
            UsedHost.GameInputRollDice (sig.playerID);
        }

        #endregion

        #endregion

    }

}