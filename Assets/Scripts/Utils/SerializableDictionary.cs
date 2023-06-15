using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();

    public TValue GetValueByKey(TKey key)
    {
        var index = keys.IndexOf(key);

        return values[index];
    }
}
