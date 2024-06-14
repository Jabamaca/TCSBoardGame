using System;
using System.Net.Http;
using System.Text;

namespace JabaUtils.Utility.Http {
    public static class HttpProcessing {

        #region Properties

        private static readonly HttpClient _sharedClient = new ();

        #endregion

        #region Methods

        public static async void ProcessHttp (HttpProcess httpProcess) {
            try {
                HttpContent content = new StringContent (httpProcess.JsonString, Encoding.UTF8, "application/json");
                var response = await _sharedClient.PostAsync (httpProcess.RequestURL, content);
                string responseData = await response.Content.ReadAsStringAsync ();

                if (response.IsSuccessStatusCode) {
                    httpProcess.SuccessAction?.Invoke (responseData);
                } else {
                    httpProcess.InternalFailAction?.Invoke (responseData);
                }
            } catch (Exception e) {
                httpProcess.ExceptionFailAction?.Invoke (e);
            }
        }

        #endregion

    }
}