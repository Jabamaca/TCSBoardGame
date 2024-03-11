using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class TurnChangeGameEvent : GameEvent {

        public override GameEventType EventType => GameEventType.GAME_TURN_CHANGE;

        public int playerID;

    }
}