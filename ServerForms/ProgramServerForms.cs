using System;
using System.Windows.Forms;
using TestPipesDLL;
using TestPipeServer;

namespace ServerUIForms
{
   static class Program
   {
      /// <summary>
      ///  The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.SetHighDpiMode(HighDpiMode.SystemAware);
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         // Create the server form
         FormServer itsFormServer = new FormServer();
         itsFormServer.Show();

         // Create the World 
         new World(itsFormServer as IServerUI);

         Application.Run();
      }
   }
}