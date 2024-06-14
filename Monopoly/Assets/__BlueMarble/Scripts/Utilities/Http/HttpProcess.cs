using System;

namespace JabaUtils.Utility.Http {
    public abstract class HttpProcess {

        #region Properties

        public virtual string RequestURL => "";

        protected Action<string> _successAction;
        public Action<string> SuccessAction => _successAction;
        protected Action<string> _internalFailAction;
        public Action<string> InternalFailAction => _internalFailAction;
        protected Action<Exception> _exceptionFailAction;
        public Action<Exception> ExceptionFailAction => _exceptionFailAction;

        protected string _jsonString;
        public string JsonString => _jsonString;

        #endregion

        #region Methods

        public void SetSuccessAction (Action<string> action) {
            _successAction = action;
        }

        public void SetInternalFailAction (Action<string> action) {
            _internalFailAction = action;
        }

        public void SetExceptionFailAction (Action<Exception> action) {
            _exceptionFailAction = action;
        }

        #endregion

    }
}