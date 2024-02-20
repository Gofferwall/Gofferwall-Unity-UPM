#if UNITY_ANDROID
using Gofferwall.Model;
using System;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    internal class Utils
    {
        public static GofferwallError ConvertToGofferwallError(AndroidJavaObject error)
        {
            int code;
            string message;

            if (error != null)
            {
                try
                {
                    code = error.Call<int>(Values.MTD_GET_CODE);
                }
                catch (Exception e)
                {
                    Debug.LogError("Android.Utils<ConvertToGofferwallError> failed to get code: " + e);
                    code = -1;
                }

                try
                {
                    message = error.Call<string>(Values.MTD_GET_MESSAGE);
                }
                catch (Exception e)
                {
                    Debug.LogError("Android.Utils<ConvertToGofferwallError> failed to get message: " + e);
                    message = "parsing error";
                }
            }
            else
            {
                Debug.LogError("Android.Utils<ConvertToGofferwallError> object from android null");
                code = -1;
                message = "no error info";
            }

            return new GofferwallError(code, message);
        }
    }
}
#endif