namespace SignalRSample.Services;

public class ChatService
{
    private readonly Dictionary<string, string> _users = new();
    private readonly List<ChatMessage> _messages = new();
    private readonly object _lock = new();

    public void AddUser(string connectionId, string username)
    {
        lock (_lock)
        {
            _users[connectionId] = username;
        }
    }

    public void RemoveUser(string connectionId)
    {
        lock (_lock)
        {
            _users.Remove(connectionId);
        }
    }

    public List<string> GetConnectedUsers()
    {
        lock (_lock)
        {
            return _users.Values.ToList();
        }
    }

    public void AddMessage(string user, string message)
    {
        lock (_lock)
        {
            _messages.Add(new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = DateTime.Now
            });

            // 메시지가 너무 많아지지 않도록 최대 1000개로 제한
            if (_messages.Count > 1000)
            {
                _messages.RemoveAt(0);
            }
        }
    }

    public List<ChatMessage> GetMessages(int? limit = null)
    {
        lock (_lock)
        {
            var messages = _messages.AsEnumerable();
            if (limit.HasValue)
            {
                messages = messages.TakeLast(limit.Value);
            }
            return messages.ToList();
        }
    }

    public string? GetUsername(string connectionId)
    {
        lock (_lock)
        {
            return _users.TryGetValue(connectionId, out var username) ? username : null;
        }
    }
}

public class ChatMessage
{
    public string User { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

