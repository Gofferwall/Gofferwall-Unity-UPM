//
//  GofferwallError.h
//  Gofferwall
//
//  Created by 심경보 on 12/14/23.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, GofferwallErrorCode) {
    INTERNAL_ERROR          = 0,
    MEDIATION_ERROR         = 1,
    INITIALIZE_ERROR        = 2,
    SERVER_SETTING_ERROR    = 3,
    INVALID_REQUEST         = 4,
    NETWORK_ERROR           = 5,
    USER_SETTING_ERROR      = 6,
    NOT_EXIST_IDFA          = 7
};

@interface GofferwallError : NSError

+ (GofferwallError *)errorCode:(GofferwallErrorCode)code;

@end
