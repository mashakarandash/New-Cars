using System;
using System.Collections.Generic;

public class ServiceLocator 
{
    public static ServiceLocator Instance;
    public ServiceLocator()
    {
        Instance = this;
    }

    private readonly Dictionary<string, IService> _serviceFolder = new Dictionary<string, IService>(); // склад, который хранит все наши сервисы, которые мы регистрируем внутри него

    public void RegisterService<TService>(TService serviceToRegister) where TService : IService //регистрация сервиса по типу. TService : IService - условия работы, любые добавляемые сервисы должны быть унаследованы от IService 
    {
        string key = typeof(TService).Name;
        if (_serviceFolder.ContainsKey(key))
        {
           throw new InvalidOperationException($"{key} сервис уже существует в _serviceFolder");
           
        }
        _serviceFolder.Add(key, serviceToRegister);
    }

    public void UnRegisterService<TService>(TService serviceToRegister) where TService : IService //наоборот удаление из склада(убираем регистрацию)
    {
        string key = typeof(TService).Name;
        if (_serviceFolder.ContainsKey(key))
        {
            _serviceFolder.Remove(key);

        }
        else
        {
            throw new InvalidOperationException($"{key} сервис не существует в _serviceFolder");
        }
    }

    public TService GetRegisterService<TService>() where TService : IService //получаем серсис по условию
    {
        string key = typeof(TService).Name;
        if (_serviceFolder.ContainsKey(key))
        {
            return (TService)_serviceFolder[key];

        }
        else
        {
            throw new InvalidOperationException($"{key} сервис не существует в _serviceFolder");
        }
    }

}

public interface IService // I - интерфейс, любые интерфейсы должны быть либо сущ., либо прилаг.(наречие)
{

}


