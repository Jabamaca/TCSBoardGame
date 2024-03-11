using Monopoly.Server.Data;
using GameUtils.Globals.Observing;

namespace Monopoly.Events {
    public abstract class GameEvent : Observable {

        public virtual GameEventType EventType => GameEventType.NULL;

    }
}