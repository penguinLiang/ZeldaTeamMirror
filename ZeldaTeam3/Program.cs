using System;

namespace Zelda
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            bool runGame = true;
            var game = new ZeldaGame();

            while (runGame)
            {
                game.Run();

                runGame = game.Resetting;

                if (runGame)
                    game = new ZeldaGame();
            }
        }
    }
#endif
}
