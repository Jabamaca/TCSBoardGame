using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.RuleData {
    public abstract class ItemGameRuleData {

        #region Properties

        public virtual ItemTypeEnum ItemType => ItemTypeEnum.Null;

        public UInt16 itemID;
        public string nameKey;
        public string descriptionKey;

        #endregion

    }
}