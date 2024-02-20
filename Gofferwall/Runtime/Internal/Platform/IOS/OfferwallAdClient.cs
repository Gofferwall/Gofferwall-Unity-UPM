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
	/// iOS client for offerwall ad
	/// this class will call iOS native plugin's method
	/// </summary>		
	public class OfferwallAdClient : IOfferwallAdClient
	{
		public event EventHandler<ShowResult> OnOpened;
		public event EventHandler<ShowFailure> OnFailedToShow;

		public event EventHandler<ShowResult> OnOpenedBackground;
		public event EventHandler<ShowFailure> OnFailedToShowBackground;

		public static OfferwallAdClient Instance;

		public OfferwallAdClient ()
		{
			Instance = this;
		}

		#region AD APIs 

		[DllImport ("__Internal")]
		private static extern bool showOfferwall(string unitId, onOfferwallAdOpenedCallback openedCallback, onOfferwallAdFailedToShowCallback failedToShowCallback);
		public bool Show(string unitId)
		{
			return showOfferwall (unitId, onOfferwallAdOpened, onOfferwallAdFailedToShow);
		}

		[DllImport ("__Internal")]
		private static extern bool showOfferwall4TNK(string unitId, onOfferwallAdOpenedCallback openedCallback, onOfferwallAdFailedToShowCallback failedToShowCallback);
		public bool Show4TNK(string unitId)
		{
			return showOfferwall4TNK (unitId, onOfferwallAdOpened, onOfferwallAdFailedToShow);
		}

		[DllImport ("__Internal")]
		private static extern bool showOfferwall4Tapjoy(string unitId, onOfferwallAdOpenedCallback openedCallback, onOfferwallAdFailedToShowCallback failedToShowCallback);
		public bool Show4Tapjoy(string unitId)
		{
			return showOfferwall4Tapjoy (unitId, onOfferwallAdOpened, onOfferwallAdFailedToShow);
		}

		#endregion

		#region Callbacks

		private delegate void onOfferwallAdOpenedCallback(string unitId);
		[MonoPInvokeCallback(typeof(onOfferwallAdOpenedCallback))] 
		public static void onOfferwallAdOpened(string unitId)
		{
			Debug.Log("onOfferwallAdOpened() unitId = " + unitId);
			Instance.OfferwallAdOpenedProc (unitId);
		}

		private void OfferwallAdOpenedProc(string unitId)
		{
			if (Instance.OnOpened != null)
			{
				UnityThread.executeInMainThread(() =>
				{
					Instance.OnOpened(Instance, new ShowResult(unitId));
				});
			}

			if (Instance.OnOpenedBackground != null)
			{
				Instance.OnOpenedBackground(Instance, new ShowResult(unitId));
			}
		}
			
		private delegate void onOfferwallAdFailedToShowCallback(string unitId, int code, string description);
		[MonoPInvokeCallback(typeof(onOfferwallAdFailedToShowCallback))]
		public static void onOfferwallAdFailedToShow(string unitId, int code, string description)
		{
			Debug.Log("onOfferwallAdFailedToShow() unitId = " + unitId + " code = " + code + " description = " + description);
			Instance.OfferwallAdFailedToShowProc (unitId, code, description);
		}

		private void OfferwallAdFailedToShowProc(string unitId, int code, string description)
		{
			if (Instance.OnFailedToShow != null)
			{
				UnityThread.executeInMainThread(() =>
				{
					Instance.OnFailedToShow(
						Instance, new ShowFailure(unitId, new GofferwallError(code, description)));
				});
			}

			if (Instance.OnFailedToShowBackground != null)
			{
				Instance.OnFailedToShowBackground (
					Instance, new ShowFailure(unitId, new GofferwallError (code, description)));
			}
		}
        #endregion
    }
}

#endif