//
//  GofferwallInterface.h
//  Gofferwall
//
//  Created by 심경보 on 12/14/23.
//

#import <Foundation/Foundation.h>
#import "GofferwallDelegate.h"
#import "GofferwallError.h"

// MARK: - GOInterface
@interface GofferwallInterface : NSObject

/// Offerwall을 사용하기 위해서 Singleton Instance에 접근할 수 있습니다.
+ (instancetype)sharedInstance;

/// Offerwall을 사용하기위해 초기화를 진행합니다. MediaID와 해당하는 Secret Key가 필요합니다.
/// 필요한 정보가 존재하지 않는다면 Adiscope에 문의해 주시기 바랍니다.
///
/// @param mediaId 사용하기 위한 Media의 고유한 ID
/// @param mediaSecret MediaID와 매칭되는 SecretKey
/// @param delegate id<GofferwallDelegate>
- (void)initialize:(NSString *)mediaId mediaSecret:(NSString *)mediaSecret delegate:(id)delegate;

/// Offerwall을 사용하기위해 초기화를 진행합니다. MediaID와 해당하는 Secret Key가 필요합니다.
/// 필요한 정보가 존재하지 않는다면 Adiscope에 문의해 주시기 바랍니다.
///
/// @param delegate id<GofferwallDelegate>
- (void)initialize:(id)delegate;

/// Gofferwall initialize 여부를 확인 할 수 있습니다.
- (BOOL)isInitialized;

/// RewardedVideo, Offerwall의 보상을 받기 위한 User의 고유한 식별자입니다.
/// @param userId 보상을 받기 위한 식별자
- (BOOL)setUserId:(NSString *)userId;

/// Gofferwall SDK의 버전정보입니다.
- (NSString *)getSDKVersion;

/// Gofferwall Unity SDK의 버전정보입니다.(Unity가 아닐 시 Gofferwall SDK의 버전정보)
- (NSString *)getUnitySDKVersion;

/// iOS의 ATT 팝업을 띄울지 판단하는 Flag입니다. 기본값은 YES이며 NO일 경우에 Offerwall에 영향이 있을 수 있습니다.
@property (nonatomic) BOOL useAppTrackingTransparencyPopup;

/// ATT Popup을 사용하고 있지 않을때 사용자 설정화면으로 강제로 이동하게 할 것인지 설정하는 Flag입니다.
@property (nonatomic) BOOL enabledForcedOpenApplicationSetting;

// MARK: - Only Use for Unity
- (void)initialize:(NSString *)mediaId MediaSecret:(NSString *)mediaSecret Delegate:(id<GofferwallBridge4UnityDelegate>)delegate;
- (void)initialize4Unity:(id<GofferwallBridge4UnityDelegate>)delegate;

@end
