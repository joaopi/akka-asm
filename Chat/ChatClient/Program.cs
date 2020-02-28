using System;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.Configuration;
using ChatActors;
using ChatMessages;
using Hocon.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("app.hocon"));

            using (var system = ActorSystem.Create("ClientSystem", config))
            {
                Console.WriteLine("Please provide your nickname:");

                var nickname = Console.ReadLine();

                var chatClient = system.ActorOf(Props.Create<ChatClientActor>(nickname, "akka.tcp://ServerSystem@localhost:8081/user/ChatServer"));

                chatClient.Tell(new Connect());

                while (true)
                {
                    var input = Console.ReadLine();
                    if (input.StartsWith("/"))
                    {
                        var parts = input.Split(' ');
                        var cmd = parts[0].ToLowerInvariant();
                        var rest = string.Join(" ", parts.Skip(1));

                        if (cmd == "/nick")
                        {
                            chatClient.Tell(new NickRequest(rest));
                        }
                        if (cmd == "/exit")
                        {
                            Console.WriteLine("exiting");
                            break;
                        }
                    }
                    else
                    {
                        chatClient.Tell(new SayRequest(input));
                    }
                }

                system.Terminate().Wait();
            }
        }
    }
}

