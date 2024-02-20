using Gofferwall.Feature;
using Gofferwall.Internal;
using Gofferwall.Internal.Interface;
using Gofferwall.Internal.Platform;
using Gofferwall.Model;
using System;

namespace Gofferwall
{
    /// <summary>
    /// Gofferwall core class
    /// this class must be used to get rewarded video ad or offerwall ad instance.
    /// </summary>
    public class Sdk
    {
        /// <summary>
        /// prevent manual construction
        /// </summary>
        private Sdk()
        {
        }

        /// <summary>
        /// get Core singleton instance
        /// </summary>
        /// <returns>singleton instance of Gofferwall Core</returns>
        public static Core GetCoreInstance()
        {
            return Core.Instance;
        }

        /// <summary>
        /// get offerwall ad singleton instance
        /// </summary>
        /// <returns>singleton instance of OfferwallAd</returns>
        public static OfferwallAd GetOfferwallAdInstance()
        {
            return OfferwallAd.Instance;
        }

        /// <summary>
        /// get option setter singleton instance
        /// </summary>
        /// <returns>singleton instance of option setter</returns>
        public static OptionSetter GetOptionSetter()
        {
            return OptionSetter.Instance;
        }

        public static OptionGetter GetOptionGetter()
        {
            return OptionGetter.Instance;
        }
    }
}
