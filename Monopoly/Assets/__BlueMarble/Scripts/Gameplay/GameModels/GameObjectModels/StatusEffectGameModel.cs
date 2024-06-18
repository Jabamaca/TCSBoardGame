using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public abstract class StatusEffectGameModel {

        #region Properties

        public virtual StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.Null;

        protected UInt16 _sourceItemID;
        public UInt16 SourceItemID => _sourceItemID;

        #endregion

        #region Constructors

        protected void SetBasicStatePropertiesWithSourceItem (ItemGameModel sourceItemModel) {
            _sourceItemID = sourceItemModel.ItemID;
        }

        protected void SetBasicStatePropertiesWithData (StatusEffectGameStateData stateData) {
            _sourceItemID = stateData.sourceItemID;
        }

        #endregion

    }
}