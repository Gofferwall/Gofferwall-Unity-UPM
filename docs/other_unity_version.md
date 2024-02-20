# Unity Editor 2022.3.+
## Project Settings - Player
![playerAndroidBuild22-3-4](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/40b1c4d9-759c-4212-a6b7-5169bc1ce012)   
- `Build > Custom Main Manifest` 체크를 설정
- `Build > Custom Main Gradle Template` 체크를 설정
- `Build > Custom Base Gradle Template` 체크를 설정
- `Build > Custom Gradle Properties Template` 체크를 설정
- `Build > Custom Gradle Settings Template` 체크를 설정
<br/>

## android.enableR8 제거
![playerAndroidproperties](https://github.com/adiscope/Adiscope-Unity-UPM/assets/60415962/1f874038-a23a-4486-ad18-267aa7fb326e)   
- Project folder에서 `Assets -> Plugins -> Android -> gradleTemplate.properties 파일 오픈

![playerAndroidenableR8](https://github.com/Gofferwall/Gofferwall-Unity-UPM/assets/60415962/54d25fbe-cd1f-42ef-888b-8f9b1f284ff9)   
- android.enableR8 해당 줄을 삭제
