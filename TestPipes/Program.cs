using System;
using System.Windows.Forms;

namespace TestPipeServer
{
    static class Program
    {
        private static World TheWorld;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TheWorld = new World();
            Application.Run();
        }
    }
}