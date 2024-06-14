using BlueMarble.Gameplay.StateData;
using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.Models {

    public class PlayerGameModel {

        #region Properties

        private UInt32 _playerID;
        public UInt32 PlayerID => _playerID;

        private Int32 _cashAmount;
        public Int32 CashAmount => _cashAmount;

        private UInt16 _currentPositionTileID;
        public UInt16 CurrentPositionTileID => _currentPositionTileID;

        private readonly List<UInt16> _ownedItemIDs = new ();
        public IReadOnlyList<UInt16> OwnedItemIDs => _ownedItemIDs;

        private UInt16 _statusEffectID;
        public UInt16 StatusEffectID => _statusEffectID;

        private Int16 _jailTurns;
        public Int16 JailTurns => _jailTurns;

        #endregion

        #region Constructors

        public PlayerGameModel (UInt32 playerID, Int32 cashAmount, UInt16 initialPositionTileID) {
            _playerID = playerID;
            _cashAmount = cashAmount;
            _currentPositionTileID = initialPositionTileID;
            _statusEffectID = 0;
            _jailTurns = 0;
        }

        public PlayerGameModel (PlayerGameStateData stateData) {
            SetStatePropertiesWithData (stateData);
        }

        private void SetStatePropertiesWithData (PlayerGameStateData stateData) {
            _playerID = stateData.playerID;
            _cashAmount = stateData.cashAmount;
            _currentPositionTileID = stateData.currentPositionTileID;
            _ownedItemIDs.AddRange (stateData.ownedItemIDs);
            _statusEffectID = stateData.statusEffectID;
            _jailTurns = stateData.jailTurns;
        }

        #endregion

        #region Methods

        public void GainCash (Int32 cashAmount) {
            _cashAmount += cashAmount;
        }

        public void LoseCash (Int32 cashAmount) {
            _cashAmount -= cashAmount;
        }

        public void MovePositionToTileID (UInt16 newPosition) {
            _currentPositionTileID = newPosition;
        }

        public void AcquireItemID (UInt16 itemID) {
            _ownedItemIDs.Add (itemID);
        }

        public bool UseItemID (UInt16 itemID) {
            if (!_ownedItemIDs.Contains (itemID)) {
                return false;
            }

            _ownedItemIDs.Remove (itemID);
            return true;
        }

        public bool ApplyStatusEffectID (UInt16 statusEffectID) {
            if (_statusEffectID == statusEffectID) {
                return false;
            }

            _statusEffectID = statusEffectID;
            return true;
        }

        public bool RemoveStatusEffectID (UInt16 statusEffectID) {
            if (_statusEffectID != statusEffectID) {
                return false;
            }

            _statusEffectID = 0;
            return true;
        }

        public void GoToJail (Int16 jailTurns) {
            _jailTurns = jailTurns;
        }

        public void JailTurnDecrease () {
            _jailTurns--;
        }

        public void LeaveJail () {
            _jailTurns = 0;
        }

        public void UpdateWithStateData (PlayerGameStateData stateData) {
            SetStatePropertiesWithData (stateData);
        }

        #endregion

    }

}