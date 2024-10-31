using Gofferwall.Model;
using System;
using Gofferwall.Feature;

namespace Gofferwall.Internal.Interface
{
    /// <summary>
    /// interface for OfferwallAd client
    /// </summary>
    internal interface IOfferwallAdClient
    {
        event EventHandler<ShowResult> OnOpened;
        event EventHandler<ShowFailure> OnFailedToShow;

        event EventHandler<ShowResult> OnOpenedBackground;
        event EventHandler<ShowFailure> OnFailedToShowBackground;

        bool Show(string unitId);
        bool Show4TNK(string unitId);
        bool Show4Tapjoy(string unitId);
        
        bool SetColorOfferwall4TNK(float red, float green, float blue, float alpha);
        bool SetPointIconOfferwall4TNK(string imageName);
    }
}
