#if UNITY_ANDROID
using Gofferwall.Model;
using System;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    public class GofferwallInitializeListener : AndroidJavaProxy
    {
        Action<bool> callback;

        public GofferwallInitializeListener(Action<Boolean> callback) : base(Values.PKG_GLOBAL_SDK_INITIALIZATION_LISTENER)
        {
            this.callback = callback;
        }

        [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
        void onInitialized(bool isSuccess)
        {
            Debug.Log("onInitialized : " + isSuccess);
            callback.Invoke(isSuccess);
        }
    }
}
#endif