using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class PlayerJoinEvent : GameEvent {

        public override GameEventType EventType => GameEventType.PLAYER_JOIN;

        public int playerID;

    }
}