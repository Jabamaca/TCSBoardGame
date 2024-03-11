using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using Monopoly.Define;
using Monopoly.Gameplay.Model;
using System;

namespace Monopoly.UI {
    public class GameplayUIController : MonoBehaviour {

        public delegate void OnRollDicePressHandler ();

        #region Properties

        [Header ("Animation Data")]
        [SerializeField]
        private float _messagePopInTime = 0.6f;
        [SerializeField]
        private float _messagePopOutTime = 0.2f;
        [SerializeField]
        private int _messagePopInCount = 4;

        [Header ("Wired Objects")]
        [Header ("Self Turn UI")]
        [SerializeField]
        private Button _rollDiceButton;
        [SerializeField]
        private TextMeshProUGUI _selfTurnLabel;

        [Header ("Other Turn UI")]
        [SerializeField]
        private TextMeshProUGUI _otherTurnLabel;

        [Header ("Other UI Elements")]
        [SerializeField]
        private TextMeshProUGUI _popInMessageText;
        [SerializeField]
        private List<GameplayPlayerUI> _playerUIPanels = new ();

        public OnRollDicePressHandler OnRollDiceButtonPress = delegate { };

        #endregion

        #region Methods

        private bool FindPlayerUIPanelWithPlayerID (int playerID, out GameplayPlayerUI playerUI) {
            playerUI = _playerUIPanels.Find ((panel) => panel.PlayerID == playerID);

            return playerUI != null;
        }

        #region Player Turn UI Methods

        public void HideTurnUI () {
            HideSelfTurnUI ();
            HideOtherTurnUI ();
        }

        public void ShowSelfTurnUI () {
            HideOtherTurnUI ();

            _rollDiceButton.gameObject.SetActive (true);
            _selfTurnLabel.gameObject.SetActive (true);
        }

        public void ShowOtherTurnUI (int playerID) {
            HideSelfTurnUI ();

            _otherTurnLabel.text = playerID + "'s TURN";
            _otherTurnLabel.gameObject.SetActive (true);
        }

        public void HideSelfTurnUIInput () {
            HideSelfTurnUI (inputOnly: true);
        }

        private void HideSelfTurnUI (bool inputOnly = false) {
            _rollDiceButton.gameObject.SetActive (false);

            if (inputOnly)
                return;

            _selfTurnLabel.gameObject.SetActive (false);
        }

        private void HideOtherTurnUI () {
            _otherTurnLabel.gameObject.SetActive (false);
        }

        #endregion

        #region Player UI Panel Methods

        public void ShowPlayerUIs () {
            foreach (var playerPanel in _playerUIPanels)
                playerPanel.gameObject.SetActive (true);
        }

        public void HidePlayerUIs () {
            foreach (var playerPanel in _playerUIPanels)
                playerPanel.gameObject.SetActive (false);
        }

        public void AssignPlayerIDAndColorToUIIndex (int playerID, PlayerColorEnum playerColor, int playerUIIndex) {
            if (playerUIIndex < 0 || playerUIIndex >= _playerUIPanels.Count)
                return;

            var playerUIPanel = _playerUIPanels[playerUIIndex];
            playerUIPanel.SetPlayerID (playerID);
            playerUIPanel.SetPlayerColor (playerColor);
        }

        public void SetPlayerUIAsYou (int playerID) {
            foreach (var pUI in _playerUIPanels) {
                pUI.SetYouPlayer (false);
            }

            if (FindPlayerUIPanelWithPlayerID (playerID, out var playerUI))
                playerUI.SetYouPlayer (true);
        }

        public void ResetTurningPlayers () {
            foreach (var pUI in _playerUIPanels) {
                pUI.SetIsTurningPlayer (false);
            }
        }

        public void SetPlayerUITurning (int playerID, bool turning) {
            ResetTurningPlayers ();

            if (FindPlayerUIPanelWithPlayerID (playerID, out var playerUI))
                playerUI.SetIsTurningPlayer (turning);
        }

        public void SetPlayerUICash (int playerID, int cash, bool animated = false) {
            if (FindPlayerUIPanelWithPlayerID (playerID, out var playerUI))
                playerUI.SetCashAmount (cash, animated);
        }

        #endregion

        #region Pop-In Message Methods

        public IEnumerator AnimateGameStartMessage () {
            yield return AnimatePopInMessage ("GAME START");
        }

        public IEnumerator AnimateYourTurnMessage () {
            yield return AnimatePopInMessage ("YOUR TURN");
        }

        public IEnumerator AnimatePlayerIDTurnMessage (int playerID) {
            yield return AnimatePopInMessage (playerID + "'s TURN");
        }

        private IEnumerator AnimatePopInMessage (string message) {
            _popInMessageText.text = message;

            for (int i = 0; i < _messagePopInCount; i++) {
                _popInMessageText.gameObject.SetActive (true);
                yield return new WaitForSeconds (_messagePopInTime);

                _popInMessageText.gameObject.SetActive (false);
                yield return new WaitForSeconds (_messagePopOutTime);
            }
        }

        #endregion

        #region Game State Methods

        public void UpdateWithGameState (GameState gState) {
            // Update Player objects.
            foreach (var playerUI in _playerUIPanels) {
                PlayerState pState = gState.GetPlayerState (playerUI.PlayerID);

                if (pState == null) {
                    continue;
                }

                playerUI.SetCashAmount (pState.CashAmount);
            }

            ResetTurningPlayers ();
            if (FindPlayerUIPanelWithPlayerID (gState.CurrentTurnPlayerID, out var turningPUI))
                turningPUI.SetIsTurningPlayer (true);
        }

        #endregion

        #endregion

        #region UI Responders

        public void UIRollDiceButtonPress () {
            OnRollDiceButtonPress?.Invoke ();
        }

        #endregion

    }
}
