using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.StateData {
    public class GameMatchGameStateData {

        #region Properties

        public UInt32 gameID;

        public List<PlayerGameStateData> playerStateDataList;
        public List<UInt32> playerIDTurnOrder;
        public UInt32 currentTurnPlayerID;

        public List<TileGameStateData> tileStateDataList;

        #endregion

    }
}