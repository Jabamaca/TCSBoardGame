using Monopoly.Gameplay.Data;

namespace Monopoly.Gameplay.Model {

    public class TileState {

        #region Properties

        private readonly int _tileID;
        private int _ownerPlayerID;
        private int _propertyLevel;

        public int TileID => _tileID;
        public int OwnerPlayerID => _ownerPlayerID;
        public int PropertyLevel => _propertyLevel;

        #endregion

        #region Constructors

        public TileState (TileStateData tsd) {
            _tileID = tsd.tileID;
            _ownerPlayerID = tsd.ownerPlayerID;
            _propertyLevel = tsd.propertyLevel;
        }

        #endregion

        #region Methods

        public void BuyPropertyForPlayer (int playerID) {
            _ownerPlayerID = playerID;
            _propertyLevel = 1;
        }

        public void UpgradeProperty () {
            _propertyLevel++;
        }

        public void SellProperty () {
            _ownerPlayerID = 0;
            _propertyLevel = 0;
        }

        public TileStateData ConvertToData () {

            TileStateData returnData = new () {
                tileID = _tileID,
                ownerPlayerID = _ownerPlayerID,
                propertyLevel = _propertyLevel
            };

            return returnData;
        }

        #endregion

    }

}