using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class DevilPhoneItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.DevilPhone;

        #endregion

        #region Constructors

        public DevilPhoneItemGameModel (DevilPhoneItemGameRuleData ruleData) {
            // No unique properties...
        }

        #endregion

    }
}