using BlueMarble.BackEnd.GameAccount.Data;

namespace BlueMarble.BackEnd.GameAccount {
    public class UserAccount {

        #region Properties

        private int _userID;
        private string _userName;
        private string _nickName;
        private string _userIconUrl;

        #endregion

        #region Constructors

        public UserAccount (UserAccountData data) {
            _userID = data.id;
            _userName = data.username;
            _nickName = data.nickname;
            _userIconUrl = data.icon;
        }

        #endregion

        #region Methods



        #endregion

    }
}