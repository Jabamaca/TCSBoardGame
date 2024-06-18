using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public class ExtraDiceItemGameRuleData : ItemGameRuleData {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.ExtraDice;

        public UInt16 extraDiceCount;

        #endregion

    }
}