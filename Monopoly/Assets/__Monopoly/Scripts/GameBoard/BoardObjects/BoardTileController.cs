using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Monopoly.Define;
using Monopoly.Gameplay.Data;
using Monopoly.Scriptables;

namespace Monopoly.Gameplay {
    public class BoardTileController : MonoBehaviour {

        #region Properties

        private const float PLAYER_TILE_AREA_RADIUS = 0.45f;
        private const float MULTI_PIECE_START_ANGLE = -Mathf.PI / 4f;

        private readonly List<PlayerPieceController> _containedPlayers = new ();

        [Header ("Gameplay Properties")]

        [SerializeField]
        private int _tileID;
        public int TileID => _tileID;

        [SerializeField]
        private BoardTileController _previousTile;
        public BoardTileController PreviousTile => _previousTile;

        [SerializeField]
        private BoardTileController _nextTile;
        public BoardTileController NextTile => _nextTile;

        [Header ("Object Display")]
        [SerializeField]
        private string _tileNameDefaultString;
        [SerializeField]
        private string _tileNameLocalizationKey;
        [SerializeField]
        private TileColorEnum _tileColor;
        [SerializeField]
        private Material _tileIconMaterial;

        [Header ("Wired Game Objects")]
        [SerializeField]
        private TextMeshPro _tileNameLabel;
        [SerializeField]
        private MeshRenderer _tileIconMeshRenderer;
        [SerializeField]
        private MeshRenderer _tileColorMeshRenderer;
        [SerializeField]
        private Transform _containedPlayersCenter;
        [SerializeField]
        private ColorMatLibrary _tileColorLibrary;
        [SerializeField]
        private PropertyHouseController _propertyHouse;

        #endregion

        #region Unity Internal Methods

#if UNITY_EDITOR

        private void OnValidate () {
            if (_tileIconMeshRenderer != null) {
                _tileIconMeshRenderer.material = _tileIconMaterial;
            }

            if (_tileNameLabel != null) {
                _tileNameLabel.text = _tileNameDefaultString;
            }

            if (_tileColorMeshRenderer != null) {
                UpdateColorMaterial ();
            }
        }

#endif

        #endregion

        #region Methods

        public void PutPlayerPiece (PlayerPieceController player, bool animated = false) {
            if (_containedPlayers.Contains (player)) {
                return;
            }

            _containedPlayers.Add (player);
            UpdatePlayerPiecePositions (animated);
        }

        public void RemovePlayerPiece (PlayerPieceController player, bool animated = false) {
            if (!_containedPlayers.Contains (player)) {
                return;
            }

            _containedPlayers.Remove (player);
            UpdatePlayerPiecePositions (animated);
        }

        private void UpdatePlayerPiecePositions (bool animated) {
            int playerCount = _containedPlayers.Count;

            if (playerCount == 1) {
                _containedPlayers[0].MovePlayerPiece (_containedPlayersCenter.position, animated);
            } else if (playerCount > 1) {
                float angleDiff = 2f * Mathf.PI / playerCount;
                float currentAngle = MULTI_PIECE_START_ANGLE;

                foreach (PlayerPieceController player in _containedPlayers) {
                    Vector3 posDiff = new (Mathf.Cos (currentAngle), 0f, Mathf.Sin (currentAngle));
                    posDiff *= PLAYER_TILE_AREA_RADIUS;

                    player.MovePlayerPiece (_containedPlayersCenter.position + posDiff, animated);

                    currentAngle += angleDiff;
                }
            }
        }

        public void SetPropertyOwnerAndLevel (PlayerColorEnum ownerColor, int level) {
            _propertyHouse.SetPlayerColor (ownerColor);
            _propertyHouse.SetUpgradeLevel (level);
        }

        public void BuyProperty (PlayerColorEnum buyerColor) {
            _propertyHouse.SetPlayerColor (buyerColor);
            _propertyHouse.SetUpgradeLevel (1);
        }

        public void SellProperty () {
            _propertyHouse.SetPlayerColor (PlayerColorEnum.Null);
            _propertyHouse.SetUpgradeLevel (0);
        }

        public void UpgradeProperty () {
            _propertyHouse.UpgradeLevelUp ();
        }

        #region Data Methods

        public GameBoardMapTileData GenerateBoardTileData () {
            GameBoardMapTileData returnData = new () {
                tileID = this.TileID,
                prevTile = this.PreviousTile.TileID,
                nextTile = this.NextTile.TileID
            };

            return returnData;
        }

        #endregion

#if UNITY_EDITOR

        #region Editor Methods

        private void UpdateColorMaterial () {
            _tileColorMeshRenderer.enabled = _tileColor != TileColorEnum.Null;

            Material colorMat = _tileColorLibrary.ColorMaterialAtIndex ((int)_tileColor);
            _tileColorMeshRenderer.material = colorMat;

        }

        internal void SetTileID (int id) {
            _tileID = id;
        }

        internal void SetPreviousTile (BoardTileController tile) {
            _previousTile = tile;
        }

        internal void SetNextTile (BoardTileController tile) {
            _nextTile = tile;
        }

        #endregion

#endif

        #endregion

    }
}