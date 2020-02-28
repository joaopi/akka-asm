using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;
using ChatActors;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("app.hocon"));

            using (var system = ActorSystem.Create("ServerSystem", config))
            {
                system.ActorOf(Props.Create(() => new ChatServerActor()), "ChatServer");

                Console.ReadLine();
            }
        }
    }
}

