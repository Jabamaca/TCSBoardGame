using GameUtils.Globals.Observing;
using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class GameSignal : Observable {

        public virtual GameSignalType SignalType => GameSignalType.NULL;

    }
}