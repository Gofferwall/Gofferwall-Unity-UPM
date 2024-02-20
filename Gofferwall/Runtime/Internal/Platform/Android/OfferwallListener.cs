#if UNITY_ANDROID
using Gofferwall.Feature;
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    public class OfferwallListener : AndroidJavaProxy
    {
        Action<string> onOpenedCallback;
        Action<string, GofferwallError> onFailedToOpenCallback;

        public OfferwallListener(Action<string> onOpenedCallback, 
            Action<string, GofferwallError> onFailedToOpenCallback) : base(Values.PKG_GLOBAL_OFFERWALL_LISTENER)
        {
            this.onOpenedCallback = onOpenedCallback;
            this.onFailedToOpenCallback = onFailedToOpenCallback;
        }

        [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
        void onOfferwallOpened(string unitId)
        {
            if (onOpenedCallback == null)
                return;

            this.onOpenedCallback.Invoke(unitId);
        }

        [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
        void onOfferwallFailedToOpen(string unitId, AndroidJavaObject error)
        {
            if (onFailedToOpenCallback == null)
                return;

            GofferwallError errorResult = null;
            if (error != null)
            {
                errorResult = Utils.ConvertToGofferwallError(error);
            }

            this.onFailedToOpenCallback.Invoke(unitId, errorResult);
        }
    }
}
#endif