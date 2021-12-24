using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DIContainer
{
    public class DIContainer
    {
        private Dictionary<Type, Func<object>> registrations;

        private object CreateInstance(Type implementationType)
        {
            if (new StackTrace().FrameCount > 100)
            {
                throw new Exception("CreateInstance: circular references");
            }

            var ctors = implementationType.GetConstructors();
            foreach (var ctor in ctors)
            {
                var paramTypes = ctor.GetParameters().Select(p => p.ParameterType);
                var dependencies = paramTypes.Select(Get).ToArray();
                if (dependencies.Length > 0)
                    return Activator.CreateInstance(implementationType, dependencies);
            }

            throw new Exception("Not instance");
        }

        public DIContainer()
        {
            registrations = new Dictionary<Type, Func<object>>();
        }

        public void AddTransient<TService, TImplementation>() where TImplementation : TService =>
            registrations.Add(typeof(TService), () => this.Get(typeof(TImplementation)));

        public void AddTransient<TService>(Func<TService> func) =>
            registrations.Add(typeof(TService), () => func());

        public void AddTransientInstance<TService>(TService instance) =>
            registrations.Add(typeof(TService), () => instance);

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
