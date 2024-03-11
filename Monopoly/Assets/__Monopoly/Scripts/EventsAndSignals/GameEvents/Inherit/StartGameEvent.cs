using System.Collections.Generic;
using Monopoly.Define;
using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class StartGameEvent : GameEvent {

        public override GameEventType EventType => GameEventType.GAME_START;

        public long startingTime;
        public int startingMoney;
        public Dictionary<int, PlayerColorEnum> assignedColors;
        public List<int> playerIDOrder;
        public int startingTileID;

    }
}