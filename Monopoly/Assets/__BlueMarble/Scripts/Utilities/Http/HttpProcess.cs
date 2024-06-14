using System;

namespace JabaUtils.Utility.Http {
    public abstract class HttpProcess {

        #region Properties

        public virtual string RequestURL => "";

        protected Action<string> _successAction;
        public Action<string> SuccessAction => _successAction;
        protected Action<string> _internalErrorAction;
        public Action<string> InternalErrorAction => _internalErrorAction;
        protected Action<Exception> _exceptionErrorAction;
        public Action<Exception> ExceptionErrorAction => _exceptionErrorAction;

        #endregion

        #region Methods

        public void SetSuccessAction (Action<string> action) {
            _successAction = action;
        }

        public void SetInternalFailAction (Action<string> action) {
            _internalErrorAction = action;
        }

        public void SetExceptionFailAction (Action<Exception> action) {
            _exceptionErrorAction = action;
        }

        public abstract string GetJsonString ();

        #endregion

    }
}