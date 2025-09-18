using System.Collections.Generic;

public class Provider<T>
{
    public IEnumerable<T> Collection => _collection;

    protected readonly List<T> _collection = new();

    public void AddRange(IEnumerable<T> collection)
    {
        _collection.AddRange(collection);
    }
    
    public void Add(T item)
    {
        _collection.Add(item);
    }
}