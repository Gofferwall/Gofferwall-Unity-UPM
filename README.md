# Gofferwall Unity Package Manager
[![GitHub package.json version](https://img.shields.io/badge/Unity-1.3.0-blue)](../../releases)
[![GitHub package.json version](https://img.shields.io/badge/Android-1.3.0-blue)](https://github.com/Gofferwall/Gofferwall-Android-Sample)
[![GitHub package.json version](https://img.shields.io/badge/iOS-1.3.0-blue)](https://github.com/Gofferwall/Gofferwall-iOS-Sample)

- **Unity Editor 2022.3.9f1 이하에서 iOS xcode15 빌드 시 ${\color{red}사용 불가}$**
- Unity Editor : 2021.3.8f1+, 2022.3.10f1+
- Android Target API Level : 31+
- Android Minimum API Level : 16
- iOS Minimum Version : 12.0
- Xcode Minimum Version : Xcode 15.1
<br/>

## Contents
#### [Add the Gofferwall package to Your Project](#add-the-gofferwall-package-to-your-project-1)
- [Update the Gofferwall package to Your Project](./docs/update.md)
#### [Gofferwall Overview](#gofferwall-overview-1)
- [Initialize](#2-initialize)
- [사용자 정보 설정](#3-사용자-정보-설정)
- [Offerwall](#4-offerwall)
#### [Gofferwall Server 연동하기](./docs/reward_callback_info.md)
#### [웹사이트 필수 등록 (Only Android)](#웹사이트-필수-등록)
#### [Xcode에서의 Error 정리 (Only iOS)](#xcode에서의-error-정리)
- [Unity Editor 21.3.33f1, 21.3.34f1, 22.3.14f1, 22.3.15f1 Error 해결 방법](./docs/xcode_error.md#unity-editor-특정-버전에서-build-error)
- [Xcode Archive Error 해결 방법](./docs/xcode_error.md#xcode-archive-error)
- [Unity Editor 2022.3.9f1 이하에서 iOS xcode15 빌드 시 Error 내용](./docs/xcode_error.md#unity-editor-202239f1-이하에서-ios-xcode15-빌드-시-error)
#### [Gofferwall Error Information](./docs/error_info.md)
#### [etc](.)
- [Gofferwall Sample App](./docs/sampleapp.md)
- [Releases](../../releases)
- [LICENSE](./LICENSE)
<br/>


## Add the Gofferwall package to Your Project
### 1. Unity Package Manager window
#### A. Git URL
![packagemanagerUrl](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/ae869a11-77b8-4ebb-95f4-605ac4dac0d6)   
가. Unity의 `Window` > `Package Manager` 메뉴 클릭<br/>
나. Package Manager의 왼쪽 상단 `플러스(+)` 버튼 > `Add package from git URL` 버튼 클릭<br/>
다. 아래 링크를 붙여넣고 `Add` 버튼 클릭<br/>
```
https://github.com/Gofferwall/Gofferwall-Unity-UPM.git?path=Gofferwall
```
> - [결과 확인](./docs/upm_result.md#1-a-unity-package-manager-window---git-url)
<br/>

#### B. tarball
![packagemanagerTarball](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/95561971-333e-4297-8077-77d1f9176b98)   
가. [Releases](../../releases) 페이지에서 필요한 SDK 버전의 `Assets` > `com.tnk.gofferwall.tgz` 버튼을 클릭하여 tarball 파일 다운로드<br/>
나. Unity의 `Window` > `Package Manager` 메뉴 클릭<br/>
다. Package Manager의 왼쪽 상단 `플러스(+)` 버튼 > `Add package from tarball` 버튼 클릭<br/>
라. 다운로드받은 tgz 파일을 선택<br/>
> - [결과 확인](./docs/upm_result.md#1-b-unity-package-manager-window---tarball)
<br/>

### 2. Download External Dependency Manager for Unity
![googleUnity](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/de659510-d6ef-4fe2-a4aa-d953b9c629d4)   
- [External Dependency Manager for Unity - 마지막 버전 파일로 이동](https://github.com/googlesamples/unity-jar-resolver/blob/master/external-dependency-manager-latest.unitypackage)   
- [External Dependency Manager for Unity - 사이트 이동](https://github.com/googlesamples/unity-jar-resolver#getting-started)   
- `external-dependency-manager-*.unitypackage` 파일을 다운로드
- Unity project를 열어서 navigate에서 `Assets -> Import Package -> Custom Package` 선택
- `external-dependency-manager-*.unitypackage` 파일을 선택 후 전체 `Import`
- Unity version `2022.2+` 에서는 `1.2.176+` 사용    
> - [결과 확인](./docs/upm_result.md#2-download-external-dependency-manager-for-unity)
<br/>

### 3. Project Settings - Player
![playerAndroid](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/0d1d8b05-7c2d-48eb-baeb-e9f0aef4635a)   
- Unity project를 열어서 navigate에서 `Edit -> Project Settings`로 Project Settings 창 Open
- `Player`를 선택 후 `Android`탭으로 이동

![playerAndroidTarget](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/c798b9a2-b1e2-4cdf-95c5-04fb5be41161)   
- `Other Settings`에서 `Target API Level`를 `API lovel 31`이상으로 설정

![playerAndroidKeystore](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/19ab3ff8-9eeb-4ec4-aed1-d0b2a6b75471)   
- `Publishing Settings`에서 `Project Keystore`와 `Project Key`를 설정

![playerAndroidBuild21-3-8](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/b148f2cb-08ac-47e8-9ca5-cd3f4914aae9)   
- `Build > Custom Main Manifest` 체크를 설정
- `Build > Custom Main Gradle Template` 체크를 설정
- `Build > Custom Gradle Properties Template` 체크를 설정
> - [2022.3.+ 변경 설정 확인](./docs/other_unity_version.md)
> - [결과 확인](./docs/upm_result.md#3-project-settings---player)
<br/>

### 4. GofferwallSDK Settings
#### 가. Project Settings - GofferwallSDK
![gofferwallJson](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/620ba7b7-0c0c-4eec-9002-e9af6bed10ac)  
- Unity project를 열어서 navigate에서 `Edit -> Project Settings`로 Project Settings 창을 열어 `GofferwallSDK`를 선택   
- `Settings Android from json file`를 선택하여 전달받은 Android.json 파일을 선택   
- `Settings iOS from json file`를 선택하여 전달받은 iOS.json 파일을 선택   
- Dashboard의 값은 Gofferwall 설정 값들로 자동 세팅
- Dashboard의 값을 직접 수정 후 `Create Gofferwall Android & iOS Files`를 선택하면 해당 값으로 앱 설정 됨
<br/>

![gofferwallJson1](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/7ec36871-4991-4e42-abc4-9fe9c551ac7f)
- `Create Gofferwall Android & iOS Files`를 선택
- ${\color{red}버전}$ ${\color{red}변경}$ ${\color{red}시}$마다 `Create Gofferwall Android & iOS Files`를 선택해야 해당 값으로 앱 설정 됨
- 인터넷이 연결되어 있어야 함
> - [Android 결과 확인](./docs/upm_result.md#4-gofferwallsdk-settings)
<br/>

#### 나. Import to Stript
```csharp
FrameworkSettingsRegister.GofferwallImportJson(<Android_Json_Path>, <iOS_Json_Path>);
```
- '/Library/PackageCache/com.tnk.gofferwall/Editor/Scripts/FrameworkSettingsRegister.cs' 파일에 있는 함수 호출
- 관리자에게 전달 받은 Android & iOS의 Json 파일 위치 입력
> - [결과 확인](./docs/upm_result.md#4-gofferwallsdk-settings)
<br/>

### 5. External Dependency Manager 설정 (Android 전용)
- Unity project를 열어서 navigate에서 `Assets -> External Dependency Manager -> Android Resolver -> Resolver(or Force Resolver)`를 선택   
> - [결과 확인](./docs/upm_result.md#5-external-dependency-manager-설정-android-전용)

<br/><br/>

## Update the Gofferwall package to Your Project
> - [가이드 확인](./docs/update.md)

<br/><br/>

## Gofferwall Overview

### 1. Namespace
```csharp
using Gofferwall;
```
<br/>

### 2. Initialize
#### 가. Code에서 Media 없이 Initialize 방법
```csharp
Gofferwall.Sdk.GetCoreInstance().Initialize((isSuccess) => {
    if (isSuccess) {
        // Initialize Call Back
    } else {
        // Initialize Fail
    }
}, CALLBACK_TAG, CHILD_YN);
```
- Android는 `Gofferwall.androidlib`폴더 내의 `AndroidManifest.xml`에 `adiscope.global.mediaId`가 있어야 함 ([파일 위치 확인](./docs/upm_result.md#4-gofferwallsdk-settings))
- iOS는 Build된 Project에서 `Info.plist` 파일에서 `GofferwallMediaId`가 있어야 함 ([Info.plist 확인](./docs/upm_result.md#6-infoplist의-gofferwallmediaid-확인-ios-전용))
- 반드시 unity의 main thread에서 실행
- App 실행 시 1회 설정 권장
> - [Other Initialize API](./docs/other_api.md#initialize)
<br/>

#### 나. Code에서 직접 Media 넣어서 Initialize 방법
```csharp
private string MEDIA_ID = "";        // 관리자를 통해 발급
private string MEDIA_SECRET = "";    // 관리자를 통해 발급
Gofferwall.Sdk.GetCoreInstance().Initialize(MEDIA_ID, MEDIA_SECRET, (isSuccess) => {
    if (isSuccess) {
        // Initialize Call Back
    } else {
        // Initialize Fail
    }
});
```
- 반드시 unity의 main thread에서 실행
- App 실행 시 1회 설정 권장
> - [Other Initialize API](./docs/other_api.md#initialize)
<br/>

### 3. 사용자 정보 설정
```csharp
private string USER_ID = "";        // set unique user id to identify the user in reward information
Gofferwall.Sdk.GetCoreInstance().SetUserId(USER_ID);
```
- `Offerwall`을 사용하기 위해 ${\color{red}필수}$ 설정
- 64자까지 설정 가능
<br/>

### 4. Offerwall
#### A. Offerwall Ad Instance 생성
```csharp
// get singleton instance of offerwall ad
Gofferwall.Feature.OfferwallAd offerwallAd = Gofferwall.Sdk.GetOfferwallAdInstance();
```
- Offerwall Ad Instance는 global singleton instance이므로 여러개의 instance를 생성할 수 없음
- Offerwall Ad의 callback event handler는 등록과 해제가 자유로우나 globally static하므로 중복 등록되지 않도록 유의

#### B. Callback 등록
```csharp
offerwallAd.OnOpened += OnOfferwallAdOpenedCallback;
offerwallAd.OnClosed += OnOfferwallAdClosedCallback;
offerwallAd.OnFailedToShow += OnOfferwallFailedToShowCallback;
```

#### C. Show
- `Show`가 실행되면 (return값이 True일 경우) `OnOpened`와 `OnFailedToShow` 중 하나가 항상 호출

##### 가. Global Show
```csharp
private string UNIT_ID = "";        // 관리자를 통해 발급
if (offerwallAd.Show(UNIT_ID)) {
    // Success
} else {
    // This Show request is duplicated
}
```
- Gofferwall 의 Offerwall ViewController 를 IP로 국내(TNK)/국외(Tapjoy)를 구분하여 Display

##### 나. TNK Show
```csharp
private string UNIT_ID = "";        // 관리자를 통해 발급
if (offerwallAd.Show4TNK(UNIT_ID)) {
    // Success
} else {
    // This Show request is duplicated
}
```
- Gofferwall 의 TNK Offerwall ViewController 를 Display

##### 다. Tapjoy Show
```csharp
private string UNIT_ID = "";        // 관리자를 통해 발급
if (offerwallAd.Show4Tapjoy(UNIT_ID)) {
    // Success
} else {
    // This Show request is duplicated
}
```
- Gofferwall 의 Tapjoy Offerwall ViewController 를 Display

#### D. Callbacks
```csharp
private void OnOfferwallAdOpenedCallback(object sender, Gofferwall.Model.ShowResult args) {
    // Offerwall이 열림
}
private void OnOfferwallAdFailedToShowCallback(object sender, Gofferwall.Model.ShowFailure args) {
    // Offerwall이 Fail
}
```
- Show 성공 시 `OnOpened` callback이 호출
- Callback은 Unity의 main thread에서 호출
<br/>

### 5. Other API
> - [Other API](./docs/other_api.md#other-api-1)

<br/><br/>

## Gofferwall Server 연동하기
> - [연동하기](./docs/reward_callback_info.md)

<br/><br/>

## 웹사이트 필수 등록
- 관리자에게 전달받은 `app-ads.txt`를 웹사이트에 등록
- Only Android
> - [app-ads.txt 등록 방법 및 정보](./docs/app-ads.txt.md)

<br/><br/>

## Xcode에서의 Error 정리
- Only iOS
<br/>

### Unity Editor 21.3.33f1, 21.3.34f1, 22.3.14f1, 22.3.15f1에서의 Error
- 'Unexpected duplicate tasks' Error
> - [해결 방법](./docs/xcode_error.md#unity-editor-특정-버전에서-build-error)

<br/><br/>

### Xcode Archive Error
> - [해결 방법](./docs/xcode_error.md#xcode-archive-error)

<br/><br/>

### Unity Editor 2022.3.9f1 이하에서 iOS xcode15 빌드 시 Error
> - [오류 내용](./docs/xcode_error.md#unity-editor-202239f1-이하에서-ios-xcode15-빌드-시-error)

<br/><br/>

## Gofferwall Error Information
> - [Error 정보](./docs/error_info.md)

<br/><br/>

## Gofferwall Sample App
> - [적용 방법 확인](./docs/sampleapp.md)

<br/><br/>

## Releases
> - [Releases](../../releases)

<br/><br/>

## LICENSE
> - [LICENSE](./LICENSE)
