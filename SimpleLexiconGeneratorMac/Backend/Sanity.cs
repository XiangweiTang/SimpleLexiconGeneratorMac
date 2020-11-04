using System;

namespace SimpleLexiconGeneratorMac
{
    public static class Sanity
    {
        public static void Requires(bool valid, string message = "Error!")
        {
            if (!valid)
                throw new Exception(message);
        }
    }
}
