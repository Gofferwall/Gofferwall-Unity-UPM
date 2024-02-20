# Gofferwall Error Information

## 1. ErrorCode Detail Info
### Gofferwall Error Code
|Code	|Value	|Description	|Cause	|Instruction|
|-------|-------|---------------|-------|-----------|
|INTERNAL_ERROR	|0	|"Internal error"	|Gofferwall Sdk 내부 오류 혹은 Gofferwall Server 오류	|지속적으로 발생 시 Adiscope 개발팀에 문의|
|MEDIATION_ERROR	|1	|"3rd party mediation network error"	|Mediation 광고 Network의 3rd party sdk 혹은 server 오류	|지속적으로 발생 시 Gofferwall 개발팀에 문의|
|INITIALIZE_ERROR	|2	|"mediaId/mediaSecret must be valid"	|Gofferwall.Sdk.Initialize 시 mediaId/mediaSecret이 유효하지 않음	|지속적으로 발생 시 Gofferwall 개발팀에 문의|
|SERVER_SETTING_ERROR	|3	|"Server settings are incorrect"	|광고를 보여주기 위해 필요한 내부 설정값 오류. AndroidManifest에 설정된 값이거나 Runtime 시 server로 부터 전달 받은 값이 정확하지 않음<br>Adiscope admin 설정의 수익화, 유닛 활성화가 OFF인 경우	|Adiscope admin page에서 등록된 media (application)의 id와 secret을 확인<br>Adiscope admin page의 설정 확인|
|INVALID_REQUEST	|4	|"The request is invalid"	|Show() 시 입력한 unitId 오류	|Adiscope admin page에 정의된 각 unitId를 다시 확인 후 Show()에 입력|
|NETWORK_ERROR	|5	|"There is a network problem"	|Network read/write timed out 혹은 Network connection 오류	|Device의 network 연결 상태를 확인|
|USER_SETTING_ERROR	|6	|"Userid is not set"	|오퍼월 Show를 호출했는데 userId 세팅이 안 되어 있는 경우|
|"UNKNOWN_ERROR (Only Unity)"	|-1	|""	|알 수 없는 오류	|지속적으로 발생 시 Gofferwall 개발팀에 문의|

<br/>

## 2. 에러 메시지에 코드 추가
* 사용자가 광고 재생을 시도했으나 실패하여 CS 인입된 경우 빠른 대응을 위해, 사용자 화면의 알림 메시지에 에러 코드를 첨부할 것을 권장한다.
* 사용자에게 전달할 에러 메시지는 커스텀 가능하나, 아래 예시처럼 메시지 뒤에 에러 코드를 첨부한다면 그 상세 배경을 확인하기 용이하므로 Gofferwall 개발팀의 빠른 대응이 가능하다.   
### Gofferwall Error Message Examples
|Code	|Value	|Cause	|Instruction	|Error Message Examples|
|-------|-------|-------|---------------|----------------------|
|MEDIATION_ERROR|1|Gofferwall Sdk 내부 오류 혹은 Gofferwall Server 오류|지속적으로 발생 시 Gofferwall 개발팀에 문의|**재생 중에 오류가 발생했습니다 잠시 후 다시 시도해주세요 (Code 1)**|
|INVALID_REQUEST|4|Show() 시 입력한 unitId 오류|Adiscope admin page에 정의된 각 unitId를 다시 확인 후 Show()에 입력|**알 수 없는 오류로 재생에 실패하였습니다 고객센터에 문의해주세요 (Code 4)**|
|NETWORK_ERROR|5|Network read/write timed out 혹은 Network connection 오류|Device의 network 연결 상태를 확인|**인터넷 연결 상태를 확인 후 다시 시도해주세요 (Code 5)**|
|UNKNOWN_ERROR(Only Unity)|-1|알 수 없는 오류|지속적으로 발생 시 Gofferwall 개발팀에 문의|**재생 중에 오류가 발생했습니다 잠시 후 다시 시도해주세요 (Code -1)**|

