using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class VoidJailTileGameRuleData : TileGameRuleData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.VoidJail;

        public Int16 initJailTurnCount;

        #endregion

    }
}