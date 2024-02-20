#if UNITY_IOS

using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

using System.Runtime.InteropServices;
using AOT;

namespace Gofferwall.Internal.Platform.IOS
{
	/// <summary>
	/// iOS client for option setter
	/// this class will call iOS native plugin's method
	/// </summary>	
	internal class OptionSetterClient : IOptionSetterClient
	{
		public OptionSetterClient ()
		{
		}

		#region APIs 

		public void SetChildYN(string childYN)
		{
		    // nothing	
		}

		[DllImport("__Internal")]
		private static extern void setUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup);
		public void SetUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup)
        {
			setUseAppTrackingTransparencyPopup(useAppTrackingTransparencyPopup);
		}

		[DllImport("__Internal")]
		private static extern void setEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting);
		public void SetEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting)
        {
			setEnabledForcedOpenApplicationSetting(enabledForcedOpenApplicationSetting);
		}

		#endregion
	}
}

#endif