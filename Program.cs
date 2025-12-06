using SignalRSample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// SignalR 서비스 추가
builder.Services.AddSignalR();

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

// 기본 라우트를 index.html로 리다이렉트
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();

