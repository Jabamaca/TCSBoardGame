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

        private readonly List<StatusEffectGameModel> _statusEffects = new ();
        public IReadOnlyList<StatusEffectGameModel> StatusEffects => _statusEffects;

        private Int16 _jailTurns;
        public Int16 JailTurns => _jailTurns;

        private Int16 _doubleRollCount;
        public Int16 DoubleRollCount => _doubleRollCount;

        #endregion

        #region Constructors

        public PlayerGameModel (UInt32 playerID, Int32 cashAmount, UInt16 initialPositionTileID) {
            _playerID = playerID;
            _cashAmount = cashAmount;
            _currentPositionTileID = initialPositionTileID;
            _jailTurns = 0;
            _doubleRollCount = 0;
        }

        public PlayerGameModel (PlayerGameStateData stateData) {
            SetStatePropertiesWithData (stateData);
        }

        private void SetStatePropertiesWithData (PlayerGameStateData stateData) {
            _playerID = stateData.playerID;
            _cashAmount = stateData.cashAmount;
            _currentPositionTileID = stateData.currentPositionTileID;
            _ownedItemIDs.AddRange (stateData.ownedItemIDList);
            SetStatusEffectsFromStateDataList (stateData.statusEffectGameStateDataList);
            _jailTurns = stateData.jailTurns;
            _doubleRollCount = stateData.doubleRollCount;
        }

        private void SetStatusEffectsFromStateDataList (IEnumerable<StatusEffectGameStateData> stateDataList) {
            foreach (var stateData in stateDataList) {
                _statusEffects.Add (StatusEffectGameModelFactory.MakeStatusEffectWithStateData (stateData));
            }
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

        public bool ApplyStatusEffect (StatusEffectGameModel statusEffect) {
            foreach (var se in _statusEffects) {
                if (se.SourceItemID == statusEffect.SourceItemID) {
                    // Item effect already applied.
                    return false;
                }
            }

            _statusEffects.Add (statusEffect);
            return true;
        }

        public bool RemoveStatusEffectOfItemID (UInt16 sourceItemID) {
            StatusEffectGameModel statusEffectToRemove = null;
            foreach (var se in _statusEffects) {
                if (sourceItemID == se.SourceItemID) {
                    statusEffectToRemove = se;
                    break;
                }
            }

            if (statusEffectToRemove != null) {
                _statusEffects.Remove (statusEffectToRemove);
                return true;
            }

            return false;
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

        public void GetDoubleRoll () {
            _doubleRollCount++;
        }

        public void ResetDoubleRoll () {
            _doubleRollCount = 0;
        }

        public void UpdateWithStateData (PlayerGameStateData stateData) {
            SetStatePropertiesWithData (stateData);
        }

        #endregion

    }

}