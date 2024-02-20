#if UNITY_ANDROID
using Gofferwall.Feature;
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    /// <summary>
    /// android client for gofferwall ad
    /// this class will call android native plugin's method
    /// </summary>
    internal class OfferwallAdClient : IOfferwallAdClient
    {
        public event EventHandler<ShowResult> OnOpened;
        public event EventHandler<ShowFailure> OnFailedToShow;

        public event EventHandler<ShowResult> OnOpenedBackground;
        public event EventHandler<ShowFailure> OnFailedToShowBackground;

        public OfferwallListener listener;

        public OfferwallAdClient()
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
            {
                if (unityPlayer == null)
                {
                    Debug.LogError("Android.OfferwallAdClient<Constructor> UnityPlayer: null");
                    return;
                }
            }
        }

        private void GetOfferwallListener()
        {
            if(listener == null)
            {
                Action<string> onOpenedCallback = (unitId) =>
                {
                    onOfferwallAdOpened(unitId);
                };

                Action<string, GofferwallError> onFailedToOpenCallback = (unitId, error) =>
                {
                    onOfferwallAdFailedToShow(unitId, error);
                };

                listener = new OfferwallListener(onOpenedCallback, onFailedToOpenCallback);
            }
        }

        #region AD APIs 
        public bool Show(string unitId)
        {
            AndroidJavaObject activity = null;
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_OFFERWALL))
            {
                if(jc == null)
                {
                    Debug.LogError("Android.OfferwallAdClient<Show> " +
                        Values.PKG_GLOBAL_OFFERWALL + ": null");
                    return false;
                }

                using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
                {
                    if (unityPlayer == null)
                    {
                        Debug.LogError("Android.OfferwallAdClient<Show> UnityPlayer: null");
                        return false;
                    }
                    activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
                }

                if(listener == null)
                {
                    GetOfferwallListener();
                }

                jc.CallStatic(Values.MTD_SHOW, unitId, activity, listener);
                return true;
            }
        }

        public bool Show4TNK(string unitId)
        {
            AndroidJavaObject activity = null;
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_OFFERWALL))
            {
                if(jc == null)
                {
                    Debug.LogError("Android.OfferwallAdClient<Show4TNK> " +
                        Values.PKG_GLOBAL_OFFERWALL + ": null");
                    return false;
                }

                using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
                {
                    if (unityPlayer == null)
                    {
                        Debug.LogError("Android.OfferwallAdClient<Show4TNK> UnityPlayer: null");
                        return false;
                    }
                    activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
                }

                if(listener == null)
                {
                    GetOfferwallListener();
                }

                jc.CallStatic(Values.MTD_SHOW, unitId, "TNK", activity, listener);
                return true;
            }
        }

        public bool Show4Tapjoy(string unitId)
        {
            AndroidJavaObject activity = null;
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_OFFERWALL))
            {
                if(jc == null)
                {
                    Debug.LogError("Android.OfferwallAdClient<Show4Tapjoy> " +
                        Values.PKG_GLOBAL_OFFERWALL + ": null");
                    return false;
                }

                using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
                {
                    if (unityPlayer == null)
                    {
                        Debug.LogError("Android.OfferwallAdClient<Show4Tapjoy> UnityPlayer: null");
                        return false;
                    }
                    activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
                }

                if(listener == null)
                {
                    GetOfferwallListener();
                }

                jc.CallStatic(Values.MTD_SHOW, unitId, "Tapjoy", activity, listener);
                return true;
            }
        }

        #endregion

        #region Callbacks
        public void onOfferwallAdOpened(string unitId)
        {
            if (this.OnOpened != null)
            {
                UnityThread.executeInMainThread(() =>
                {
                    this.OnOpened(this, new ShowResult(unitId));
                });
            }

            if (this.OnOpenedBackground != null)
            {
                this.OnOpenedBackground(this, new ShowResult(unitId));
            }
        }

        public void onOfferwallAdFailedToShow(string unitId, GofferwallError error)
        {
            if (this.OnFailedToShow != null)
            {
                UnityThread.executeInMainThread(() =>
                {
                    this.OnFailedToShow(
                        this, new ShowFailure(unitId, error));
                });
            }

            if (this.OnFailedToShowBackground != null)
            {
                this.OnFailedToShowBackground(
                    this, new ShowFailure(unitId, error));
            }
        }
        #endregion

        // ToString implementation to be used in Android native
        [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
        public override string ToString()
        {
            return "Gofferwall.Internal.Platform.Android.OfferwallAdClient as " +
                Values.PKG_GLOBAL_OFFERWALL_LISTENER;
        }
    }
}
#endif