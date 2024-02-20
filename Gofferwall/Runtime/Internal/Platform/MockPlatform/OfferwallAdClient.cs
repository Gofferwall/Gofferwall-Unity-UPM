#if (UNITY_EDITOR) || (!UNITY_ANDROID)
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using System.Threading;
using Gofferwall.Feature;

namespace Gofferwall.Internal.Platform.MockPlatform
{
    /// <summary>
    /// mockup client for offerwall ad
    /// this class will emulate callback very simply, limitedly
    /// </summary>
    internal class OfferwallAdClient : IOfferwallAdClient
    {
        public event EventHandler<ShowResult> OnOpened;
        public event EventHandler<ShowFailure> OnFailedToShow;

        public event EventHandler<ShowResult> OnOpenedBackground;
        public event EventHandler<ShowFailure> OnFailedToShowBackground;

        private string unitId;
        private string itemId;
        private string url;
        private bool showing;

        public OfferwallAdClient()
        {
        }

        #region AD APIs 
        public bool Show(string unitId)
        {
            if (this.showing)
            {
                return false;
            }

            this.showing = true;

            this.unitId = unitId;
#if (UNITY_EDITOR)
            new Thread(() => DelayedCallback(onOfferwallAdOpened, 100)).Start();
#else
            new Thread(() => DelayedCallback(onOfferwallAdFailedToShow, 5)).Start();
#endif
            return true;
        }

        public bool Show4TNK(string unitId)
        {
            if (this.showing)
            {
                return false;
            }

            this.showing = true;

            this.unitId = unitId;
#if (UNITY_EDITOR)
            new Thread(() => DelayedCallback(onOfferwallAdOpened, 100)).Start();
#else
            new Thread(() => DelayedCallback(onOfferwallAdFailedToShow, 5)).Start();
#endif
            return true;
        }

        public bool Show4Tapjoy(string unitId)
        {
            if (this.showing)
            {
                return false;
            }

            this.showing = true;

            this.unitId = unitId;
#if (UNITY_EDITOR)
            new Thread(() => DelayedCallback(onOfferwallAdOpened, 100)).Start();
#else
            new Thread(() => DelayedCallback(onOfferwallAdFailedToShow, 5)).Start();
#endif
            return true;
        }
        #endregion

        static void DelayedCallback(Action action, int delay)
        {
            Thread.Sleep(delay);
            action.Invoke();
        }

        #region Callbacks
        public void onOfferwallAdOpened()
        {
            if (this.OnOpened != null)
            {
                UnityThread.executeInMainThread(() =>
                {
                    this.OnOpened(this, new ShowResult(this.unitId));
                });
            }

            if (this.OnOpenedBackground != null)
            {
                this.OnOpenedBackground(this, new ShowResult(this.unitId));
            }
        }

        public void onOfferwallAdFailedToShow()
        {
            GofferwallError error = new GofferwallError(-1, "Gofferwall only supports following platforms: Android");

            if (this.OnFailedToShow != null)
            {
                UnityThread.executeInMainThread(() =>
                {
                    this.OnFailedToShow(this, new ShowFailure(this.unitId, error));
                });
            }

            if (this.OnFailedToShowBackground != null)
            {
                this.OnFailedToShowBackground(this, new ShowFailure(this.unitId, error));
            }
        }
        #endregion
    }
}
#endif