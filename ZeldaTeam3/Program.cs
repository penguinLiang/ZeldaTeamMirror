using System;

namespace Zelda
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            //using (var game = new ZeldaGame())
            //    game.Run();

            var game = new ZeldaGame();

            while (game.Resetting)
            {
                game.Run();

                if (game.Resetting)
                    game = new ZeldaGame();
            }
        }
    }
#endif
}
