using System;
using System.Collections.Generic;

namespace BlueMarble.Gameplay.Models {

    public abstract class GameMatchGameModel {

        #region Properties

        protected UInt32 _gameID;
        public UInt32 GameID => _gameID;

        protected readonly List<PlayerGameModel> _playerModels = new ();
        public IReadOnlyList<PlayerGameModel> PlayerModels => _playerModels;

        protected readonly List<TileGameModel> _tileModels = new ();
        public IReadOnlyList<TileGameModel> TileModels => _tileModels;

        #endregion

        #region Methods

        #endregion

    }

}