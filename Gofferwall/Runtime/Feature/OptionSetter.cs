using Gofferwall.Internal.Interface;
using Gofferwall.Internal.Platform;
using System;

namespace Gofferwall.Feature
{
    /// <summary>
    /// OptionSetter singleton instance class
    /// </summary>
    public class OptionSetter
    {
        private IOptionSetterClient client;

        private static class InitializationOnDemandHolderIdiom
        {
            public static readonly OptionSetter SingletonInstance = new OptionSetter();
        }

        public static OptionSetter Instance
        {
            get
            {
                return InitializationOnDemandHolderIdiom.SingletonInstance;
            }
        }

        private OptionSetter()
        {
            this.client = ClientBuilder.BuildOptionSetterClient();

        }

        /// <summary>
        /// Set whether user is child, Only Using for Android.
        /// </summary>
        /// <param name="childYN">value whether user is child (This value need for Google Family Policy)</param>
        public void SetChildYN(string childYN)
        {
            client.SetChildYN(childYN);
        }

        /// <summary>
        /// Setup the ATT popup Flag in Gofferwall. Only Using for iOS.
        /// </summary>
        /// <param name="useAppTrackingTransparencyPopup">if the turn on this flag, Using popup on will start an Initialize. default flag is true</param>
        public void SetUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup)
        {
            client.SetUseAppTrackingTransparencyPopup(useAppTrackingTransparencyPopup);
        }

        /// <summary>
        /// Setup the Flag: use Jump to Gofferwall in Setting App. Only Using for iOS.
        /// </summary>
        /// <param name="enabledForcedOpenApplicationSetting">if the turn on this flag, Showing "OK" Button Message is change to "Move up".. default flag is true</param>
        public void SetEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting)
        {
            client.SetUseAppTrackingTransparencyPopup(enabledForcedOpenApplicationSetting);
        }
    }
}