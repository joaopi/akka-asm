using System;

namespace ChatMessages
{
    public class Connect
    {
        public Connect()
        {
        }
    }

    public class ConnectRequest
    {
        public ConnectRequest(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public string Username { get; private set; }
    }

    public class ConnectResponse
    {
        public ConnectResponse(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Message { get; private set; }
    }

    public class NickRequest
    {
        public NickRequest(string newUsername)
        {
            NewUsername = newUsername ?? throw new ArgumentNullException(nameof(newUsername));
        }

        public string NewUsername { get; private set; }
    }

    public class NickResponse
    {
        public NickResponse(string oldUsername, string newUsername)
        {
            OldUsername = oldUsername ?? throw new ArgumentNullException(nameof(oldUsername));
            NewUsername = newUsername ?? throw new ArgumentNullException(nameof(newUsername));
        }

        public string OldUsername { get; private set; }
        public string NewUsername { get; private set; }
    }


    public class SayRequest
    {
        public SayRequest(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Text { get; private set; }
    }

    public class ChatMessage
    {
        public ChatMessage(string username, string text)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Username { get; private set; }
        public string Text { get; private set; }
    }
}
