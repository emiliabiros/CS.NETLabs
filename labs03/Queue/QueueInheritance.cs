using System.Collections;
namespace labs03.Queue;

public class QueueInheritance : ArrayList
{
    public void Enqueue(object value)
    {
        this.Add(value);
    }

    public object Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        object value = this[0];
        this.RemoveAt(0);
        return value;
    }
}