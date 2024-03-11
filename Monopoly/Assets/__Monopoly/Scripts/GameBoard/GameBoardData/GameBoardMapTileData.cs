using System;

namespace Monopoly.Gameplay.Data {

    [Serializable]
    public class GameBoardMapTileData {

        #region Properties

        public int tileID;
        public int prevTile;
        public int nextTile;

        #endregion

        #region Internal Methods

        public override bool Equals (object obj) {
            GameBoardMapTileData other = obj as GameBoardMapTileData;

            if (this.tileID != other.tileID ||
                this.prevTile != other.prevTile ||
                this.nextTile != other.nextTile) {

                return false;
            }

            return true;
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        #endregion

    }
}