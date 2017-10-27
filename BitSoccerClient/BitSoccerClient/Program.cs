using System;
using Common;

namespace BitSoccerClient
{
#if WINDOWS || LINUX
    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Global.Random = new Random(0);

            using (var game = new BitSoccerClient(new TeamRas.TeamRas(), new TeamTwo.TeamTwo()))
            {
                game.Run();
            }
        }
    }
#endif
}