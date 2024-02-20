#if (UNITY_EDITOR) || (!UNITY_ANDROID)
using Gofferwall.Internal.Interface;
using Gofferwall.Model;
using System;
using System.Threading;

namespace Gofferwall.Internal.Platform.MockPlatform
{
    /// <summary>
    /// mockup client for option setter
    /// this class will emulate callback very simply, limitedly
    /// </summary>
    internal class OptionSetterClient : IOptionSetterClient
    {

        public OptionSetterClient()
        {
        }

        #region APIs 
        public void SetChildYN(string childYN)
        {
        }

        public void SetUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup)
        {
        }

        public void SetEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting)
        {
        }
        #endregion
    }
}
#endif