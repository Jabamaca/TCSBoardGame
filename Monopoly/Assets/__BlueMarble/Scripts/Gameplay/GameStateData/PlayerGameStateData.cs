using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.StateData {
    public class PlayerGameStateData {

        #region Properties

        public UInt32 playerID;
        public Int32 cashAmount;
        public UInt16 currentPositionTileID;
        public List<UInt16> ownedItemIDs;
        public UInt16 statusEffectID;
        public Int16 jailTurns;
        public Int16 doubleRollCount;

        #endregion

    }
}