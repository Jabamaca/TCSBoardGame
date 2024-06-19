using BlueMarble.Gameplay.Models.Defines;
using BlueMarble.Gameplay.RuleData;
using BlueMarble.Gameplay.StateData;
using System;
using UnityEngine.Tilemaps;

namespace BlueMarble.Gameplay.Models {
    public class BankTileGameModel : TileGameModel {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Bank;

        // Game Rule Properties

        private Int32 _bankFee;
        public Int32 BankFee => _bankFee;

        // Game State Properties

        private Int32 _collectionTotal;
        public Int32 CollectionTotal => _collectionTotal;

        #endregion

        #region Constructors

        public BankTileGameModel (BankTileGameRuleData ruleData) {
            SetBasicRulePropertiesWithData (ruleData);

            _bankFee = ruleData.bankFee;

            _collectionTotal = 0;
        }

        #endregion

        #region Overrides

        public override void UpdateWithStateData (TileGameStateData stateData) {
            base.UpdateWithStateData (stateData);

            var bankTileStateData = stateData as BankTileGameStateData;
            _collectionTotal = bankTileStateData.collectionTotal;
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

        #endregion

    }
}