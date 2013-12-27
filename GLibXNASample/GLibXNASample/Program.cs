using System;

namespace GLibXNASample
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GLibXNASampleGame game = new GLibXNASampleGame())
            {
                game.Run();
            }
        }
    }
#endif
}

