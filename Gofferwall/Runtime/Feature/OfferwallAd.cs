using System.Collections.Generic;
using Gofferwall.Internal.Interface;
using Gofferwall.Internal.Platform;
using Gofferwall.Model;
using System;

namespace Gofferwall.Feature
{
    public class OfferwallAd
    {
        public event EventHandler<ShowResult> OnOpened;
        public event EventHandler<ShowResult> OnOpenedBackground;

        public event EventHandler<ShowFailure> OnFailedToShow;
        public event EventHandler<ShowFailure> OnFailedToShowBackground;

        private IOfferwallAdClient client;

        private static class ClassWrapper { public static readonly OfferwallAd instance = new OfferwallAd(); }
        public static OfferwallAd Instance { get { return ClassWrapper.instance; } }

        private OfferwallAd()
        {
            this.client = ClientBuilder.BuildOfferwallAdClient();

            this.client.OnOpened += (sender, args) => { OnOpened?.Invoke(sender, args); };
            this.client.OnOpenedBackground += (sender, args) => { OnOpenedBackground?.Invoke(sender, args); };

            this.client.OnFailedToShowBackground += (sender, args) => { OnFailedToShowBackground?.Invoke(sender, args); };
            this.client.OnFailedToShow += (sender, args) => { OnFailedToShow?.Invoke(sender, args); };
        }

        public bool Show(string unitId)
        {
            return client.Show(unitId);
        }

        public bool Show4TNK(string unitId)
        {
            return client.Show4TNK(unitId);
        }

        public bool Show4Tapjoy(string unitId)
        {
            return client.Show4Tapjoy(unitId);
        }

        public bool SetColorOfferwall4TNK(float red, float green, float blue, float alpha)
        {
            return client.SetColorOfferwall4TNK(red, green, blue, alpha);
        }

        public bool SetPointIconOfferwall4TNK(string imageName)
        {
            return client.SetPointIconOfferwall4TNK(imageName);
        }
    }
}
