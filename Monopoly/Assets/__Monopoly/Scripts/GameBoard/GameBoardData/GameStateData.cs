using System;
using System.Collections.Generic;
using GameUtils;
using MixedReality.Toolkit;

namespace Monopoly.Gameplay.Data {

    [Serializable]
    public class GameStateData {

        #region Properties

        public long gameTimeLeft;
        public int currentTurnPlayerID;
        public int bankCashAmount;
        public List<int> playerIDTurnOrder;
        public SerializableDictionary<int, PlayerStateData> playerStateDataDict;
        public SerializableDictionary<int, TileStateData> tileStateDataDict;

        #endregion

        #region Internal Methods

        public override bool Equals (object obj) {
            GameStateData other = (GameStateData)obj;

            // Check equality of ALL properties.
            if (this.bankCashAmount != other.bankCashAmount ||
                this.currentTurnPlayerID != other.currentTurnPlayerID ||
                !ListUtils.CheckEquals (this.playerIDTurnOrder, other.playerIDTurnOrder) ||
                !DictionaryUtils.CheckEquals (this.playerStateDataDict, other.playerStateDataDict) ||
                !DictionaryUtils.CheckEquals (this.tileStateDataDict, other.tileStateDataDict)) {

                return false;
            }

            return true;
        }

        public override int GetHashCode () {
            return base.GetHashCode ();
        }

        #endregion

        #region Static Methods

        public static void GenerateInitialPlayerDataDict (List<int> playerIDs, int startTileID, out SerializableDictionary<int, PlayerStateData> outDict) {
            outDict = new ();
            foreach (int pID in playerIDs) {
                outDict.Add (pID, new PlayerStateData {
                    playerID = pID,
                    placementTileID = startTileID,
                    cashAmount = 0,
                    jailTurns = 0
                });
            }
        }

        public static void GenerateInitialTileStateDataDict (GameBoardMapData mapData, out SerializableDictionary<int, TileStateData> outDict) {
            outDict = new ();
            foreach (var kvp in mapData.tilesData) {
                outDict.Add (kvp.Value.tileID, new TileStateData {
                    tileID = kvp.Value.tileID,
                    ownerPlayerID = -1,
                    propertyLevel = 0
                });
            }
        }

        #endregion

    }
}