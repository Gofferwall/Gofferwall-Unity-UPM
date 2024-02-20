using Gofferwall.Model;
using System;

namespace Gofferwall.Internal.Interface
{
    /// <summary>
    /// interface for OptionSetter client
    /// </summary>
    internal interface IOptionGetterClient
    {
        string GetNetworkVersions();
        string GetNetworkSDKVersion();
        string GetUnitySDKVersion();
        string GetSDKVersion();
    }
}
