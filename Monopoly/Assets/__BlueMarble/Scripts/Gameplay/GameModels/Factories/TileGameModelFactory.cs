using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public static class TileGameModelFactory {

        #region Properties

        #endregion

        #region Methods

        public static TileGameModel MakeTileWithRuleData (TileGameRuleData ruleData) {
            return ruleData.TileType switch {
                TileTypeEnum.GoStart => new GoStartTileGameModel (ruleData as GoStartTileGameRuleData),
                TileTypeEnum.Property => new PropertyTileGameModel (ruleData as PropertyTileGameRuleData),
                TileTypeEnum.Chance => new ChanceTileGameModel (ruleData as ChanceTileGameRuleData),
                TileTypeEnum.VoidJail => new VoidJailTileGameModel (ruleData as VoidJailTileGameRuleData),
                TileTypeEnum.Bank => new BankTileGameModel (ruleData as BankTileGameRuleData),
                TileTypeEnum.Treasure => new TreasureTileGameModel (ruleData as TreasureTileGameRuleData),
                _ => null,
            };
        }

        #endregion

    }
}