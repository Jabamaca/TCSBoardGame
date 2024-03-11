using Monopoly.Server.Data;
using System.Collections.Generic;

namespace Monopoly.Events {
    public class PlayerDiceRollEvent : GameEvent {

        public override GameEventType EventType => GameEventType.PLAYER_DICEROLL;

        public int playerID;
        public List<int> diceResults;

    }
}