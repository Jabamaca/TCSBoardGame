using BlueMarble.Gameplay.Models.Defines;
using System;

namespace BlueMarble.Gameplay.StateData {
    public class BankTileGameStateData : TileGameStateData {

        #region Properties

        public override TileTypeEnum TileType => TileTypeEnum.Bank;

        public Int32 collectionTotal;

        #endregion

    }

}