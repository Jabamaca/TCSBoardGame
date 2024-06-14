using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class BankTileGameRuleData : TileGameRuleData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Bank;

        public Int32 bankFee;

        #endregion

    }

}