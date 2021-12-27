using System;
using System.Collections.Generic;

namespace DIContainer
{
    public interface IB
    {
        public void WriteB();
    }

    public class B : IB
    {
        private List<int> listB;

        public B() => listB = new List<int>();

        public B(IA ia)
        {
            listB = new List<int>();
            Console.WriteLine("Construct B with IA");
            ia.WriteA();
        }

        public void WriteB() => Console.WriteLine("Class B");
    }
}