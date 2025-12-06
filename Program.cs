using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;
using SignalRSample.Services;

var builder = WebApplication.CreateBuilder(args);

// SignalR 서비스 추가
builder.Services.AddSignalR();

// 채팅 서비스 추가
builder.Services.AddSingleton<ChatService>();

// CORS 설정
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// CORS 미들웨어 사용
app.UseCors("AllowAll");

// 정적 파일 제공 (wwwroot 폴더)
app.UseDefaultFiles();
app.UseStaticFiles();

// SignalR Hub 엔드포인트 매핑
app.MapHub<ChatHub>("/chathub");

// REST API 엔드포인트
var api = app.MapGroup("/api");

// 연결된 사용자 목록 조회
api.MapGet("/users", (ChatService chatService) =>
{
    var users = chatService.GetConnectedUsers();
    return Results.Ok(new { users });
})
.WithName("GetUsers")
.WithTags("Chat API");

// 메시지 전송
api.MapPost("/messages", (SendMessageRequest request, ChatService chatService, IHubContext<ChatHub> hubContext) =>
{
    if (string.IsNullOrWhiteSpace(request.User) || string.IsNullOrWhiteSpace(request.Message))
    {
        return Results.BadRequest(new { error = "사용자 이름과 메시지는 필수입니다." });
    }

    chatService.AddMessage(request.User, request.Message);
    var time = DateTime.Now.ToString("HH:mm:ss");
    hubContext.Clients.All.SendAsync("ReceiveMessage", request.User, request.Message, time);
    
    return Results.Ok(new { success = true, timestamp = time });
})
.WithName("SendMessage")
.WithTags("Chat API")
.Accepts<SendMessageRequest>("application/json");

// 메시지 히스토리 조회
api.MapGet("/messages", (ChatService chatService, int? limit) =>
{
    var messages = chatService.GetMessages(limit);
    return Results.Ok(new { messages });
})
.WithName("GetMessages")
.WithTags("Chat API");

// 기본 라우트를 index.html로 리다이렉트
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();

// 요청 모델
public record SendMessageRequest(string User, string Message);

