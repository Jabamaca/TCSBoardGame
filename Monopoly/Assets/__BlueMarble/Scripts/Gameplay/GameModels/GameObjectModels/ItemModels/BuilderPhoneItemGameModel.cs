using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class BuilderPhoneItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.BuilderPhone;

        #endregion

        #region Constructors

        public BuilderPhoneItemGameModel (BuilderPhoneItemGameRuleData ruleData) {
            // No unique properties...
        }

        #endregion

    }
}