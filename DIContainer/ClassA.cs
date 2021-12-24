using System;
using System.Collections.Generic;

namespace DIContainer
{
    public interface IA
    {
        public void WriteA();
    }

    public class A : IA
    {
        private List<int> listA;

        public A(IB ib)
        {
            listA = new List<int>();
            Console.WriteLine("Constructor A with IB");
            ib.WriteB();
        }
        public void WriteA() => Console.WriteLine("Class A");
    }
}
