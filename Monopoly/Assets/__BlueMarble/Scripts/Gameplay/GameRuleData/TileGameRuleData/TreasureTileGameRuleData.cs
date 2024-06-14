using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class TreasureTileGameRuleData : TileGameRuleData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Treasure;

        public Int32 treasureCost;
        public UInt16 treasureItemCount;

        #endregion

    }
}