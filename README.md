# SignalR 실시간 채팅 샘플 프로젝트

ASP.NET Core SignalR을 사용한 실시간 통신 샘플 애플리케이션입니다.

## 기능

- ✅ **기본 연결**: 클라이언트-서버 실시간 연결 및 연결 상태 관리
- ✅ **실시간 채팅**: 모든 클라이언트에게 메시지 브로드캐스트
- ✅ **그룹 메시징**: 특정 그룹에만 메시지 전송
- ✅ **사용자 관리**: 연결된 사용자 목록 표시
- ✅ **연결 상태 표시**: 실시간 연결 상태 모니터링

## 요구사항

- .NET 8.0 SDK 이상
- 웹 브라우저 (Chrome, Edge, Firefox 등)

## 설치 및 실행

### 1. 프로젝트 복원

```bash
cd C:\ws\SignlaR
dotnet restore
```

### 2. 프로젝트 실행

```bash
dotnet run
```

서버가 시작되면 기본적으로 `http://localhost:5000` 또는 `https://localhost:5001`에서 실행됩니다.

### 3. 브라우저에서 접속

웹 브라우저를 열고 다음 주소로 접속하세요:

```
http://localhost:5000
```

또는

```
https://localhost:5001
```

## 사용 방법

1. **사용자 이름 설정**
   - 페이지 상단의 입력란에 사용자 이름을 입력하고 "이름 설정" 버튼을 클릭합니다.

2. **메시지 전송**
   - 하단의 메시지 입력란에 메시지를 입력하고 "전송" 버튼을 클릭하거나 Enter 키를 누릅니다.
   - 그룹에 참가하지 않은 경우: 모든 사용자에게 메시지가 전송됩니다.
   - 그룹에 참가한 경우: 해당 그룹의 사용자들에게만 메시지가 전송됩니다.

3. **그룹 관리**
   - 그룹 이름을 입력하고 "그룹 참가" 버튼을 클릭하여 그룹에 참가할 수 있습니다.
   - "그룹 나가기" 버튼을 클릭하여 그룹에서 나갈 수 있습니다.
   - 여러 그룹에 참가할 수 있으며, 메시지는 현재 참가한 첫 번째 그룹에 전송됩니다.

4. **연결 상태 확인**
   - 페이지 상단의 상태 표시기를 통해 연결 상태를 확인할 수 있습니다.
   - 오른쪽 사이드바에서 연결된 사용자 목록과 Connection ID를 확인할 수 있습니다.

## 프로젝트 구조

```
SignalRSample/
├── SignalRSample.csproj    # 프로젝트 파일
├── Program.cs               # SignalR Hub 등록 및 미들웨어 설정
├── Hubs/
│   └── ChatHub.cs          # SignalR Hub 구현
├── wwwroot/
│   └── index.html          # 클라이언트 UI
└── README.md               # 프로젝트 설명
```

## 주요 파일 설명

### Program.cs
- SignalR 서비스 등록
- CORS 설정 (모든 오리진 허용)
- 정적 파일 제공 설정
- ChatHub 엔드포인트 매핑

### Hubs/ChatHub.cs
- `SetUsername`: 사용자 이름 설정
- `SendMessage`: 모든 클라이언트에게 메시지 전송
- `SendMessageToGroup`: 특정 그룹에 메시지 전송
- `JoinGroup`: 그룹 참가
- `LeaveGroup`: 그룹 나가기
- `GetConnectedUsers`: 연결된 사용자 목록 조회

### wwwroot/index.html
- SignalR JavaScript 클라이언트 라이브러리 사용
- 실시간 채팅 UI
- 그룹 관리 기능
- 연결 상태 표시

## 테스트 방법

1. 여러 브라우저 탭이나 창을 열어 여러 클라이언트를 시뮬레이션합니다.
2. 각 클라이언트에서 다른 사용자 이름을 설정합니다.
3. 메시지를 전송하여 실시간 통신이 작동하는지 확인합니다.
4. 그룹 기능을 테스트하기 위해 일부 클라이언트만 특정 그룹에 참가시킵니다.
5. 그룹 메시지가 해당 그룹의 사용자들에게만 전송되는지 확인합니다.

## 문제 해결

### 연결이 안 될 때
- 서버가 실행 중인지 확인하세요.
- 브라우저 콘솔에서 오류 메시지를 확인하세요.
- CORS 설정이 올바른지 확인하세요.

### 메시지가 전송되지 않을 때
- 사용자 이름이 설정되었는지 확인하세요.
- 연결 상태가 "연결됨"인지 확인하세요.
- 브라우저 콘솔에서 오류 메시지를 확인하세요.

## 참고 자료

- [ASP.NET Core SignalR 공식 문서](https://docs.microsoft.com/aspnet/core/signalr/introduction)
- [SignalR JavaScript 클라이언트](https://docs.microsoft.com/aspnet/core/signalr/javascript-client)

