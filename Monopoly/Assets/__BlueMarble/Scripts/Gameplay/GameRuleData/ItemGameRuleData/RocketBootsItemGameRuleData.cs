using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class RocketBootsItemGameRuleData : ItemGameRuleData {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.RocketBoots;

        public Int16 rollBoostCount;

        #endregion

    }
}