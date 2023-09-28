using System;

namespace assn3
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Baba.BabaGame())
                game.Run();
        }
    }
}
