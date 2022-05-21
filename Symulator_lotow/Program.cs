namespace Symulator_lotow
{
    using System.Diagnostics;
    using System.Windows.Forms;
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var mainForm = new MainForm();
            KontrolerRadaru kontroler = new KontrolerRadaru(mainForm);            
            kontroler.UruchomSymulacje();
            Application.Run(mainForm);


        }
    }
}