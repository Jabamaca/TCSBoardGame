using BlueMarble.BackEnd.GameAccount.Data;
using JabaUtils.Utility.Http;
using System;
using UnityEngine;

namespace BlueMarble.BackEnd.GameAccount {
    public static class AccountManager {

        #region Properties

        private static string _loginToken;
        public static string LoginToken => _loginToken;
        private static UserAccount _userAccount;

        #endregion

        #region Methods

        public static void AttemptLogin (string username, string password, Action<string> successAction,
            Action<string> internalErrorAction, Action<Exception> exceptionErrorAction) {

            AccountLoginRequest loginHttp = new (username, password);
            loginHttp.SetSuccessAction ((rds) => {
                var responseData = JsonUtility.FromJson<AccountLoginResponseData> (rds);
                _userAccount = new (responseData.data.user);

                successAction (rds);
            });
            loginHttp.SetInternalFailAction (internalErrorAction);
            loginHttp.SetExceptionFailAction (exceptionErrorAction);

            HttpProcessing.ProcessHttpRequest (loginHttp);
        }

        #endregion

    }
}