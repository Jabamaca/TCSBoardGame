using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class PlayerMoveEvent : GameEvent {

        public override GameEventType EventType => GameEventType.PLAYER_MOVE;

        public int playerID;
        public int tileID;

    }
}