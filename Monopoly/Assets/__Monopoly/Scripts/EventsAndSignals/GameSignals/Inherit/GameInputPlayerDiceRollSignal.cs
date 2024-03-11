using Monopoly.Server.Data;

namespace Monopoly.Events {
    public class GameInputPlayerDiceRollSignal : GameSignal {

        public override GameSignalType SignalType => GameSignalType.GAMEINPUT_PLAYER_DICEROLL;

        public int playerID;

    }
}