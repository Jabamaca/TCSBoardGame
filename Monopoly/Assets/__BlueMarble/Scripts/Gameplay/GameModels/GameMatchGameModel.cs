using BlueMarble.Gameplay.RuleData;
using BlueMarble.Gameplay.StateData;
using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.Models {

    public abstract class GameMatchGameModel {

        #region Properties

        protected UInt32 _gameID;
        public UInt32 GameID => _gameID;

        protected readonly List<PlayerGameModel> _playerModels = new ();
        public IReadOnlyList<PlayerGameModel> PlayerModels => _playerModels;

        protected readonly List<UInt32> _playerIDTurnOrder = new ();
        public IReadOnlyList<UInt32> PlayerIDTurnOrder => _playerIDTurnOrder;
        protected UInt32 _currentTurnPlayerID;
        public UInt32 CurrentTurnPlayerID => _currentTurnPlayerID;

        protected readonly List<TileGameModel> _tileModels = new ();
        public IReadOnlyList<TileGameModel> TileModels => _tileModels;

        protected readonly List<ItemGameModel> _itemModelLibrary = new ();
        public IReadOnlyList<ItemGameModel> ItemModelLibrary => _itemModelLibrary;

        #endregion

        #region Methods

        #region Game Rule and State Methods

        protected void InitWithRuleData (GameMatchGameRuleData ruleData) {
            _gameID = ruleData.gameID;
            InitItemModelLibraryWithRuleDataList (ruleData.itemLibraryRuleDataList);
            InitTileModelsWithRuleDataList (ruleData.tileOrderRuleDataList);
        }

        private void InitItemModelLibraryWithRuleDataList (List<ItemGameRuleData> ruleDataLibrary) {
            _itemModelLibrary.Clear ();
            foreach (var ruleData in ruleDataLibrary) {
                _itemModelLibrary.Add (ItemGameModelFactory.MakeItemWithRuleData (ruleData));
            }
        }

        private void InitTileModelsWithRuleDataList (List<TileGameRuleData> ruleDataList) {
            _tileModels.Clear ();
            foreach (var ruleData in ruleDataList) {
                _tileModels.Add (TileGameModelFactory.MakeTileWithRuleData (ruleData));
            }
        }

        public void UpdateWithGameStateData (GameMatchGameStateData stateData) {
            UpdatePlayerModelsWithStateDataList (stateData.playerStateDataList);

            _playerIDTurnOrder.Clear ();
            _playerIDTurnOrder.AddRange (stateData.playerIDTurnOrder);
            _currentTurnPlayerID = stateData.currentTurnPlayerID;

            UpdateTileModelsWithStateDataList (stateData.tileStateDataList);
        }

        private void UpdatePlayerModelsWithStateDataList (List<PlayerGameStateData> stateDataList) {
            _playerModels.Clear ();
            foreach (var stateData in stateDataList) {
                _playerModels.Add (new PlayerGameModel (stateData));
            }
        }

        private void UpdateTileModelsWithStateDataList (List<TileGameStateData> stateDataList) {
            foreach (var stateData in stateDataList) {
                if (GetTileModelWithID (stateData.tileID, out var tileModel)) {
                    tileModel.UpdateWithStateData (stateData);
                }
            }
        }

        #endregion

        #region Query Methods

        private bool GetTileModelWithID (int tileID, out TileGameModel tileModel) {
            tileModel = _tileModels.Find ((tm) => tm.TileID == tileID);

            return tileModel != null;
        }

        #endregion

        #endregion

    }

}