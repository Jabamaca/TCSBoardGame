using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class RequestPlayerJoinSignal : GameSignal {

        public override GameSignalType SignalType => GameSignalType.REQUEST_PLAYER_JOIN;

        public int playerID;

    }
}