namespace Play.With.Generics.Invariance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Box<Fruit> boxFruit = new Box<Fruit>();
            Box<Apple> boxApple = new Box<Apple>();
            Box<Banana> boxBanana = new Box<Banana>();

            //invariance -- use only the type originally specified
            boxFruit = boxApple;

            //covariance -- return more derived type when general is expected
            //Allows more derived types to be returned.
            IEnumerable<Fruit> fruits = new List<Apple>();//can -> behind we return apple which is the child of Fruit
            IEnumerable<Apple> apples = new List<Fruit>();//error

            //Contravariance -- accepting more derived type as input when more general is expected.
            //Allows more derived types to be accepted as input
            Action<Fruit> actionExpectFruit = input => { };
            Action<Apple> actionExpectApple = input => { };

            actionExpectApple = actionExpectFruit;//can -> accept apple, but behind method that can handle fruit, apple is child of fruit
            actionExpectFruit = actionExpectApple;//error -> can accept fruit, but behind is the method that can handle apple

        }

        class Box<T>
        {
            public T Value;
        }

        class Fruit { }
        class Apple : Fruit { }
        class Banana : Fruit { }
    }
}
