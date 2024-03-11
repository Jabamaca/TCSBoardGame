using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class RequestPlayerLeaveSignal : GameSignal {

        public override GameSignalType SignalType => GameSignalType.REQUEST_PLAYER_LEAVE;

        public int playerID;

    }
}
