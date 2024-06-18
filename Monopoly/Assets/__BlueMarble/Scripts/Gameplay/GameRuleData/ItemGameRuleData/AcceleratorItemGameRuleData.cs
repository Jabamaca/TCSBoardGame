using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class AcceleratorItemGameRuleData : ItemGameRuleData {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.Accelerator;

        public UInt16 lapCount;

        #endregion

    }
}