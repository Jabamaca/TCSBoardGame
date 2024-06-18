using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class VoidJailTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.VoidJail;

        // Game Rule Properties

        private Int16 _initJailTurnCount;
        public Int16 InitJailTurnCount => _initJailTurnCount;

        #endregion

        #region Constructors

        public VoidJailTileGameModel (VoidJailTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _initJailTurnCount = ruleData.initJailTurnCount;
        }

        #endregion

    }
}