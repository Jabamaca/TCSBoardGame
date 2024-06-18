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

        private UInt16 _chanceInstance;
        public UInt16 ChanceInstance => _chanceInstance;

        #endregion

        #region Constructors

        protected void SetBasicRulePropertiesWithData (ItemGameRuleData ruleData) {
            _itemID = ruleData.itemID;
            _nameKey = ruleData.nameKey;
            _descriptionKey = ruleData.descriptionKey;
            _chanceInstance = ruleData.chanceInstance;
        }

        #endregion

    }
}