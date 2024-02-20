#if UNITY_IOS

using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using AOT;

namespace Gofferwall.Internal.Platform.IOS
{
    /// <summary>
    /// iOS client for gofferwall core
    /// this class will call iOS native plugin's method
    /// </summary>

    using InitAction = Action<bool>;
    internal class CoreClient : ICoreClient
    {
		private static IDictionary<string, InitAction> initHandleMap = new Dictionary<string, InitAction>();
		private static string keyOfInitialize = null;

		public static CoreClient Instance;

		public CoreClient()
		{
			Instance = this;
		}

		private delegate void onInitializedCallback(bool isSuccess);

        [MonoPInvokeCallback(typeof(onInitializedCallback))]
        private static void OnInitializdCallback(bool isSuccess)
        {
            if (keyOfInitialize == null) { return; }

            InitAction callback;
            if (false == initHandleMap.TryGetValue(keyOfInitialize, out callback)) { return; }

            callback.Invoke(isSuccess);
        }

		[DllImport("__Internal")]
		private static extern void unityInitialize(string media_id, string app_id, onInitializedCallback callback);
		public void Initialize(string mediaId, string mediaSecret, Action<bool> callback)
        {
            Debug.Log("Initialize medai");
            if (callback != null) 
            { 
                string key = Guid.NewGuid().ToString();
                keyOfInitialize = key;
                initHandleMap.Add(key, callback); 
            }

			unityInitialize(mediaId, mediaSecret, OnInitializdCallback);
		}

		[DllImport("__Internal")]
		private static extern void unityInitializePlist(onInitializedCallback callback);
        public void Initialize(Action<bool> callback)
        {
            Debug.Log("Initialize");
            if (callback != null) 
            { 
                var key = Guid.NewGuid().ToString();
                keyOfInitialize = key;
                initHandleMap.Add(key, callback); 
            }

			unityInitializePlist(OnInitializdCallback);
        }

        [DllImport ("__Internal")]
		private static extern bool isInitialized();
		public bool IsInitialized() { return isInitialized(); }

		[DllImport ("__Internal")]
		private static extern bool setUserId(string user_id);        
        public void SetUserId(string user_id) 
        {
            if (!setUserId(user_id))
                throw new System.ArgumentException();
        }
    }
}

#endif