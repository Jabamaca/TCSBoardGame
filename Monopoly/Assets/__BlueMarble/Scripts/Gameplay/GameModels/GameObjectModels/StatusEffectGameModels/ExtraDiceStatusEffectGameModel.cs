using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class ExtraDiceStatusEffectGameModel : StatusEffectGameModel {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.ExtraDice;

        private UInt16 _extraDiceCount;
        public UInt16 ExtraDiceCount => _extraDiceCount;

        #endregion

        #region Constructors

        public ExtraDiceStatusEffectGameModel (ExtraDiceItemGameModel sourceItemModel) {
            SetBasicStatePropertiesWithSourceItem (sourceItemModel);

            _extraDiceCount = sourceItemModel.ExtraDiceCount;
        }

        public ExtraDiceStatusEffectGameModel (ExtraDiceStatusEffectGameStateData stateData) {
            SetBasicStatePropertiesWithData (stateData);

            _extraDiceCount = stateData.extraDiceCount;
        }

        #endregion

    }
}