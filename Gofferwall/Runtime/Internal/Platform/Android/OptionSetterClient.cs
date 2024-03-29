﻿#if UNITY_ANDROID
using Gofferwall.Internal.Interface;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    /// <summary>
    /// android client for option setter
    /// this class will call android native plugin's method
    /// </summary>
    internal class OptionSetterClient : IOptionSetterClient
    {
        private AndroidJavaObject optionSetter;

        public OptionSetterClient()
        {
            this.optionSetter = GetOptionSetterInstance();

            if (this.optionSetter == null)
            {
                Debug.LogError("Android.OptionSetterClient<Constructor> OptionSetter: null");
                return;
            }
        }

        private AndroidJavaObject GetOptionSetterInstance()
        {
            AndroidJavaObject activity = null;

            using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
            {
                if (unityPlayer == null)
                {
                    Debug.LogError("Android.OptionSetterClient<Constructor> UnityPlayer: null");
                    return null;
                }
                activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
            }

            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
            {
                if (jc == null)
                {
                    Debug.LogError("Android.OptionSetterClient<getOptionSetterInstance> " +
                        Values.PKG_GLOBAL_SDK + ": null");
                    return null;
                }

                AndroidJavaObject optionSetter = jc.CallStatic<AndroidJavaObject>(
                    Values.MTD_GET_OPTION_SETTER_INSTANCE, activity);

                return optionSetter;
            }
        }

#region APIs 
        public void SetChildYN(string childYN)
        {
            if (optionSetter == null)
            {
                Debug.LogError("Android.OptionSetterClient<SetChildYN> OptionSetter: null");
            }

            optionSetter.Call(Values.MTD_SET_CHILD_YN, childYN);
        }

		public void SetUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup)
        {
            // Not Support this Platform
        }

		public void SetEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting)
        {
            // Not Support this Platform
        }
#endregion

        // ToString implementation to be used in Android native
        [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
        public override string ToString()
        {
            return "Gofferwall.Internal.Platform.Android.OptionSetterClient";
        }
    }
}
#endif