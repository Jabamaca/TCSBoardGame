using Monopoly.Gameplay.Data;

namespace Monopoly.Gameplay.Model {

    public class PlayerState {

        #region Properties

        private readonly int _playerID;
        private int _placementTileID;
        private int _cashAmount;
        private int _jailTurns;

        public int PlayerID => _playerID;
        public int PlacementTileID => _placementTileID;
        public int CashAmount => _cashAmount;
        public int JailTurns => _jailTurns;

        #endregion

        #region Constructors

        public PlayerState (PlayerStateData psd) {
            _playerID = psd.playerID;
            _placementTileID = psd.placementTileID;
            _cashAmount = psd.cashAmount;
            _jailTurns = psd.jailTurns;
        }

        #endregion

        #region Methods

        #region Cash Activity Methods

        public void SetCash (int amount) {
            _cashAmount = amount;
        }

        public void EarnCash (int amount) {
            _cashAmount += amount;
        }

        public void SpendCash (int amount) {
            _cashAmount -= amount;
        }

        #endregion

        #region Jail Activity Methods

        public void GetJailed (int jailTurns) {
            _jailTurns = jailTurns;

            if (_jailTurns < 0)
                _jailTurns = 0;
        }

        public void ReduceJailTurns (int turns) {
            _jailTurns -= turns;

            if (_jailTurns < 0)
                _jailTurns = 0;
        }

        public void FreeFromJail () {
            _jailTurns = 0;
        }

        #endregion

        public void PlaceToTileID (int tileID) {
            _placementTileID = tileID;
        }

        public PlayerStateData ConvertToData () {
            PlayerStateData returnData = new () {
                playerID = _playerID,
                placementTileID = _placementTileID,
                cashAmount = _cashAmount,
                jailTurns = _jailTurns
            };

            return returnData;
        }

        #endregion

    }

}