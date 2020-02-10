using System;
using System.Windows.Forms;
using TestPipesClient;
using TestPipesDLL;

namespace ClientUIForms
{
   static class ProgramClientForms
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

         FormsUI aUI = new FormsUI();


         InternalPipeClient theClient = new InternalPipeClient(aUI as IClientUI);

         Application.Run();
      }
   }
}