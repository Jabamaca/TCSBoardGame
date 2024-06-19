using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class PropertyTileGameModel : TileGameModel {

        #region Properties

        // Game Rule Properties

        public override TileTypeEnum TileType => TileTypeEnum.Property;

        private PropertyColorEnum _propertyColor;
        public PropertyColorEnum PropertyColor => _propertyColor;
        private Int32 _hotelBuildCost;
        public Int32 HotelBuildCost => _hotelBuildCost;
        private Int32 _landmarkBuildCost;
        public Int32 LandmarkBuildCost => _landmarkBuildCost;
        private Int32 _hotelTollFee;
        public Int32 HotelTollFee => _hotelTollFee;
        private Int32 _landmarkTollFee;
        public Int32 LandmarkTollFee => _landmarkTollFee;

        // Game State Property

        private UInt32 _ownerPlayerID;
        public UInt32 OwnerPlayerID => _ownerPlayerID;
        private PropertyUpgradeLevelEnum _propertyUpgradeLevel;
        public PropertyUpgradeLevelEnum PropertyUpgradeLevel => _propertyUpgradeLevel;
        public bool IsOwned => OwnerPlayerID != 0 && PropertyUpgradeLevel != 0;

        #endregion

        #region Constructors

        public PropertyTileGameModel (PropertyTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _propertyColor = ruleData.propertyColor;
            _hotelBuildCost = ruleData.hotelBuildCost;
            _landmarkBuildCost = ruleData.landmarkBuildCost;
            _hotelTollFee = ruleData.hotelTollFee;
            _landmarkTollFee = ruleData.landmarkTollFee;

            _ownerPlayerID = 0;
            _propertyUpgradeLevel = PropertyUpgradeLevelEnum.None;
        }

        #endregion

        #region Overrides

        public override void UpdateWithStateData (TileGameStateData stateData) {
            base.UpdateWithStateData (stateData);

            var propertyTileStateData = stateData as PropertyTileGameStateData;
            _ownerPlayerID = propertyTileStateData.ownerPlayerID;
            _propertyUpgradeLevel = propertyTileStateData.propertyUpgradeLevel;
        }

        #endregion

        #region Methods

        public void SetOwnerPlayerID (UInt32 playerID, out bool didOwnerChange) {
            didOwnerChange = _ownerPlayerID != 0;

            _ownerPlayerID = playerID;
        }

        public void UpgradeProperty () {
            if (PropertyUpgradeLevel >= PropertyUpgradeLevelEnum.Landmark)
                return;

            _propertyUpgradeLevel++;
        }

        #endregion

    }
}