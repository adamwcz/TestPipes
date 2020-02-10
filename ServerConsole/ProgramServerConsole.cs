using System;
using System.Threading.Tasks;
using TestPipesDLL;
using TestPipeServer;

namespace ServerUIConsole
{
    class ProgramServerConsole
    {
        static void Main(string[] args)
        {
            ConsoleUI TheServerUI = new ConsoleUI();

            World TheWorld = new World(TheServerUI as IServerUI);

            Console.WriteLine("Hello World!");

            // If the console is closed with the X button, close the server
            AppDomain.CurrentDomain.ProcessExit += (s, e) => TheServerUI.Close();

            // Run until escape is pressed
            ConsoleKey aKey = ConsoleKey.NoName;
            while (aKey != ConsoleKey.Escape)
            {
                aKey = Console.ReadKey().Key;

                // keys to do things can go here
            }
        }
    }
    //class Main
    //{
    //    static ConsoleUI TheServerUI;
    //    public Main()
    //    {
            
    //    }
    //}

    class ConsoleUI : IServerUI
    {
        public Action ServerUIClosing { get; set; }
        public void AddLogText(string theText)
        {
            Console.WriteLine(theText);
        }

        public void UserListAddUser(string theName)
        {
            Console.WriteLine("add user: " + theName);
        }

        public void UserListRemoveUser(string theName)
        {
            Console.WriteLine("remove user: " + theName);
        }
        public void Close()
        {
            ServerUIClosing?.Invoke();

        }
    }
}