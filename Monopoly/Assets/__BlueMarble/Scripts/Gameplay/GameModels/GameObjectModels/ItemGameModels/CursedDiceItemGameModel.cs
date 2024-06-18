using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class CursedDiceItemGameModel : ItemGameModel {

        #region Properties

        public override ItemTypeEnum ItemType => ItemTypeEnum.CursedDice;

        #endregion

        #region Constructors

        public CursedDiceItemGameModel (CursedDiceItemGameRuleData ruleData) {
            // No unique properties...
        }

        #endregion

    }
}