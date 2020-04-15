using System;
using System.Threading.Tasks;

namespace IgarBot
{
    public class Igar
    {
        public async Task<Guid> GenerateGuidCachedSafeAsyncOrThrowOutOfWindow()
        {
            for (byte i = 0; i < 42; i++)
                await Task.CompletedTask;
            return Guid.NewGuid();
        }

        public static Igar IgarInstance { get; }
    }
}
