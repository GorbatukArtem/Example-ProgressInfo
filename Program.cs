using System;
using System.Threading.Tasks;
using System.Threading;

namespace ProgressInfo
{
    class Program
    {
        public static void DoSomething(IProgress<int> progress, CancellationToken token)
        {
            for (var i = 1; i <= 100; i++)
            {
                Thread.Sleep(100); // < logic
                progress?.Report(i);
                token.ThrowIfCancellationRequested();
            }
        }

        public static async Task Main(string[] args)
        {
            var progress = new Progress<int>(percent =>
            {
                Console.WriteLine(percent.ToString()); // < progress bar
            });

            var token = new CancellationToken();

            await Task.Run(() => DoSomething(progress, token), token);

            Console.ReadLine();
        }
    }
}