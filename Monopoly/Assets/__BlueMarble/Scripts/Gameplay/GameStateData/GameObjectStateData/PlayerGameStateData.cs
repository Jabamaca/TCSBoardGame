using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.StateData {
    public class PlayerGameStateData {

        #region Properties

        public UInt32 playerID;
        public Int32 cashAmount;
        public UInt16 currentPositionTileID;
        public List<UInt16> ownedItemIDList;
        public List<StatusEffectGameStateData> statusEffectGameStateDataList;
        public Int16 jailTurns;
        public Int16 doubleRollCount;

        #endregion

    }
}