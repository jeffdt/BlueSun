using System;

namespace BlueSun {
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BlueSunGame())
                game.Run();
        }
    }
#endif
}
