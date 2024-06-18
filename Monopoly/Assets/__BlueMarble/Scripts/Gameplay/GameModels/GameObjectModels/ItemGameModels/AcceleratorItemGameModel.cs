using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class AcceleratorItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.Accelerator;

        private UInt16 _lapCount;
        public UInt16 LapCount => _lapCount;

        #endregion

        #region Constructors

        public AcceleratorItemGameModel (AcceleratorItemGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _lapCount = ruleData.lapCount;
        }

        #endregion

    }
}