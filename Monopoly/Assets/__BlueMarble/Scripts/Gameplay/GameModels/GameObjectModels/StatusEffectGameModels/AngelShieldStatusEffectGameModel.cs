using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class AngelShieldStatusEffectGameModel : StatusEffectGameModel {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.AngelShield;

        #endregion

        #region Constructors

        public AngelShieldStatusEffectGameModel (AngelShieldItemGameModel sourceItemModel) {
            SetBasicStatePropertiesWithSourceItem (sourceItemModel);
        }

        public AngelShieldStatusEffectGameModel (AngelShieldStatusEffectGameStateData stateData) {
            SetBasicStatePropertiesWithData (stateData);
        }

        #endregion

    }
}