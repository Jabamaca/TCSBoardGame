using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class RocketBootsItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.RocketBoots;

        private Int16 _rollBoostCount;
        public Int16 RollBoostCount => _rollBoostCount;

        #endregion

        #region Constructors

        public RocketBootsItemGameModel (RocketBootsItemGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _rollBoostCount = ruleData.rollBoostCount;
        }

        #endregion

    }
}