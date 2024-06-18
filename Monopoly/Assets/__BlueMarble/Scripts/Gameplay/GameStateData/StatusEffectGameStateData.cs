using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public abstract class StatusEffectGameStateData {

        #region Properties

        public virtual StatusEffectTypeEnum StatusEffectType => StatusEffectTypeEnum.Null;

        public UInt16 sourceItemID;

        #endregion

    }
}