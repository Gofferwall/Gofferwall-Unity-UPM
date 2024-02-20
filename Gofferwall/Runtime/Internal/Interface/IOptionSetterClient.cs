using Gofferwall.Model;
using System;

namespace Gofferwall.Internal.Interface
{
    /// <summary>
    /// interface for OptionSetter client
    /// </summary>
    internal interface IOptionSetterClient
    {
        // Only Android
        void SetChildYN(string childYN);

        // Only iOS
        void SetUseAppTrackingTransparencyPopup(bool useAppTrackingTransparencyPopup);
        void SetEnabledForcedOpenApplicationSetting(bool enabledForcedOpenApplicationSetting);
    }
}
