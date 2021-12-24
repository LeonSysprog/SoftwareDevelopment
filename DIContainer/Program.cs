using System;

namespace DIContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            DIContainer di = new DIContainer();
            di.AddTransient<IA, A>();
            di.AddTransient<IB, B>();
            var instance = di.Get(typeof(IA));
        }
    }
}
