using UnityEngine;
using System.Collections.Generic;

public class speechQueue {

    static speechQueue inst;
    public static speechQueue Inst
    {
        get
        {
            if (inst == null)
                inst = new speechQueue();
            return inst;
        }
    }

    bool beingRead = false;

    object o = new object();
    Queue<speech> queue;

    public class speech
    {
        public string name;
        public string comment;

        public speech(string name, string comment)
        {
            this.name = name;
            this.comment = comment;
        }
    }

    public speechQueue()
    {
        queue = new Queue<speech>();
        observer.Inst.listen("speechQueueFinished", finished);
    }

    public void add(speech s)
    {
        lock(o)
            queue.Enqueue(s);
    }

    public void add(string name, string comment)
    {
        lock(o)
            queue.Enqueue(new speech(name, comment));
    }

    public speech getNext()
    {
        lock(o)
        {
            if (queue.Count != 0)
                return queue.Dequeue();
            else
                return null;
        }
    }

    public void begin()
    {
        if (!beingRead)
        {
            beingRead = true;
            Application.LoadLevelAdditive("speechBubble");
        }
    }

    void finished(List<object> o)
    {
        beingRead = false;
    }
}
