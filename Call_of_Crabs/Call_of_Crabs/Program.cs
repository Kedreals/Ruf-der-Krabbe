using System;
using System.Globalization;
using System.Threading;

namespace Call_of_Crabs
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("En-Us");
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
