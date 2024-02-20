#if UNITY_ANDROID
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using UnityEngine;

namespace Gofferwall.Internal.Platform.Android
{
    /// <summary>
    /// android client for gofferwall core
    /// this class will call android native plugin's method
    /// </summary>
    internal class CoreClient : ICoreClient
    {
        private AndroidJavaObject activity;
             
        public CoreClient()
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass(Values.PKG_UNITY_PLAYER))
            {
                if (unityPlayer == null)
                {
                    Debug.LogError("Android.CoreClient<Constructor> UnityPlayer: null");
                    return;
                }

                this.activity = unityPlayer.GetStatic<AndroidJavaObject>(Values.MTD_CURRENT_ACTIVITY);
            }
        }

        public void Initialize(Action<bool> callback)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
            {
                if (this.activity == null)
                {
                    Debug.LogError("Android.CoreClient<Initialize> UnityPlayerActivity: null");
                    return;
                }

                if (jc == null)
                {
                    Debug.LogError("Android.CoreClient<Initialize> " + Values.PKG_GLOBAL_SDK + ": null");
                    return;
                }

                GofferwallInitializeListener listener = new GofferwallInitializeListener(callback);
                jc.CallStatic(Values.MTD_INITIALIZE, this.activity, listener);
            }
        }

        public void Initialize(string mediaId, string mediaSecret, Action<bool> callback)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
            {
                if (this.activity == null)
                {
                    Debug.LogError("Android.CoreClient<Initialize> UnityPlayerActivity: null");
                    return;
                }

                if (jc == null)
                {
                    Debug.LogError("Android.CoreClient<Initialize> " +
                        Values.PKG_GLOBAL_SDK + ": null");
                    return;
                }

                GofferwallInitializeListener listener = new GofferwallInitializeListener(callback);

                int mediaIdNumber;

                if (Int32.TryParse(mediaId, out mediaIdNumber))
                {
                    jc.CallStatic(Values.MTD_INITIALIZE, this.activity, mediaIdNumber, mediaSecret, listener);
                } else
                {
                    Debug.LogError("Android.CoreClient<Initialize> " +
                        Values.PKG_GLOBAL_SDK + ": mediaId is must be number");
                    return;
                }
            }
        }

        public void SetUserId(string userId)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
            {
                if (jc == null)
                {
                    Debug.LogError("Android.CoreClient<SetUserId> " + Values.PKG_GLOBAL_SDK + ": null");
                    return;
                }

                jc.CallStatic<bool>(Values.MTD_SET_USER_ID, userId);
            }
        }

        public bool IsInitialized()
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(Values.PKG_GLOBAL_SDK))
            {
                if (jc == null)
                {
                    Debug.LogError("Android.CoreClient<IsInitialized> " + Values.PKG_GLOBAL_SDK + ": null");
                }
                return jc.CallStatic<bool>(Values.MTD_ISINITIALIZE);
            }
            
        }
    }
}
#endif