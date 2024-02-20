//
//  GofferwallDelegate.h
//  Gofferwall
//
//  Created by 심경보 on 12/14/23.
//

#ifndef GofferwallDelegate_h
#define GofferwallDelegate_h

#import <UIKit/UIKit.h>

@class GofferwallError;

// MARK: - AdiscopeBridge for Unity Delegate
@protocol GofferwallBridge4UnityDelegate <NSObject>

- (void)onInitializedCallback:(BOOL)isSuccess;

- (void)onOfferwallAdOpenedCallback:(const char *)unitId;
- (void)onOfferwallAdFailedToShowCallback:(const char *)unitId code:(int)code description:(const char *)description;

@end

// MARK: - GofferwallDelegate
@protocol GofferwallDelegate
@optional

- (void)onInitialized:(BOOL)isSuccess;

- (void)onOfferwallAdOpened:(NSString *)unitID;
- (void)onOfferwallAdFailedToShow:(NSString *)unitID Error:(GofferwallError *)error;

@end

#endif /* GofferwallDelegate_h */
