using System.Collections.Generic;
using System;
using UnityEngine;

public class observer {

    Dictionary<string, List<Action<List<object>>>> actions;

    static observer inst;
    public static observer Inst
    {
        get
        {
            if (inst == null)
                inst = new observer();
            return inst;
        }
    }

	private observer()
    {
        actions = new Dictionary<string, List<Action<List<object>>>>();
    }
	
	public void invoke(string action, List<object> parameters)
    {
        if (actions.ContainsKey(action))
        {
            Debug.Log("[Action] " + action);
            foreach (Action<List<object>> a in actions[action])
            {
                a.Invoke(parameters);
            }
        }
    }

    public void listen(string action, Action<List<object>> a)
    {
        if (!actions.ContainsKey(action))
            actions[action] = new List<Action<List<object>>>();
        actions[action].Add(a);
    }
}
