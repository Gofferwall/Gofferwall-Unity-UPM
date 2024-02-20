//
//  GofferwallInterface+Offerwall.h
//  Gofferwall
//
//  Created by 심경보 on 12/15/23.
//

#import "Gofferwall.h"

NS_ASSUME_NONNULL_BEGIN

@interface GofferwallInterface (Offerwall)

- (BOOL)showOfferwall:(NSString *)unitID delegate:(id)delegate;
- (BOOL)showOfferwall4TNK:(NSString *)unitID delegate:(id)delegate;
- (BOOL)showOfferwall4Tapjoy:(NSString *)unitID delegate:(id)delegate;

@end

NS_ASSUME_NONNULL_END
