using System;

namespace Zelda
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var game = new ZeldaGame();
            game.Run();
            game.Dispose();
        }
    }
#endif
}
