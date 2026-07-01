using System;
using System.Collections.Generic;

public interface IServiceRegistry
{
    void Register<T>(T service);

    T Get<T>();

    bool TryGet<T>(out T service);
}

public class ServiceRegistry : IServiceRegistry
{
    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T service)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"{typeof(T).Name} already registered");
        }

        _services[typeof(T)] = service;
    }

    public T Get<T>()
    {
        if (_services.TryGetValue(typeof(T), out var service))
        {
            return (T)service;
        }

        throw new InvalidOperationException($"Service of type {typeof(T).Name} not registered.");
    }

    public bool TryGet<T>(out T service)
    {
        if (_services.TryGetValue(typeof(T), out var foundService))
        {
            service = (T)foundService;
            return true;
        }

        service = default;
        return false;
    }

    public bool Contains<T>()
    {
        return _services.ContainsKey(typeof(T));
    }
}
