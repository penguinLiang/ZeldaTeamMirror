using System;

namespace Zelda
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            ZeldaGame game;

            do
            {
                game = new ZeldaGame();
                game.Run();
            } while (game.Resetting);
        }
    }
#endif
}
