using Gofferwall.Internal.Interface;
using Gofferwall.Internal.Platform;
using Gofferwall.Internal;
using Gofferwall.Model;
using System;

namespace Gofferwall.Feature
{
    public class Core
    {
        private readonly ICoreClient client;

        private static class ClassWrapper { public static readonly Core instance = new Core(); }

        public static Core Instance { get { return ClassWrapper.instance; } }

        private Core() { this.client = ClientBuilder.BuildCoreClient(); }


        public void Initialize(Action<bool> callback = null)
        {
            this.client.Initialize(callback);
            UnityThread.initUnityThread(true);
        }

        public void Initialize(string mediaId, string mediaSecret, Action<bool> callback = null)
        {
            this.client.Initialize(mediaId, mediaSecret, callback);
            UnityThread.initUnityThread(true);
        }

        public bool IsInitialized() { return this.client.IsInitialized(); }

        public void SetUserId(string userId) { this.client.SetUserId(userId); }
    }
}
