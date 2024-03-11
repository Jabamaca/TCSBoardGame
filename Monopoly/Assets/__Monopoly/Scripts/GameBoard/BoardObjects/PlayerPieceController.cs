using UnityEngine;
using DG.Tweening;
using Monopoly.Define;
using Monopoly.Scriptables;

namespace Monopoly.Gameplay {
    public class PlayerPieceController : MonoBehaviour {
        private const float MOVE_ANIM_HEIGHT = 0.5f;

        #region Properties

        [Header ("Gameplay Data")]
        public int playerID;

        [SerializeField]
        private PlayerColorEnum _playerColor;
        public PlayerColorEnum PlayerColor => _playerColor;

        public int placementTileID;

        [Header ("Wired Objects")]
        [SerializeField]
        private Transform _playerGraphicTransform;
        [SerializeField]
        private MeshRenderer _playerModel;
        [SerializeField]
        private ColorMatLibrary _playerColorLib;

        private Tween _currentAnim;

        #endregion

        #region Unity Internal Methods

#if UNITY_EDITOR

        private void OnValidate () {
            _playerModel.material = _playerColorLib.ColorMaterialAtIndex ((int)PlayerColor);
        }

#endif

        #endregion

        #region Methods

        internal void MovePlayerPiece (Vector3 position, bool animated) {
            if (animated) {
                AnimateMovement (position);
            } else {
                transform.position = position;
            }
        }

        private void AnimateMovement (Vector3 finalPos) {
            _currentAnim?.Kill ();

            float animTime = MonopolyConst.SINGLE_SPACE_MOVE_ANIM_TIME;

            Sequence movSeq = DOTween.Sequence ();
            movSeq.Append (_playerGraphicTransform.DOLocalMoveY (MOVE_ANIM_HEIGHT, animTime / 2f).SetEase (Ease.InQuad));
            movSeq.Append (_playerGraphicTransform.DOLocalMoveY (0f, animTime / 2f).SetEase (Ease.OutQuad));
            movSeq.Insert (0f, transform.DOMove (finalPos, animTime).SetEase (Ease.InOutQuad));
            movSeq.OnKill (() => {
                _currentAnim = null;
            });

            _currentAnim = movSeq;
            _currentAnim.Play ();
        }

        #endregion

    }
}
