using TMPro;
using UnityEngine;
using System;
using JabaUtils.Utility.Http;
using BlueMarble.BackEnd.GameAccount;

namespace Monopoly.UI {
    public class LoginUI : MonoBehaviour {

        #region Properties

        [Header ("Wired Objects")]
        [SerializeField]
        private TMP_InputField _usernameField;
        [SerializeField]
        private TMP_InputField _passwordField;

        #endregion

        #region Methods

        private void AttemptLogin () {
            string username = _usernameField.text;
            string password = _passwordField.text;

            ProcessLogin (username, password);
        }

        private void ProcessLogin (string username, string password) {
            AccountManager.AttemptLogin (username, password,
                successAction: LoginSuccess,
                internalErrorAction: LoginInternalError,
                exceptionErrorAction: LoginExceptionError);
        }

        private void LoginSuccess (string responseData) {
            Debug.Log (responseData);
        }

        private void LoginInternalError (string responseData) {
            Debug.Log (responseData);
        }

        private void LoginExceptionError (Exception e) {
            Debug.Log (e);
        }

        #endregion

        #region UI Responders

        public void OnLoginButtonClicked () {
            AttemptLogin ();
        }

        #endregion

    }
}