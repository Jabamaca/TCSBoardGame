using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public class RocketBootsStatusEffectGameStateData : StatusEffectGameStateData {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.RocketBoots;

        public Int16 rollBoostCount;

        #endregion

    }
}