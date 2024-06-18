using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public class ExtraDiceStatusEffectGameStateData : StatusEffectGameStateData {

        #region Properties

        public override StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.ExtraDice;

        public UInt16 extraDiceCount;

        #endregion

    }
}