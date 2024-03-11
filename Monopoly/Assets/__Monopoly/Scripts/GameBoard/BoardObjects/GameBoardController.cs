using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Monopoly.Define;
using Monopoly.Gameplay.Data;
using Monopoly.Gameplay.Model;
using MixedReality.Toolkit;

namespace Monopoly.Gameplay {
    public class GameBoardController : MonoBehaviour {

        #region Properties

        [SerializeField]
        private string _mapName;

        [SerializeField]
        private DiceRollController _diceRoller;
        [SerializeField]
        private List<PlayerPieceController> _playerPieces = new ();
        [SerializeField]
        private BoardTileController _startingTile;
        [SerializeField]
        private List<BoardTileController> _boardTiles = new ();

        #endregion

        #region Unity Internal Methods

        private void OnDestroy () {
            StopAllCoroutines ();
        }

#if UNITY_EDITOR

#endif

        #endregion

        #region Methods

        public IEnumerator AnimateMovePlayerIDToTileID (int playerID, int tileID, bool sequential = false) {
            if (sequential) {
                FindPlayerWithID (playerID, out var movingPlayer);
                while (movingPlayer.placementTileID != tileID) {
                    FindTileWithID (movingPlayer.placementTileID, out var currTile);
                    MovePlayerToTile (movingPlayer, currTile.NextTile, animated: true);
                    yield return new WaitForSeconds (MonopolyConst.SINGLE_SPACE_MOVE_ANIM_TIME);
                }
            } else {
                MovePlayerIDToTileID (playerID, tileID, animated: true);
                yield return new WaitForSeconds (MonopolyConst.SINGLE_SPACE_MOVE_ANIM_TIME);
            }
        }

        public IEnumerator AnimateDiceRoll (List<int> diceResults) {
            _diceRoller.Roll (diceResults);
            yield return new WaitForSeconds (MonopolyConst.DICE_ROLLING_ANIM_TIME);
            _diceRoller.ResetAllDice ();
        }

        public void MovePlayerIDToTileID (int playerID, int tileID, bool animated = false) {
            if (!FindPlayerWithID (playerID, out var playerPiece) ||
                !FindTileWithID (tileID, out var tile))
                return;

            MovePlayerToTile (playerPiece, tile, animated);
        }

        public void AssignPlayerIDToColor (int playerID, PlayerColorEnum color) {
            if (FindPlayerPieceWithColor (color, out var playerPiece)) {
                playerPiece.playerID = playerID;
            }
        }

        private void MovePlayerToTile (PlayerPieceController player, BoardTileController tile, bool animated = false) {
            // Try remove player from old tile.
            int prevPlacement = player.placementTileID;
            if (FindTileWithID (prevPlacement, out var prevTile)) {
                prevTile.RemovePlayerPiece (player, animated);
            }

            // Player placement to tile.
            player.placementTileID = tile.TileID;
            tile.PutPlayerPiece (player, animated);
        }

        private bool FindTileWithID (int tileID, out BoardTileController tile) {
            tile = _boardTiles.Find ((t) => t.TileID == tileID);

            return tile != null;
        }

        private bool FindPlayerWithID (int playerID, out PlayerPieceController playerPiece) {
            playerPiece = _playerPieces.Find ((pp) => pp.playerID == playerID);

            return playerPiece != null;
        }

        private bool FindPlayerPieceWithColor (PlayerColorEnum color, out PlayerPieceController playerPiece) {
            playerPiece = _playerPieces.Find ((pp) => pp.PlayerColor == color);

            return playerPiece != null;
        }

        #region Game State Methods

        public void UpdateWithGameState (GameState gState) {
            // Update Player objects.
            foreach (var player in _playerPieces) {
                PlayerState pState = gState.GetPlayerState (player.playerID);

                if (pState == null) {
                    continue;
                }

                // Update player piece placement on board.
                if (FindTileWithID (pState.PlacementTileID, out var currTile)) {
                    MovePlayerToTile (player, currTile, animated:false);
                }
            }

            // Update Tile object.
            foreach (var tile in _boardTiles) {
                TileState tState = gState.GetTileState (tile.TileID);

                if (tState == null) {
                    continue;
                }

                PlayerColorEnum ownerColor = GetColorOfPlayerID (tState.OwnerPlayerID);
                tile.SetPropertyOwnerAndLevel (ownerColor, tState.PropertyLevel);
            }
        }

        private PlayerColorEnum GetColorOfPlayerID (int playerID) {
            PlayerPieceController player = _playerPieces.Find ((p) => p.playerID == playerID);

            if (player != null)
                return player.PlayerColor;

            return PlayerColorEnum.Null;
        }

        public GameBoardMapData GenerateBoardMapData () {
            GameBoardMapData returnData = new () {
                startingTileID = _startingTile.TileID
            };

            SerializableDictionary<int, GameBoardMapTileData> tilesDict = new ();
            foreach (BoardTileController tile in _boardTiles) {
                tilesDict.Add (tile.TileID, tile.GenerateBoardTileData ());
            }
            returnData.tilesData = tilesDict;

            return returnData;
        }

        #endregion

        #endregion

#if UNITY_EDITOR

        #region EDITOR_ONLY

        private const string MAP_DATA_FILENAME = "_BoardMap.json";
        private string MapDataFileName => "/" + _mapName + MAP_DATA_FILENAME;

        public void GenerateBoardDataJSON () {
            GameBoardMapData mapData = GenerateBoardMapData ();
            string generatedString = JsonUtility.ToJson (mapData);
            string fullPath = Application.dataPath + MapDataFileName;

            System.IO.File.WriteAllText (fullPath, generatedString);

            Debug.Log ("GAME BOARD MAP DATA GENERATED: See \"" + fullPath + "\"");
        }

        #endregion

#endif

    }
}