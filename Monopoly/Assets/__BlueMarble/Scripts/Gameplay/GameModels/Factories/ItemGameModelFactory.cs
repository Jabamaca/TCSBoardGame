using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;

namespace BlueMarble.Gameplay.Models {
    public static class ItemGameModelFactory {

        #region Properties

        #endregion

        #region Methods

        public static ItemGameModel MakeItemWithRuleData (ItemGameRuleData ruleData) {
            return ruleData.ItemType switch {
                ItemTypeEnum.RocketBoots => new RocketBootsItemGameModel (ruleData as RocketBootsItemGameRuleData),
                ItemTypeEnum.ExtraDice => new ExtraDiceItemGameModel (ruleData as ExtraDiceItemGameRuleData),
                ItemTypeEnum.CursedDice => new CursedDiceItemGameModel (ruleData as CursedDiceItemGameRuleData),
                ItemTypeEnum.Accelerator => new AcceleratorItemGameModel (ruleData as AcceleratorItemGameRuleData),
                ItemTypeEnum.AngelShield => new AngelShieldItemGameModel (ruleData as AngelShieldItemGameRuleData),
                ItemTypeEnum.DevilPhone => new DevilPhoneItemGameModel (ruleData as DevilPhoneItemGameRuleData),
                ItemTypeEnum.BuilderPhone => new BuilderPhoneItemGameModel (ruleData as BuilderPhoneItemGameRuleData),
                ItemTypeEnum.BagOfWind => new BagOfWindItemGameModel (ruleData as BagOfWindItemGameRuleData),
                _ => null,
            };
        }

        #endregion

    }
}