using System;
using System.Collections.Generic;
using GameUtils;
using MixedReality.Toolkit;

namespace Monopoly.Gameplay.Data {

    [Serializable]
    public class GameBoardMapData {

        #region Properties

        public int startingTileID;
        public SerializableDictionary<int, GameBoardMapTileData> tilesData;

        #endregion

        #region Methods

        public int GetTileIDSpaceAwayFromTileID (int spaces, int tileID) {
            int currTileID = tileID;
            if (spaces > 0) { // Move space forwards.
                for (int i = 0; i < spaces; i++) {
                    currTileID = tilesData[currTileID].nextTile;
                }
            } else if (spaces < 0) { // Move space backwards.
                for (int i = 0; i > spaces; i--) {
                    currTileID = tilesData[currTileID].prevTile;
                }
            }

            return currTileID;
        }

        #endregion

        #region Internal Methods

        public override bool Equals (object obj) {
            GameBoardMapData other = obj as GameBoardMapData;

            if (this.startingTileID != other.startingTileID) {
                return false;
            }

            // Dictionary data count check.
            if (!DictionaryUtils.CheckEquals (this.tilesData, other.tilesData))
                return false;

            return true;
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        #endregion

    }

}