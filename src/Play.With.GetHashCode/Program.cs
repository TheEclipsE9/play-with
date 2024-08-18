namespace Play.With.GetHashCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new KeyValuePair<int, int>(1, 1).GetHashCode());//always the same as value type

            Console.WriteLine("1234".GetHashCode());//always different as ref type - based on location in memory
        }
    }
}
