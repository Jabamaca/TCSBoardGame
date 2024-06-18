using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class BagOfWindItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.BagOfWind;

        private UInt16 _moveRange;
        public UInt16 MoveRange => _moveRange;

        #endregion

        #region Constructors

        public BagOfWindItemGameModel (BagOfWindItemGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _moveRange = ruleData.moveRange;
        }

        #endregion

    }
}