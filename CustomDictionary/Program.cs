using System;
using System.Collections.Generic;

namespace CustomDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomDictionary<string, int> cd = new CustomDictionary<string, int>(5);
            Console.WriteLine("Count: {0}", cd.Count);

            cd.Add(new KeyValuePair<string, int>("1", 1));
            cd.Add(new KeyValuePair<string, int>("2", 2));
            cd.Add(new KeyValuePair<string, int>("3", 3));
            cd.Add(new KeyValuePair<string, int>("4", 4));
            cd.Add(new KeyValuePair<string, int>("5", 5));

            foreach (var p in cd)
            {
                Console.WriteLine("{0} : {1}", p.Key, p.Value);
            }

            if (cd.Contains(new KeyValuePair<string, int>("4", 3)))
                Console.WriteLine("Pair contains in CustomDictionary");
            else
                Console.WriteLine("Pair isn`t contains in CustomDictionary");

            cd.Remove(new KeyValuePair<string, int>("1", 1));

            foreach (var p in cd)
            {
                Console.WriteLine("{0} : {1}", p.Key, p.Value);
            }
        }
    }
}
