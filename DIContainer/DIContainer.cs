using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DIContainer
{
    public class DIContainer
    {
        private Dictionary<Type, Func<object>> registrations;
        private Dictionary<Type, int> color;

        private bool acycle(Type implementationType)
        {
            color[implementationType] = 1;   //grey

            foreach (var record in registrations)
            {
                if (color[record.Key] == 2) //white
                    acycle(record.Key);
                if (color[record.Key] == 1)
                    return false;
            }

            color[implementationType] = 3;   //black
            return true;
        }

        private object CreateInstance(Type implementationType)
        {
            if (!acycle(implementationType))
                throw new Exception("Circle reference");

            var ctors = implementationType.GetConstructors();
            foreach (var ctor in ctors)
            {
                var paramTypes = ctor.GetParameters().Select(p => p.ParameterType);
                var dependencies = paramTypes.Select(Get).ToArray();
                if (dependencies.Length > 0)
                    return Activator.CreateInstance(implementationType, dependencies);
            }

            return Activator.CreateInstance(implementationType, null);
        }

        public DIContainer()
        {
            registrations = new Dictionary<Type, Func<object>>();
            color = new Dictionary<Type, int>();
        }

        public void AddTransient<TService, TImplementation>() where TImplementation : TService
        {
            registrations.Add(typeof(TService), () => this.Get(typeof(TImplementation)));
            color.Add(typeof(TService), 0);
        }

        public void AddTransient<TService>(Func<TService> func)
        {
            registrations.Add(typeof(TService), () => func());
            color.Add(typeof(TService), 0);
        }

        public void AddTransientInstance<TService>(TService instance)
        {
            registrations.Add(typeof(TService), () => instance);
            color.Add(typeof(TService), 0);
        }

        public void AddSingleton<TService>(Func<TService> func)
        {
            var lazy = new Lazy<TService>(func);
            AddTransient(() => lazy.Value);
        }

        public object Get(Type type)
        {
            if (registrations.TryGetValue(type, out Func<object> func))
                return func();
            else if (!type.IsAbstract)
                return this.CreateInstance(type);
            throw new InvalidOperationException("No registration for " + type);
        }
    }
}
