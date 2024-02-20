#if UNITY_ANDROID

using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

using System.Runtime.InteropServices;
using AOT;

namespace Gofferwall.Internal.Platform.Android
{
	internal class OptionGetterClient : IOptionGetterClient
	{
		private AndroidJavaObject activity;

		public OptionGetterClient() {
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
            {
                if (unityPlayer == null)
                {
                    Debug.LogError("Android.OptionGetterClient<Constructor> UnityPlayer: null");
                    return;
                }

                this.activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
            }
		}

		#region APIs 

		public string GetNetworkVersions()
		{
			using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
			{
				if (jc == null)
				{
					Debug.LogError("Android.OptionGetterClient<GetNetworkVersions> " +
						Values.PKG_GLOBAL_SDK + ": null");
					return "";
				}
				return jc.CallStatic<string>(Values.MTD_GET_NETWORK_VERSION);
			}
		}

		public string GetNetworkSDKVersion()
		{
			using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
			{
				if (jc == null)
				{
					Debug.LogError("Android.OptionGetterClient<GetNetworkSDKVersion> " +
						Values.PKG_GLOBAL_SDK + ": null");
					return "";
				}
				return jc.CallStatic<string>(Values.MTD_GET_NETWORK_SDK_VERSION);
			}
		}

		private static string getSDKVersion() 
		{
			using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
			{
				if (jc == null)
				{
					Debug.LogError("Android.OptionGetterClient<getSDKVersion> " +
						Values.PKG_GLOBAL_SDK + ": null");
					return "";
				}
				return jc.CallStatic<string>(Values.MTD_GET_SDK_VERSION);
			}
		}

		public string GetSDKVersion() 
		{
			return getSDKVersion();
		}

		public string GetUnitySDKVersion() 
		{
			using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
			{
				if (this.activity == null)
                {
                    Debug.LogError("Android.OptionGetterClient<GetUnitySDKVersion> UnityPlayerActivity: null");
                    return "";
                }
				
				if (jc == null)
				{
					Debug.LogError("Android.OptionGetterClient<GetUnitySDKVersion> " +
						Values.PKG_GLOBAL_SDK + ": null");
					return "";
				}
				return jc.CallStatic<string>(Values.MTD_GET_UNITY_SDK_VERSION, this.activity);
			}
		}

		#endregion
	}
}

#endif