using UnityEngine;
using TMPro;
using DG.Tweening;

namespace GameUtils.UI {

    public class CountingLabel : MonoBehaviour {

        #region Properties

        [Header ("Counting Info")]
        [SerializeField]
        private int _currentFinalValue = 0;
        [SerializeField]
        private string _countPrefix;
        [SerializeField]
        private string _countSuffix;
        [SerializeField]
        private int _countRate = 100;

        [Header ("Wired Objects")]
        [SerializeField]
        private TextMeshProUGUI _countingText;

        private int _currentCountingValue;
        private string FormattedCountString => _countPrefix + _currentCountingValue + _countSuffix;
        private Tween _countingTween;

        #endregion

        #region Unity Internal Methods

#if UNITY_EDITOR

        private void OnValidate () {
            StopCurrentCounting ();

            _currentCountingValue = _currentFinalValue;
            _countingText.text = FormattedCountString;
        }

#endif

        #endregion

        #region Methods

        public void SetValue (int value, bool isAnimated = false) {
            StopCurrentCounting ();

            _currentFinalValue = value;

            if (isAnimated) {
                float countTime = (float)Mathf.Abs (_currentFinalValue - _currentCountingValue) / _countRate;
                _countingTween = DOTween.To (() => _currentCountingValue, v => UpdateCountText (v), _currentFinalValue, countTime);
            } else {
                _currentCountingValue = _currentFinalValue;
                _countingText.text = FormattedCountString;
            }
        }

        private void UpdateCountText (int count) {
            _currentCountingValue = count;
            _countingText.text = FormattedCountString;
        }

        private void StopCurrentCounting () {
            _countingTween?.Kill ();
            _countingTween = null;
        }

        #endregion

    }

}