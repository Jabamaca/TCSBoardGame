using System;
using BlueMarble.Gameplay.Models.Defines;

namespace BlueMarble.Gameplay.RuleData {
    public class PropertyTileGameRuleData : TileGameRuleData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Property;

        public PropertyColorEnum propertyColor;

        public Int32 hotelBuildCost;
        public Int32 landmarkBuildCost;
        public Int32 hotelTollFee;
        public Int32 landmarkTollFee;

        #endregion

    }
}