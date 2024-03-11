using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Monopoly.Scriptables;
using Monopoly.Define;
using GameUtils.UI;
using DG.Tweening;

namespace Monopoly.UI {

    public class GameplayPlayerUI : MonoBehaviour {

        private const string CASH_GAIN_FORMAT = "+ $";
        private const string CASH_LOSS_FORMAT = "- $";
        private static readonly Color WAITING_PLAYER_COLOR = new (0.5f, 0.5f, 0.5f, 0.75f);

        #region Properties

        [Header ("UI Info")]
        [SerializeField]
        private int _playerID;
        public int PlayerID => _playerID;
        [SerializeField]
        private PlayerColorEnum _playerColor;
        [SerializeField]
        private bool _isYou;
        [SerializeField]
        private bool _isTurningPlayer;
        [SerializeField]
        private int _currentCash;

        [Header ("Wired Objects")]
        [SerializeField]
        private TextMeshProUGUI _playerNameLabel;
        [SerializeField]
        private TextMeshProUGUI _youTag;
        [SerializeField]
        private Image _portraitBG;
        [SerializeField]
        private Image _infoBG;
        [SerializeField]
        private CountingLabel _cashLabel;
        [SerializeField]
        private TextMeshProUGUI _cashGainLabel;
        [SerializeField]
        private TextMeshProUGUI _cashLossLabel;
        [SerializeField]
        private ColorMatLibrary _playerColorLib;

        private Color PlayerBaseColor => _playerColorLib.ColorBaseAtIndex ((int)_playerColor);

        #endregion

        #region Unity Internal Methods

#if UNITY_EDITOR

        private void OnValidate () {
            RefreshNameLabel ();
            RefreshColorUI ();
            RefreshYouTag ();
            RefreshCashLabel (animated: false);
        }

#endif

        #endregion

        #region Methods

        public void SetPlayerID (int playerID) {
            _playerID = playerID;
            RefreshNameLabel ();
        }

        public void SetPlayerColor (PlayerColorEnum playerColor) {
            _playerColor = playerColor;
            RefreshColorUI ();
        }

        public void SetCashAmount (int cash, bool animated = false) {
            if (animated) {
                AnimateCashDiff (cash);
            }

            _currentCash = cash;
            RefreshCashLabel (animated);
        }

        public void SetIsTurningPlayer (bool isTurningPlayer) {
            _isTurningPlayer = isTurningPlayer;
            RefreshColorUI ();
        }

        public void SetYouPlayer (bool isYou) {
            _isYou = isYou;
            RefreshYouTag ();
        }

        private void RefreshNameLabel () {
            _playerNameLabel.text = _playerID.ToString ();
        }

        private void RefreshYouTag () {
            _youTag.gameObject.SetActive (_isYou);
        }

        private void RefreshColorUI () {
            _portraitBG.color = PlayerBaseColor;

            _infoBG.color = _isTurningPlayer ? PlayerBaseColor : WAITING_PLAYER_COLOR;
        }

        private void RefreshCashLabel (bool animated) {
            _cashLabel.SetValue (_currentCash, animated);
        }

        private void AnimateCashDiff (int newValue) {
            int oldValue = _currentCash;
            int cashDiff = newValue - oldValue;

            if (cashDiff > 0) {
                AnimateCashGain (cashDiff);
            } else if (cashDiff < 0) {
                AnimateCashLoss (cashDiff);
            }
        }

        private void AnimateCashGain (int cashDiff) {
            _cashGainLabel.text = CASH_GAIN_FORMAT + cashDiff;
            _cashGainLabel.gameObject.SetActive (true);
            _cashGainLabel.transform.localPosition = new Vector3 (0f, 0f, 0f);
            Tween goUp = _cashGainLabel.transform.DOLocalMoveY (30f, 2f);
            goUp.SetEase (Ease.Linear);
            goUp.OnKill (() => {
                _cashGainLabel.gameObject.SetActive (false);
            });
        }

        private void AnimateCashLoss (int cashDiff) {
            _cashLossLabel.text = CASH_LOSS_FORMAT + Mathf.Abs (cashDiff);
            _cashLossLabel.gameObject.SetActive (true);
            _cashLossLabel.transform.localPosition = new Vector3 (0f, 0f, 0f);
            Tween goUp = _cashLossLabel.transform.DOLocalMoveY (30f, 2f);
            goUp.SetEase (Ease.Linear);
            goUp.OnKill (() => {
                _cashLossLabel.gameObject.SetActive (false);
            });
        }

        #endregion

    }

}