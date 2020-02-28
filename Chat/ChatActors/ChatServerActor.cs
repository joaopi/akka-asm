
using System.Collections.Generic;
using Akka.Actor;
using ChatMessages;

namespace ChatActors
{
    public class ChatServerActor : ReceiveActor, ILogReceive
    {
        private readonly IDictionary<IActorRef, string> _clients = new Dictionary<IActorRef, string>();

        public ChatServerActor()
        {
            Receive<SayRequest>(message =>
            {
                var nick = _clients[Sender];
                var response = new SayResponse(nick, message.Text);

                foreach (var client in _clients) client.Key.Tell(response, Self);
            });

            Receive<ConnectRequest>(message =>
            {
                _clients.Add(Sender, message.Username);
                Sender.Tell(new ConnectResponse("Hello and welcome to Akka.NET chat example"), Self);

                Context.Watch(Sender);
            });

            Receive<Terminated>(term =>
            {
                _clients.Remove(Sender);

                Context.Unwatch(Sender);
            });

            Receive<NickRequest>(message =>
            {
                var nick = _clients[Sender];
                _clients[Sender] = message.NewUsername;

                var response = new NickResponse(nick, message.NewUsername);

                foreach (var client in _clients) client.Key.Tell(response, Self);
            });
        }
    }
}

