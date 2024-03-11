using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class PlayerLeaveEvent : GameEvent {

        public override GameEventType EventType => GameEventType.PLAYER_LEAVE;

        public int playerID;

    }
}