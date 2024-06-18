using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class BagOfWindItemGameRuleData : ItemGameRuleData {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.BagOfWind;

        public UInt16 moveRange;

        #endregion

    }
}