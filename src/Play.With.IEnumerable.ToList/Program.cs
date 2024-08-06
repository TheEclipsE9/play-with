namespace Play.With.IEnumerable.ToList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //array has its own object and list has ots own => every time create instances for CustomId
            // ids is like data source, it is not a collection, it is not materialized so creates new objects each time
            //IEnumerable<CustomId> ids = GetIds();// seperate objects

            // ids is an existing list in memory,
            // so ToList()/ToArray() just make shallow copy 
            // if change objects in list, it will change objects in array
            // both store references to the same object
            IEnumerable<CustomId> ids = GetIds().ToList();//

            var list = ids.Where(x => x.Value != 10).ToList();
            var array = ids.Where(x => x.Value != 10).ToArray();
            list[0].Value = -1;
            array[1].Value = -2;

            foreach (var id in list) Console.WriteLine($"List {id.Value}");
            foreach (var id in array) Console.WriteLine($"Array {id.Value}");
        }

        class CustomId
        {
            public int Value { get; set; }
        }

        static IEnumerable<CustomId> GetIds()
        {
            yield return new CustomId { Value = 1};
            yield return new CustomId { Value = 2};
            yield return new CustomId { Value = 3};
            yield return new CustomId { Value = 10};
        }
    }
}