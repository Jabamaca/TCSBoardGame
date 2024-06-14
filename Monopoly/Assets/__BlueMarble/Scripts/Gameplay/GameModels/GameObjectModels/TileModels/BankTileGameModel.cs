using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using BlueMarble.Gameplay.StateData;
using System;

namespace BlueMarble.Gameplay.Models {
    public class BankTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Bank;

        // Game Rule Properties

        private Int32 _bankFee;
        public Int32 BankFee => _bankFee;

        // Game State Properties

        private Int32 _collectionTotal;
        public Int32 collectionTotal => _collectionTotal;

        #endregion

        #region Constructors

        public BankTileGameModel (BankTileGameRuleData gameRule) {
            SetRulePropertiesWithData (gameRule);
            SetStatePropertiesDefault ();
        }

        public BankTileGameModel (BankTileGameRuleData gameRule, BankTileGameStateData stateData) {
            SetRulePropertiesWithData (gameRule);
            SetStatePropertiesWithData (stateData);
        }

        private void SetRulePropertiesWithData (BankTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _bankFee = ruleData.bankFee;
        }

        private void SetStatePropertiesWithData (BankTileGameStateData stateData) {
            _collectionTotal = stateData.collectionTotal;
        }

        private void SetStatePropertiesDefault () {
            _collectionTotal = 0;
        }

        #endregion

        #region Methods

        public void CollectCash (int cashAmount) {
            _collectionTotal += cashAmount;
        }

        public void GiveAwayCash (out int totalCashGiven) {
            totalCashGiven = _collectionTotal;
            _collectionTotal = 0;
        }

        public void UpdateWithStateData (BankTileGameStateData stateData) {
            SetStatePropertiesWithData (stateData);
        }

        #endregion

    }
}