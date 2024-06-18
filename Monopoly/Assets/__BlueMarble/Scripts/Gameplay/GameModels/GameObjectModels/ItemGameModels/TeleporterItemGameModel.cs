using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class TeleporterItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.Teleporter;

        #endregion

        #region Constructors

        public TeleporterItemGameModel (TeleporterItemGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);
        }

        #endregion

    }
}