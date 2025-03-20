using System;
using UnityEngine;

[Serializable]
public class ReactiveProperty <T>
{
    public event Action<T> OnChange;
    [SerializeField] private T _value;

    public void RemoveAllListeners() => OnChange = null;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnChange?.Invoke(_value);

        }
    }
}
