using System;
using BlueMarble.Gameplay.Models.Defines;

namespace BlueMarble.Gameplay.RuleData {
    public abstract class TileGameRuleData {

        #region Properties

        public virtual TileTypeEnum TileType => TileTypeEnum.None;

        public UInt16 tileID;

        public string nameKey;
        public UInt16 previousTileID;
        public UInt16 nextTileID;

        #endregion

    }
}