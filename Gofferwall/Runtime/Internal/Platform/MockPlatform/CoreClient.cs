#if (UNITY_EDITOR) || (!UNITY_ANDROID)
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using System.Threading;

namespace Gofferwall.Internal.Platform.MockPlatform
{
    /// <summary>
    /// mockup client for gofferwall core
    /// this class will do nothing
    /// </summary>
    internal class CoreClient : ICoreClient
    {
        public event EventHandler<InitResult> OnInitialized;
        public event EventHandler<InitResult> OnInitializedBackground;

        public static CoreClient Instance;

        public CoreClient()
        {
            Instance = this;
        }

        private void DelayedCallback(Action action, int delay)
        {
            Thread.Sleep(delay);
            action.Invoke();
        }

        public void Initialize(string media_id, string app_id, Action<bool> callback)
        {
            new Thread(() => DelayedCallback(
                () => {

                    if (OnInitialized != null)
                    {
                        UnityThread.executeInMainThread(() =>
                        {
                            OnInitialized(this, new InitResult(true));
                        });
                    }

                    if (OnInitializedBackground != null)
                    {
                        OnInitializedBackground(this, new InitResult(true));
                    }

                }, 10)).Start();
        }

        public bool IsInitialized()
        {
            return true;
        }

        public void SetUserId(string userId)
        {
        }

        public void Initialize(Action<bool> callback)
        {
            new Thread(() => DelayedCallback(() =>
            {

                if (OnInitialized != null)
                {
                    UnityThread.executeInMainThread(() =>
                    {
                        OnInitialized(this, new InitResult(true));
                    });
                }

                if (OnInitializedBackground != null)
                {
                    OnInitializedBackground(this, new InitResult(true));
                }

            }, 10)).Start();
        }
    }
}
#endif