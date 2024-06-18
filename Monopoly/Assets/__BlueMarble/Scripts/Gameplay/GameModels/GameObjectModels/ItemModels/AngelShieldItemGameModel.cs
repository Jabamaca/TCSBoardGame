using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class AngelShieldItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.AngelShield;

        #endregion

        #region Constructors

        public AngelShieldItemGameModel (AngelShieldItemGameRuleData ruleData) {
            // No unique properties...
        }

        #endregion

    }
}