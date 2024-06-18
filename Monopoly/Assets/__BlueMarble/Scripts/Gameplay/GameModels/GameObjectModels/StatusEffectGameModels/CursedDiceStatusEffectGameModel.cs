using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class CursedDiceStatusEffectGameModel : StatusEffectGameModel {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.CursedDice;

        #endregion

        #region Constructors

        public CursedDiceStatusEffectGameModel (CursedDiceItemGameModel sourceItemModel) {
            SetBasicStatePropertiesWithSourceItem (sourceItemModel);
        }

        public CursedDiceStatusEffectGameModel (CursedDiceStatusEffectGameStateData stateData) {
            SetBasicStatePropertiesWithData (stateData);
        }

        #endregion

    }
}