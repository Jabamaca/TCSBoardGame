using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.StateData;

namespace BlueMarble.Gameplay.Models {
    public static class StatusEffectGameModelFactory {

        #region Properties

        #endregion

        #region Methods

        public static StatusEffectGameModel MakeStatusEffectWithItem (ItemGameModel item) {
            return item.ItemType switch {
                ItemTypeEnum.RocketBoots => new RocketBootsStatusEffectGameModel (item as RocketBootsItemGameModel),
                ItemTypeEnum.ExtraDice => new ExtraDiceStatusEffectGameModel (item as ExtraDiceItemGameModel),
                ItemTypeEnum.CursedDice => new CursedDiceStatusEffectGameModel (item as CursedDiceItemGameModel),
                ItemTypeEnum.AngelShield => new AngelShieldStatusEffectGameModel (item as AngelShieldItemGameModel),
                _ => null,
            };
        }

        public static StatusEffectGameModel MakeStatusEffectWithStateData (StatusEffectGameStateData stateData) {
            return stateData.StatusEffectType switch {
                StatusEffectTypeEnum.RocketBoots => new RocketBootsStatusEffectGameModel (stateData as RocketBootsStatusEffectGameStateData),
                StatusEffectTypeEnum.ExtraDice => new ExtraDiceStatusEffectGameModel (stateData as ExtraDiceStatusEffectGameStateData),
                StatusEffectTypeEnum.CursedDice => new CursedDiceStatusEffectGameModel (stateData as CursedDiceStatusEffectGameStateData),
                StatusEffectTypeEnum.AngelShield => new AngelShieldStatusEffectGameModel (stateData as AngelShieldStatusEffectGameStateData),
                _ => null
            };
        }

        #endregion

    }
}
