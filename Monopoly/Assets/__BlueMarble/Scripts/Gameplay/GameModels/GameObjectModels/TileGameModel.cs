using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public abstract class TileGameModel {

        #region Properties

        public virtual TileTypeEnum TileType => TileTypeEnum.None;

        private UInt16 _tileID;
        public UInt16 TileID => _tileID;

        private string _nameKey;
        public string NameKey => _nameKey;

        private UInt16 _previousTileID;
        public UInt16 PreviousTileID => _previousTileID;
        private UInt16 _nextTileID;
        public UInt16 NextTileID => _nextTileID;

        #endregion

        #region Constructors

        protected void SetBasicRulePropertiesWithData (TileGameRuleData ruleData) {
            _tileID = ruleData.tileID;
            _nameKey = ruleData.nameKey;
            _previousTileID = ruleData.previousTileID;
            _nextTileID = ruleData.nextTileID;
        }

        #endregion

        #region Methods

        public virtual void UpdateWithStateData (TileGameStateData stateData) {
            // No common data.
        }

        #endregion

    }
}