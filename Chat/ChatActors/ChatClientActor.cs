using System;
using Akka.Actor;
using ChatMessages;

namespace ChatActors
{
    public class ChatClientActor : ReceiveActor, ILogReceive
    {
        private string _nick;
        private readonly ActorSelection _server;

        void Connecting()
        {
            Receive<ConnectResponse>(rsp =>
            {
                Console.WriteLine("Connected!");
                Console.WriteLine(rsp.Message);

                Become(Connected);

                Context.Watch(Sender);
            });
        }

        void Connected()
        {
            Receive<Terminated>(term => 
            Self.GracefulStop(TimeSpan.FromSeconds(30)));

            Receive<NickRequest>(nr =>
            {
                Console.WriteLine("Changing nick to {0}", nr.NewUsername);

                _server.Tell(nr);
            });

            Receive<NickResponse>(nrsp =>
               Console.WriteLine("{0} is now known as {1}", nrsp.OldUsername, nrsp.NewUsername));

            Receive<SayRequest>(sr => _server.Tell(sr));

            Receive<SayResponse>(srsp =>
                Console.WriteLine("{0}: {1}", srsp.Username, srsp.Text));
        }

        public ChatClientActor(string nickname, string remoteChatServerPath)
        {
            _nick = nickname;
            _server = Context.ActorSelection(remoteChatServerPath);

            Receive<Connect>(cr =>
            {
                Console.WriteLine("Connecting....");

                _server.Tell(new ConnectRequest(_nick));

                Become(Connecting);
            });
        }
    }
}

