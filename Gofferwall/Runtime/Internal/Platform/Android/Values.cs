#if UNITY_ANDROID
namespace Gofferwall.Internal.Platform.Android
{
    /// <summary>
    /// class for constant values
    /// </summary>
    internal class Values
    {
        // gofferwall
        public const string PKG_GLOBAL_SDK = "com.adiscope.global.sdk.AdiscopeGlobalSdk";
        public const string PKG_GLOBAL_SDK_INITIALIZATION_LISTENER = "com.adiscope.global.AdiscopeGlobalInitializationListener";
        public const string PKG_GLOBAL_OFFERWALL = "com.adiscope.global.sdk.AdiscopeGlobalOfferwall";
        public const string PKG_GLOBAL_OFFERWALL_LISTENER = "com.adiscope.global.OfferwallListener";

        public const string MTD_GET_OPTION_SETTER_INSTANCE = "getOptionSetterInstance";
        public const string MTD_GET_NETWORK_VERSION = "getNetworkVersion";
        public const string MTD_GET_NETWORK_SDK_VERSION = "getNetworkSdkVersion";
        public const string MTD_GET_SDK_VERSION = "getSdkVersion";
        public const string MTD_GET_UNITY_SDK_VERSION = "getUnitySdkVersion";
        public const string MTD_INITIALIZE = "initialize";
        public const string MTD_ISINITIALIZE = "isInitialized";
        public const string MTD_SET_USER_ID = "setUserId";
        public const string MTD_SHOW = "show";
        public const string MTD_GET_CODE = "getCode";
        public const string MTD_GET_MESSAGE = "getMessage";
        public const string MTD_GET_XB3TRACEID = "getXb3TraceId";
        public const string MTD_GET_TYPE = "getType";
        public const string MTD_GET_AMOUNT = "getAmount";
        public const string MTD_IS_LIVE = "isLive";
        public const string MTD_IS_ACTIVE = "isActive";
        public const string MTD_SET_USE_CLOUD_FRONT_PROXY = "setUseCloudFrontProxy";
        public const string MTD_SET_CHILD_YN = "setChildYN";

        public const string MTD_SHOW_DEBUG = "showDebug";

        // unity
        public const string PKG_UNITY_PLAYER = "com.unity3d.player.UnityPlayer";

        public const string MTD_CURRENT_ACTIVITY = "currentActivity";
    }
}
#endif