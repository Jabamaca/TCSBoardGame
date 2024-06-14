using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public abstract class TileGameStateData {

        #region Properties

        public virtual TileTypeEnum TileType => TileTypeEnum.None;

        public UInt16 tileID;
        
        #endregion

    }
}