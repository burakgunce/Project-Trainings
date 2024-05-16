namespace generictrain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<int> toplam = new MyList<int>();
            toplam.Add(1);
            toplam.Add(2);

            MyList<string> cumle = new MyList<string>();
            cumle.Add("a");
            cumle.Add("b");

            Console.WriteLine(toplam.Count());
            Console.WriteLine(cumle.Count());

            MyList<int> toplam2 = new MyList<int>();
            toplam2.Add2(1);
            toplam2.Add2(2);
            toplam2.Add2(3);

            MyList<string> cumle2 = new MyList<string>();
            cumle2.Add2("a");
            cumle2.Add2("b");
            cumle2.Add2("c");

            Console.WriteLine(toplam2.Count2());
            Console.WriteLine(cumle2.Count2());

            
        }
    }
}