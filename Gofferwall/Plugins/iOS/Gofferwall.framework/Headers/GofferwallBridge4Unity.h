//
//  GofferwallBridge4Unity.h
//  Gofferwall
//
//  Created by 심경보 on 12/15/23.
//

#import "GofferwallInterface.h"
#import "Gofferwall.h"

#ifdef __cplusplus
extern "C" {
#endif
    
#pragma mark - Gofferwall
    typedef void (*DelegateOnInitializdCallback)(BOOL isSuccess);
    void unityInitialize(const char * mediaId, const char * mediaSecret, DelegateOnInitializdCallback callback);
    void unityInitializePlist(DelegateOnInitializdCallback callback);

    BOOL isInitialized();

    BOOL setUserId(const char * userId);
    
    const char * getSDKVersion();
    const char * getUnitySDKVersion();
    const char * getNetworkVersions();
    void setUseAppTrackingTransparencyPopup(BOOL useAppTrackingTransparencyPopup);
    void setEnabledForcedOpenApplicationSetting(BOOL enabledForcedOpenApplicationSetting);

    // Offerwall
    typedef void (*DelegateOnOfferwallAdOpenedCallback)(const char * unitId);
    typedef void (*DelegateOnOfferwallAdFailedToShowCallback)(const char * unitId, int code, const char * description);
    BOOL showOfferwall(const char * unitId, DelegateOnOfferwallAdOpenedCallback openedCallback, DelegateOnOfferwallAdFailedToShowCallback failedToShowCallback);
    BOOL showOfferwall4TNK(const char * unitId, DelegateOnOfferwallAdOpenedCallback openedCallback, DelegateOnOfferwallAdFailedToShowCallback failedToShowCallback);
    BOOL showOfferwall4Tapjoy(const char * unitId, DelegateOnOfferwallAdOpenedCallback openedCallback, DelegateOnOfferwallAdFailedToShowCallback failedToShowCallback);

#ifdef __cplusplus
}
#endif
