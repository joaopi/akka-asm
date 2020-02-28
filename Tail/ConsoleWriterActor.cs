using System;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for serializing message writes to the console.
    /// (write one message at a time, champ :)
    /// </summary>
    class ConsoleWriterActor : ReceiveActor
    {
        public ConsoleWriterActor()
        {
            Receive<Messages.InputError>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message.Reason);
                Console.ResetColor();
            });
            Receive<Messages.InputSuccess>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message.Reason);
                Console.ResetColor();
            });
            ReceiveAny(message => Console.WriteLine(message));
        }

    }
}