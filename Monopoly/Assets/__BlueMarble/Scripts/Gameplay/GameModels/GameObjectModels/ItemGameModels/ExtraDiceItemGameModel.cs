using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class ExtraDiceItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.ExtraDice;

        private UInt16 _extraDiceCount;
        public UInt16 ExtraDiceCount => _extraDiceCount;

        #endregion

        #region Constructors

        public ExtraDiceItemGameModel (ExtraDiceItemGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _extraDiceCount = ruleData.extraDiceCount;
        }

        #endregion

    }
}