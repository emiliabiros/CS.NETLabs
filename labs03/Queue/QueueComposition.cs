using System.Collections;
namespace labs03.Queue;

public class QueueComposition
{
    private readonly ArrayList _data = new ArrayList();
    public void Enqueue(object value)
    {
        _data.Add(value); 
    }
    
    public object Dequeue()
    {
        if (_data.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        object value = _data[0]; 
        _data.RemoveAt(0);     
        return value;
    }

    public object Peek()
    {
        if (_data.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        return _data[0];
    }
    
    public int Count => _data.Count;
}