using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class RocketBootsStatusEffectGameModel : StatusEffectGameModel {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.RocketBoots;

        private Int16 _rollBoostCount;
        public Int16 RollBoostCount => _rollBoostCount;

        #endregion

        #region Constructors

        public RocketBootsStatusEffectGameModel (RocketBootsItemGameModel sourceItemModel) {
            SetBasicStatePropertiesWithSourceItem (sourceItemModel);

            _rollBoostCount = sourceItemModel.RollBoostCount;
        }

        public RocketBootsStatusEffectGameModel (RocketBootsStatusEffectGameStateData stateData) {
            SetBasicStatePropertiesWithData (stateData);

            _rollBoostCount = stateData.rollBoostCount;
        }

        #endregion

    }
}