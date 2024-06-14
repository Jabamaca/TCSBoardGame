using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public class PropertyTileGameStateData : TileGameStateData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Property;

        public UInt32 ownerPlayerID;
        public PropertyUpgradeLevelEnum propertyUpgradeLevel;

        #endregion

    }
}