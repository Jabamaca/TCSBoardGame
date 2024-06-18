using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public class ChanceTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Chance;

        #endregion

        #region Constructors

        public ChanceTileGameModel (ChanceTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);
        }

        #endregion

    }
}