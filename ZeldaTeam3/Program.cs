using System;

namespace Zelda
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            ZeldaGame game;

            do
            {
                game = new ZeldaGame();
                game.Run();
                game.Dispose();
            } while (game.Resetting);
        }
    }
#endif
}
