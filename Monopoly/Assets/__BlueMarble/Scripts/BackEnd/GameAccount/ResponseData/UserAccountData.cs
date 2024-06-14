using System;

namespace BlueMarble.BackEnd.GameAccount.Data {
    [Serializable]
    public class UserAccountData {

        #region Properties

        public int id;
        public string username;
        public string nickname;
        public string userType;
        public string icon;
        public int level;

        #endregion

    }
}