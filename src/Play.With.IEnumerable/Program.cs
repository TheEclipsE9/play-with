namespace Play.With.IEnumerable
{
    internal class Program
    {
        static int Counter = 0;

        static void Main(string[] args)
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> fliter = ints.Where(x =>
            {
                Counter++;
                if (x > 6)
                {
                    return true;
                }

                return false;
            });
            fliter.Any();//calls where
            Console.WriteLine(fliter.First());//calls where

            Console.WriteLine($"End main. Counter: {Counter}");//14
        }
    }
}
