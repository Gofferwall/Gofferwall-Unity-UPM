// Copyright (C) 2014 - 2018 by Tapjoy Inc.
//
// This file is part of the Tapjoy SDK.
//
// By using the Tapjoy SDK in your software, you agree to the terms of the Tapjoy SDK License Agreement.
//
// The Tapjoy SDK is bound by the Tapjoy SDK License Agreement and can be found here: https://www.tapjoy.com/sdk/license

//The Tapjoy iOS SDK.

#ifndef _TAPJOY_H
#define _TAPJOY_H

#import <UIKit/UIKit.h>
#import <Tapjoy/TapjoyConnectConstants.h>
#import <Tapjoy/TJPlacement.h>
#import <Tapjoy/TJPrivacyPolicy.h>
#import <Tapjoy/TJOfferwallDiscoverView.h>
#import <Tapjoy/TapjoyPluginAPI.h>
#import <Tapjoy/TJSegment.h>
#import <Tapjoy/TJEntryPoint.h>
#import <Tapjoy/TJSdkStatus.h>


#define TJC_DEPRECATION_WARNING(VERSION) __attribute__((deprecated("Go to dev.tapjoy.com for instructions on how to fix this warning")))
#define TJC_MINIMUM_SUPPORTED_SYSTEM_VERISON	@"10.0"

NS_ASSUME_NONNULL_BEGIN

typedef void (^currencyCompletion)(NSDictionary * _Nullable parameters, NSError * _Nullable error);
typedef void (^networkCompletion)(BOOL success, NSError * _Nullable error);


@class TJPlacement;

/**	
 * The Tapjoy Connect Main class. This class provides all publicly available methods for developers to integrate Tapjoy into their applications. 
 */
@interface Tapjoy :  NSObject

/** The application SDK key unique to this app. */
@property (nullable, nonatomic, copy) NSString *sdkKey;

/** The application ID unique to this app. */
@property (nullable, nonatomic, copy) NSString *appID;

/** The Tapjoy secret key for this applicaiton. */
@property (nullable, nonatomic, copy) NSString *secretKey;

/** The user ID, a custom ID set by the developer of an app to keep track of its unique users. */
@property (nullable, nonatomic, readonly) NSString *userID TJC_DEPRECATION_WARNING(14.0.0);

/** The name of the plugin used. If no plugin is used, this value is set to "native" by default. */
@property (nullable, nonatomic, copy) NSString *plugin;

/** Indicates Tapjoy SDK's connect status*/
@property (nonatomic, assign, readonly) TJSdkStatus status;


@property (nullable, nonatomic, copy) NSString *appGroupID;
@property (nullable, nonatomic, copy) NSString *managedDeviceID;
@property (nullable, nonatomic, copy) NSString *customParameter;




/**
 * This method is called to initialize the Tapjoy system and notify the server that this device is running your application.
 *
 * This method should be called upon app delegate initialization in the applicationDidFinishLaunching method.
 *
 * You can use the notification names TJC_CONNECT_SUCCESS and TJC_CONNECT_FAILED with NSNotificationCenter. This allows you to receive notifications when Tapjoy successfully connects or fails to connect.
 *
 * @param sdkKey The application SDK Key. Retrieved from the app dashboard in your Tapjoy account.
 */
+ (void)connect:(NSString *)sdkKey;

/**
 * This method is called to initialize the Tapjoy system and notify the server that this device is running your application.
 *
 * This method should be called upon app delegate initialization in the applicationDidFinishLaunching method.
 *
 * You can use the notification names TJC_CONNECT_SUCCESS and TJC_CONNECT_FAILED with NSNotificationCenter. This allows you to receive notifications when Tapjoy successfully connects or fails to connect.
 *
 * @param sdkKey The application SDK Key. Retrieved from the app dashboard in your Tapjoy account.
 * @param options NSDictionary of special flags to enable non-standard settings. Valid key:value options:
 *
 * TJC_OPTION_ENABLE_LOGGING : BOOL to enable logging
 *
 * TJC_OPTION_USER_ID : NSString user id that must be set if your currency is not managed by Tapjoy. If you don’t have a user id on launch you can call setUserID later
 *
 * TJC_OPTION_DISABLE_GENERIC_ERROR_ALERT : BOOL to disable our default error dialogs
 *
 */
+ (void)connect:(NSString *)sdkKey options:(nullable NSDictionary *)options;

/**
 * Helper function to check if SDK is initialized
 */
+ (BOOL)isConnected;

/**
 * This method returns URL to Tapjoy support web page. This will use your default currency.
 *
 * @return URL of Tapjoy support web page
 */
+ (nullable NSString *)getSupportURL;

/**
 * This method returns the URL to Tapjoy support web page for specified currency
 * You can get your currencyID from the Tapjoy Dashboard under the currency section.
 *
 * @param currencyID the app's currency id
 *
 * @return URL of Tapjoy support web page for specified currency
 */
+ (nullable NSString *)getSupportURL:(nullable NSString *)currencyID;

/**
 *
 * This method enables/disables the debug mode of the SDK.
 * @param enabled true to enable, false to disable
 */
+ (void)setDebugEnabled:(BOOL)enabled; // default NO

/**
 * This method is called to track the session manually. If this method called, automatic session tracking will be disabled.
 *
 */
+ (void)startSession;
/**
 * This method is called to track the session manually. If this method called, automatic session tracking will be disabled.
 *
 */
+ (void)endSession;

/**
 * Returns the TJPrivacyPolicy instance for calling methods to set GDPR, User's consent, below consent age ,and US Privacy policy flags
 *
 * @return The globally accessible TJPrivacyPolicy singleton object.
 */
+ (TJPrivacyPolicy *)getPrivacyPolicy;

/**
 * Sets the default UIViewController to show a content of the placement having no specific view controller given.
 *
 * @warning This is **experimental** and only applicable to contents of the default placements.
 */
+ (void)setDefaultViewController:(nullable UIViewController *)viewController;

/**
 * This method is called to set the level of the user.
 *
 * @param userLevel
 *        the level of the user
 */
+ (void)setUserLevel:(int)userLevel;

/**
 * This method retrieves user's level
 *
 *@return userLevel
 */
+ (int)getUserLevel;

/**
 * This method sets the maximum level.
 *
 * @param maxLevel
 *        the maximum possible level.
 */
+ (void)setMaxLevel:(int)maxLevel;

/**
 * This method retrieves the maximum possible level
 *
 * @return maxLevel
 */
+ (NSNumber *)getMaxLevel;

/**
 * Returns a string set which contains tags on the user.
 *
 * @return set of string
 */
+ (nullable NSSet *)getUserTags;

/**
 * Sets tags for the user.
 *
 * @param tags the tags to be set
 *             can have up to 200 tags where each tag can have 200 characters
 */
+ (void)setUserTags:(nullable NSSet *)tags;

/**
 * Removes all tags from the user.
 */
+ (void)clearUserTags;

/**
 * Adds the given tag to the user if it is not already present.
 *
 * @param tag the tag to be added
 */
+ (void)addUserTag:(NSString *)tag;

/**
 * Removes the given tag from the user if it is present.
 *
 * @param tag the tag to be removed
 */
+ (void)removeUserTag:(NSString *)tag;

/**
 * This method is called to track the purchase.
 *
 * @param currencyCode the currency code of price as an alphabetic currency code specified in ISO 4217, i.e. "USD", "KRW"
 * @param price the price of product
 */
+ (void)trackPurchaseWithCurrencyCode:(NSString *)currencyCode price:(double)price;

/**
 * Deprecated since 14.0.0
 * This method is called to track the purchase.
 *
 * @param productIdentifier the identifier of product
 * @param currencyCode the currency code of price as an alphabetic currency code specified in ISO 4217, i.e. "USD", "KRW"
 * @param price the price of product
 * @param campaignId the campaign id of the purchase request which initiated this purchase, can be nil
 * @param transactionId the identifier of iap transaction, if this is given, we will check receipt validation. (Available in iOS 7.0 and later)
 */
+ (void)trackPurchase:(nullable NSString *)productIdentifier currencyCode:(nullable NSString *)currencyCode price:(double)price campaignId:(nullable NSString *)campaignId transactionId:(nullable NSString *)transactionId TJC_DEPRECATION_WARNING(14.0.0);

/**
 * Deprecated since 14.1.0
 * Informs the Tapjoy server that the specified Pay-Per-Action was completed. Should be called whenever a user completes an in-game action.
 *
 * @param actionID The action ID of the completed action
 */
+ (void)actionComplete:(NSString *)actionID TJC_DEPRECATION_WARNING(14.1.0);

/**	
 * Retrieves the globally accessible Tapjoy singleton object.
 *
 * @return The globally accessible Tapjoy singleton object.
 */
+ (instancetype)sharedTapjoyConnect;


/** The user ID, a custom ID set by the developer of an app to keep track of its unique users.
 *
 * @return The user ID assigned to this device.
 */
+ (NSString * _Nullable)getUserID;

/**
 * Assigns a user ID for this user/device. This is used to identify the user in your application
 *
 * @param theUserID The user ID you wish to assign to this device.
 * @param completion The completion block that is invoked after a response is received from the server.
 */
+ (void)setUserIDWithCompletion:(nullable NSString*)theUserID completion:(nullable networkCompletion)completion;

/**
 * Assigns a custom parameter associated with any following placement requests that contains an ad type.
 * We will return this value on the currency callback. Only applicable for publishers who manage their own currency servers.
 * This value does NOT get unset with each subsequent placement request.
 *
 * @param customParam The custom parameter to assign to this device
 *
 */
+ (void)setCustomParameter:(nullable NSString *)customParam;


/**
 * Returns the currently set custom parameter.
 *
 * @return the value of the currently set custom parameter
 */
+ (nullable NSString *)getCustomParameter;


/**
 * Toggle logging to the console.
 *
 * @param enable YES to enable logging, NO otherwise.
 */
+ (void)enableLogging:(BOOL)enable;

/**
 * Returns the SDK version.
 *
 * @return The Tapjoy SDK version.
 */
+ (NSString *)getVersion;
/**
 * Sets the segment of the user
 *
 * @param userSegment TJSegment enum 0 (non-payer), 1 (payer), 2 (VIP), -1 (unknown)
 */
+ (void)setUserSegment:(TJSegment)userSegment;

/**
 * Gets the segment of the user
 *
 * @return TJSegment enum 0 (non-payer), 1 (payer), 2 (VIP), -1 (unknown).
 */
+ (TJSegment)getUserSegment;

@end

@interface Tapjoy (TJCCurrencyManager)

/**
 * Requests for virtual currency balance notify via TJC_GET_CURRENCY_RESPONSE_NOTIFICATION notification.
 *
 */
+ (void)getCurrencyBalance;

/**
 * Requests for virtual currency balance information.
 *
 * @param completion The completion block that is invoked after a response is received from the server.
 */
+ (void)getCurrencyBalanceWithCompletion:(nullable currencyCompletion)completion;

/**
 * Updates the virtual currency for the user with the given spent amount of currency.
 *
 * If the spent amount exceeds the current amount of currency the user has, nothing will happen.
 * @param amount The amount of currency to subtract from the current total amount of currency the user has.
 */
+ (void)spendCurrency:(int)amount;

/**
 * Updates the virtual currency for the user with the given spent amount of currency.
 *
 * If the spent amount exceeds the current amount of currency the user has, nothing will happen.
 * @param amount The amount of currency to subtract from the current total amount of currency the user has.
 * @param completion The completion block that is invoked after a response is received from the server.
 */
+ (void)spendCurrency:(int)amount completion:(nullable currencyCompletion)completion;

/**
 * Updates the virtual currency for the user with the given awarded amount of currency.
 *
 * @param amount The amount of currency to add to the current total amount of currency the user has.
 */
+ (void)awardCurrency:(int)amount;

/**
 * Updates the virtual currency for the user with the given awarded amount of currency.
 *
 * @param amount The amount of currency to add to the current total amount of currency the user has.
 * @param completion The completion block that is invoked after a response is received from the server.
 */
+ (void)awardCurrency:(int)amount completion:(nullable currencyCompletion)completion;

/**
 * Shows a UIAlert that tells the user how much currency they just earned.
 *
 */
+ (void)showDefaultEarnedCurrencyAlert;
@end


@protocol TJCTopViewControllerProtocol <NSObject>
@required
@property (nonatomic, assign) UIInterfaceOrientation lockedOrientation;
@property (assign, nonatomic) BOOL canRotate;
@end

#endif


NS_ASSUME_NONNULL_END
