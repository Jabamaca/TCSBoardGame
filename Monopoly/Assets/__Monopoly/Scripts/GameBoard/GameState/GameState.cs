using Monopoly.Gameplay.Data;
using System.Collections.Generic;
using MixedReality.Toolkit;

namespace Monopoly.Gameplay.Model {
    public class GameState {

        #region Properties

        private long _gameTimeLeft;
        private int _currentTurnPlayerID;
        private int _bankCashAmount;
        private readonly List<int> _playerIDTurnOrder = new ();
        private readonly Dictionary<int, PlayerState> _playerStateDict = new ();
        private readonly Dictionary<int, TileState> _tileStateDict = new ();

        private int _currentTurnPlayerIndex = -1;

        #endregion

        #region Constructors

        public GameState (GameStateData gsd) {
            _gameTimeLeft = gsd.gameTimeLeft;
            _bankCashAmount = gsd.bankCashAmount;

            _currentTurnPlayerID = gsd.currentTurnPlayerID;
            _playerIDTurnOrder.AddRange (gsd.playerIDTurnOrder);
            UpdateCurrentTurnPlayerIndex ();
            
            foreach (var playerKVP in gsd.playerStateDataDict) {
                _playerStateDict.Add (playerKVP.Value.playerID, new PlayerState (playerKVP.Value));
            }

            foreach (var tileKVP in gsd.tileStateDataDict) {
                _tileStateDict.Add (tileKVP.Value.tileID, new TileState (tileKVP.Value));
            }
        }

        #endregion

        #region Methods

        #region Getter Methods

        public long GameTimeLeft => _gameTimeLeft;
        public int CurrentTurnPlayerID => _currentTurnPlayerID;
        public int BankCashAmount => _bankCashAmount;

        public PlayerState GetPlayerState (int playerID) {
            return _playerStateDict[playerID];
        }

        public TileState GetTileState (int tileID) {
            return _tileStateDict[tileID];
        }

        #endregion

        #region Game Time Activity Methods

        public void SetTime (long time) {
            _gameTimeLeft = time;

            if (_gameTimeLeft < 0)
                _gameTimeLeft = 0;
        }

        public void ReduceTime (long time) {
            _gameTimeLeft -= time;

            if (_gameTimeLeft < 0)
                _gameTimeLeft = 0;
        }

        public void GoNextTurnPlayer () {
            _currentTurnPlayerIndex = (_currentTurnPlayerIndex + 1) % _playerIDTurnOrder.Count;
            _currentTurnPlayerID = _playerIDTurnOrder[_currentTurnPlayerIndex];
        }

        public void SetTurnPlayerID (int playerID) {
            _currentTurnPlayerID = playerID;
            UpdateCurrentTurnPlayerIndex ();
        }

        private void UpdateCurrentTurnPlayerIndex () {
            int i = 0;
            foreach (int playerID in _playerIDTurnOrder) {
                if (playerID == _currentTurnPlayerID) {
                    _currentTurnPlayerIndex = i;
                    return;
                }
                i++;
            }

            _currentTurnPlayerIndex = -1;
        }

        #endregion

        #region Bank Activity Methods

        public void SetBankCash (int amount) {
            _bankCashAmount = amount;
        }

        public void ReduceBankCash (int amount) {
            _bankCashAmount -= amount;
        }

        public void IncreaseBankCash (int amount) {
            _bankCashAmount += amount;
        }

        #endregion

        #region Tile Activity Methods

        public void TileBuyPropertyForPlayer (int tileID, int playerID) {
            _tileStateDict[tileID].BuyPropertyForPlayer (playerID);
        }

        public void TileUpgradeProperty (int tileID) {
            _tileStateDict[tileID].UpgradeProperty ();
        }

        public void TileSellProperty (int tileID) {
            _tileStateDict[tileID].SellProperty ();
        }

        #endregion

        #region Player Activity Methods

        public void ChangeTurnPlayer (int playerID) {
            _currentTurnPlayerID = playerID;
        }

        public void PlayerSetCash (int playerID, int amount) {
            _playerStateDict[playerID].SetCash (amount);
        }

        public void PlayerEarnCash (int playerID, int amount) {
            _playerStateDict[playerID].EarnCash (amount);
        }

        public void PlayerSpendCash (int playerID, int amount) {
            _playerStateDict[playerID].SpendCash (amount);
        }

        public void PlayerGetJailed (int playerID, int jailTurns) {
            _playerStateDict[playerID].GetJailed (jailTurns);
        }

        public void PlayerReduceJailTurns (int playerID, int turns) {
            _playerStateDict[playerID].ReduceJailTurns (turns);
        }

        public void PlayerFreeFromJail (int playerID) {
            _playerStateDict[playerID].FreeFromJail ();
        }

        public void PlayerMoveToTileID (int playerID, int tileID) {
            _playerStateDict[playerID].PlaceToTileID (tileID);
        }

        #endregion

        #region Data Convertion Methods

        public GameStateData ConvertToData () {
            GameStateData returnData = new () {
                gameTimeLeft = _gameTimeLeft,
                currentTurnPlayerID = _currentTurnPlayerID,
                bankCashAmount = _bankCashAmount,
                playerIDTurnOrder = new (_playerIDTurnOrder),
                playerStateDataDict = ConvertPlayerDictToData (),
                tileStateDataDict = ConvertTileDictToData ()
            };

            return returnData;
        }

        private SerializableDictionary<int, TileStateData> ConvertTileDictToData () {
            SerializableDictionary<int, TileStateData> returnDict = new ();

            foreach (var kvp in _tileStateDict) {
                returnDict.Add (kvp.Key, kvp.Value.ConvertToData ());
            }

            return returnDict;
        }

        private SerializableDictionary<int, PlayerStateData> ConvertPlayerDictToData () {
            SerializableDictionary<int, PlayerStateData> returnDict = new ();

            foreach (var kvp in _playerStateDict) {
                returnDict.Add (kvp.Key, kvp.Value.ConvertToData ());
            }

            return returnDict;
        }

        #endregion

        #endregion

    }
}