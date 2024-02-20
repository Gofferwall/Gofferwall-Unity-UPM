# Unity Editor 특정 버전에서 Build Error
- 검토한 Unity Editor : 21.3.8, 21.3.19, 21.3.30 ~ 34, 22.2.21, 22.3.0 ~ 18
- 오류 확인한 Unity Editor : 21.3.33 ~ 34, 22.3.14 ~ 15
- [Issuetracker 확인](https://issuetracker.unity3d.com/issues/ios-embed-frameworks-build-phase-is-duplicated-when-multiple-frameworks-are-present-in-the-project)
<br/>

## Unexpected Duplicate Tasks Error
![unity22_3_14_xcode_build_error](https://github.com/adiscope/Adiscope-Unity-UPM/assets/60415962/dd037a20-6290-49af-ab08-e1e402dc0bbb)<br/>
<br/>

## 해결 방법 (A, B 모두 처리)
### A. Unity-iPhone 처리
![unity22_3_14_xcode_build_error_fix1](https://github.com/adiscope/Adiscope-Unity-UPM/assets/60415962/543bcc97-370c-4a17-a97e-84b97851090e)<br/>
#### 1. Xcode에서 프로젝트 선택
#### 2. TARGETS에서 Unity-iPhone 선택
#### 3. Build Phases 선택
#### 4. Embed Frameworks에서 UnityFramework.framework가 있는 것에서 Adiscope.framework의 Code Sign On Copy 선택
#### 5. Embed Frameworks에서 UnityFramework.framework가 없는 것을 삭제
<br/><br/>

### B. UnityFramework 처리
![unity22_3_14_xcode_build_error_fix2](https://github.com/adiscope/Adiscope-Unity-UPM/assets/60415962/c95489e0-f28d-48db-993e-4610f6805b62)
#### 1. Xcode에서 프로젝트 선택
#### 2. TARGETS에서 UnityFramework 선택
#### 3. Build Phases 선택
#### 4. Embed Frameworks에서 Code Sign On Copy 선택 안 되어 있는 것을 삭제
<br/><br/><br/>

# Xcode Archive Error
<br/>

## ITMS-90206 해결 방법
![apple_itms_90206](https://github.com/adiscope/Adiscope-Unity-UPM/assets/60415962/8293acc0-7de8-4d05-bcf8-d36d1df395f4)<br/>

### 1. Archives 창에서 Archive 한 Version 우클릭 해서 'Show in Finder' 선택
### 2. 선택이 되어 있는 Xcode Archive File 우클릭 해서 '패키지 내용 보기' 선택
### 3. 'Products' 폴더 이동
### 4. 'Applications' 폴더 이동
### 5. 앱이름의 응용 프로그램 우클릭 해서 '패키지 내용 보기' 선택
### 6. 'Frameworks' 폴더 이동
### 7. 'UnityFramework.framework' 폴더 이동
### 8. 'Frameworks' 폴더 삭제
### 9. Archives 창에서 폴더 삭제 한 Version으로 'Distribute App' 선택 후 재진행
<br/><br/><br/>

# Unity Editor 2022.3.9f1 이하에서 iOS xcode15 빌드 시 Error
- GameAssembly - Command PhaseScriptExecution failed with a nonzero exit code
- [Issuetracker 확인](https://issuetracker.unity3d.com/issues/ios-application-building-fails-and-the-command-phasescriptexecution-failed-with-a-nonzero-exit-code-error-appears-when-building-with-xcode-15-dot-0)
