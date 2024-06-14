using JabaUtils.Utility.Http;
using JabaUtils.Data;
using System.Collections.Generic;

namespace BlueMarble.BackEnd.GameAccount {
    public class AccountLoginHttp : HttpProcess {

        #region Properties

        public override string RequestURL => "https://api.lula.bet/api/auth/signin";

        private const string USERNAME_KEY = "username";
        private const string PASSWORD_KEY = "password";

        private readonly string _username;
        public string Username => _username;
        private readonly string _password;
        public string Password => _password;

        #endregion

        #region Constructors

        public AccountLoginHttp (string username, string password) {
            _username = username;
            _password = password;
        }

        #endregion

        #region Override HttpProcess

        public override string GetJsonString () {
            Dictionary<string, string> jsonDict = new () {
                {
                    USERNAME_KEY,
                    _username
                },
                {
                    PASSWORD_KEY,
                    _password
                }
            };

            return DictionaryUtils.DictToJsonString (jsonDict);
        }

        #endregion

    }
}