using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class GoStartTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.GoStart;

        // Game Rule Properties

        private Int32 _goSalary;
        public Int32 GoSalary => _goSalary;

        #endregion

        #region Constructors

        public GoStartTileGameModel (GoStartTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _goSalary = ruleData.goSalary;
        }

        #endregion

    }
}