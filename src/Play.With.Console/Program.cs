namespace Play.With.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ids = null;
            using (var worker = new Worker(() => $"Items: {ids?.Count}"))
            {
                ids = IdsProvider.Get();
            }

            int n = 10; // Например, 10-е число Фибоначчи
            int fibonacci = Enumerable
                .Range(1, n - 1)
                .Aggregate(new { Prev = 0, Current = 1 }, 
                (acc, _) => new { Prev = acc.Current, Current = acc.Prev + acc.Current }).Current;
            Console.WriteLine(fibonacci);
        }

        private class IdsProvider
        {
            internal static List<int>? Get()
            {
                return new List<int> { 1, 2 };
            }
        }
    }
    public class Worker : IDisposable
    {
        private readonly Func<string> logger;

        public Worker(Func<string> logger)
        {
            this.logger = logger;

            Console.WriteLine(logger());
        }

        public void Dispose()
        {
            Console.WriteLine(logger());
        }
    }
}
