using Monopoly.Events;
using Monopoly.Gameplay.Model;
using UnityEngine;

namespace Monopoly.Server {

    public abstract class GameRuleHost : MonoBehaviour {

        public delegate void OnEventFetchedHandler (GameEvent ge);
        public delegate void OnHostGameStateFetchedHandler (GameState gs);

        public OnEventFetchedHandler OnEventFetched = delegate { };
        public OnHostGameStateFetchedHandler OnHostGameStateFetched = delegate { };

        #region Client Input Methods

        // Connectivity Methods
        public abstract void RequestJoinPlayerID (int playerID);
        public abstract void RequestLeavePlayerID (int playerID);

        // Game Input Methods
        public abstract void GameInputRollDice (int playerID);

        #endregion

        #region Server Fetched Methods

        #endregion

    }
}