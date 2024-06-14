using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class GoStartTileGameRuleData : TileGameRuleData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.GoStart;

        public Int32 goSalary;

        #endregion

    }
}