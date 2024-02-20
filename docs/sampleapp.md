# Adiscope Sample App
"README.md"의 Add the Gofferwall package to Your Project를 먼저 실행해야 합니다.
<br/><br/>

## 1. Download Files
[Sample App Files 위치 이동](https://github.com/Gofferwall/Gofferwall-Unity-UPM/tree/main/Samples)<br/>
"Sdk" 폴더를 다운받음
<br/>

## 2. Move Files
다운로드 한 "Sdk" 폴더를 <code>Your Project 폴더 -> Assets</code>로 이동
결과 : <code>Your Project 폴더 -> Assets/Sdk/Examples</code>에<br/>
"GofferwallExample.cs", "GofferwallExampleScene01.unity", "GofferwallExampleScene02.unity" 파일 확인
<br/>

## 3. Change Value
![sampleappId](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/6f4288a0-e4c0-45f4-bdc4-ebd351e3f1c3)<br/>
이동한 파일 중 "GofferwallExample.cs" 파일을 열어서 ID등 값들을 추가(필요 값만 입력하면 됨)<br/><br/>
![sampleappScret](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/c3f89a97-1b80-466d-bd23-34dc985adba7)<br/>
{ "gofferwall_media_id" : "gofferwall_media_secret" } 로 Android, iOS 값들을 추가
<br/>

## 4. Add Build Settings
![buildSettings](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/83e9b9f4-0195-4aca-8dd7-c1417369f21d)<br/>
Unity project를 열어서 navigate에서 <code>File -> Build Settings</code>로 Build Settings창을 열고<br/>
Unity project에서 Project 텝에서 이동한 파일 중 "GofferwallExampleScene01.unity", "GofferwallExampleScene02.unity"를 추가
<br/>

## 5. Android Build
Android Platform으로 Switch 후 Build(or Build And Run)
<br/>

## 6. iOS Build
iOS Platform으로 Switch 후 Build(or Build And Run)
<br/>
