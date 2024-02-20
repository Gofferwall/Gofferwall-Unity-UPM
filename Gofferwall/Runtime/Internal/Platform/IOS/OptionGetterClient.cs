#if UNITY_IOS

using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

using System.Runtime.InteropServices;
using AOT;

namespace Gofferwall.Internal.Platform.IOS {
	internal class OptionGetterClient : IOptionGetterClient {

		public OptionGetterClient () { }

#region APIs 

		[DllImport ("__Internal")]
		private static extern string getNetworkVersions();
		public string GetNetworkVersions() {
			return "";
		}

		[DllImport ("__Internal")]
		private static extern string getNetworkSDKVersion();
		public string GetNetworkSDKVersion() {
			return "";
		}

		[DllImport ("__Internal")]
		private static extern string getSDKVersion();
		public string GetSDKVersion() {
			return getSDKVersion();
		}

		[DllImport ("__Internal")]
		private static extern string getUnitySDKVersion();
		public string GetUnitySDKVersion() {
			return getUnitySDKVersion();
		}

#endregion

	}
}

#endif