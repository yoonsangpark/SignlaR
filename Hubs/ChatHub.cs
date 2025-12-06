using Microsoft.AspNetCore.SignalR;
using SignalRSample.Services;

namespace SignalRSample.Hubs;

public class ChatHub : Hub
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    // 클라이언트 연결 시 호출
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    // 클라이언트 연결 해제 시 호출
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var username = _chatService.GetUsername(Context.ConnectionId);
        if (username != null)
        {
            _chatService.RemoveUser(Context.ConnectionId);
            await Clients.All.SendAsync("UserDisconnected", username, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    // 사용자 이름 설정
    public async Task SetUsername(string username)
    {
        _chatService.AddUser(Context.ConnectionId, username);
        await Clients.All.SendAsync("UserJoined", username, Context.ConnectionId);
    }

    // 모든 클라이언트에게 메시지 전송
    public async Task SendMessage(string user, string message)
    {
        _chatService.AddMessage(user, message);
        await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now.ToString("HH:mm:ss"));
    }

    // 연결된 사용자 목록 가져오기
    public async Task GetConnectedUsers()
    {
        var users = _chatService.GetConnectedUsers();
        await Clients.Caller.SendAsync("ConnectedUsers", users);
    }
}

