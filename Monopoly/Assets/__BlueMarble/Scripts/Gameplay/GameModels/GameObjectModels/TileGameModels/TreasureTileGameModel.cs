using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class TreasureTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Treasure;

        // Game Rule Properties

        private Int32 _treasureCost;
        public Int32 TreasureCost => _treasureCost;

        private UInt16 _treasureItemCount;
        public UInt16 TreasureItemCount => _treasureItemCount;

        #endregion

        #region Constructors

        public TreasureTileGameModel (TreasureTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _treasureCost = ruleData.treasureCost;
            _treasureItemCount = ruleData.treasureItemCount;
        }

        #endregion

    }
}