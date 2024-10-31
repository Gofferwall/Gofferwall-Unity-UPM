# Other API
## Other API
### SetColorOfferwall4TNK (only iOS)
```csharp
Gofferwall.Feature.OfferwallAd offerwallAd = Gofferwall.Sdk.GetOfferwallAdInstance();
offerwallAd.SetColorOfferwall4TNK({0~255}/255, {0~255}/255, {0~255}/255, {0~1});
```

### SetPointIconOfferwall4TNK
```csharp
Gofferwall.Feature.OfferwallAd offerwallAd = Gofferwall.Sdk.GetOfferwallAdInstance();
offerwallAd.SetPointIconOfferwall4TNK("{이미지명}")
```
- **Android**: `Assets/Gofferwall/Plugins/Gofferwall.androidlib` 경로에 `res/drawable` 폴더를 만들어 `.png` or `.jpg` 파일 추가 후 해당 이미지명으로 호출
- **iOS**: Assets 폴더내 GofferwallResources 폴더를 만들어 내부에 `.png` or `.jpg` 파일을 추가 후 호출

### IsInitialized
```csharp
Gofferwall.Sdk.GetCoreInstance().IsInitialized();
```

### SDK Versions
```csharp
Gofferwall.Sdk.GetOptionGetter().GetSDKVersion();
```

### Unity SDK Versions
```csharp
Gofferwall.Sdk.GetOptionGetter().GetUnitySDKVersion();
```

### Network Versions (only Android)
```csharp
Gofferwall.Sdk.GetOptionGetter().GetNetworkVersions();
```

### Android Media Id
```csharp
Gofferwall.FrameworkSettings.MediaID_AOS;
```

### iOS Media Id
```csharp
Gofferwall.FrameworkSettings.MediaID_IOS;
```
