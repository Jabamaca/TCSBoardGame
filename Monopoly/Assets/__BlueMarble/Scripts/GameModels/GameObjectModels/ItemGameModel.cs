using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public abstract class ItemGameModel {

        #region Properties

        public virtual ItemTypeEnum ItemType => ItemTypeEnum.Null;

        private UInt16 _itemID;
        public UInt16 ItemID => _itemID;

        private string _nameKey;
        public string NameKey => _nameKey;
        private string _descriptionKey;
        public string DescriptionKey => _descriptionKey;

        #endregion

        #region Constructors

        protected void SetBasicRulePropertiesWithData (ItemGameRuleData ruleData) {
            _itemID = ruleData.itemID;
            _nameKey = ruleData.nameKey;
            _descriptionKey = ruleData.descriptionKey;
        }

        #endregion

    }
}