using System;

namespace BlueMarble.BackEnd.GameAccount.Data {
    [Serializable]
    public class AccountLoginResponseData {

        #region Properties

        public string type;
        public string message;
        public AccountLoginData data;

        #endregion

    }
}