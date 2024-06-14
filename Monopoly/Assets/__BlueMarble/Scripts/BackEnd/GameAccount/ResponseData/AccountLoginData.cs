using System;

namespace BlueMarble.BackEnd.GameAccount.Data {
    [Serializable]
    public class AccountLoginData {

        #region Properties

        public string token;
        public UserAccountData user;

        #endregion

    }
}