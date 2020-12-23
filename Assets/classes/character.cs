using System.Collections.Generic;
using UnityEngine;
using System;

public class character {

    static List<character> characters = new List<character>();
    static object characterLock = new object();

    public delegate void talkedToHandler(character c, string speech);
    public event talkedToHandler talkedTo;

    string name;
    GameObject go;
    int direction;
    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public character(string name, GameObject go, int direction)
    {
        lock (characterLock)
        {
            if (getByName(name) != null)
            {
                Debug.Log("DUPLICATE NPC OF NAME " + name);
            }
            else
            {
                characters.Add(this);
            }
        }
        
        this.name = name;
        this.go = go;
        this.direction = direction;
    }

    public string getName()
    {
        return name;
    }

    public GameObject getObject()
    {
        return go;
    }

    public void conversationFrom(character c, string speech)
    {
        if (talkedTo != null)
        {
            talkedTo.Invoke(c, speech);
        } else
        {
            observer.Inst.invoke("enableControls", null);
        }
    }

    public float distanceFrom(Vector3 t)
    {
        return Vector3.Distance(go.transform.position, t);
    }

    public int directionTo(Vector3 t)
    {
        int v = 1;
        float vDist = 0f;

        if(t.y > go.transform.position.y) // above
        {
            v = 1;
            vDist = t.y - go.transform.position.y;
        } else
        {
            v = 3;
            vDist = go.transform.position.y - t.y;
        }

        int h = 2;
        float hDist = 0f;

        if (t.x > go.transform.position.x) // right
        {
            h = 2;
            hDist = t.x - go.transform.position.x;
        }
        else
        {
            h = 4;
            hDist = go.transform.position.x - t.x;
        }

        return (hDist > vDist) ? h : v;
    }

    public bool inDistance(Vector3 t, float distance)
    {
        return (distanceFrom(t) < distance);
    }

    public static List<character> getCharacterList()
    {
        return new List<character>(characters);
    }

    public static character getByName(string characterName)
    {
        foreach (character c in characters)
        {
            if (c.name.Equals(characterName))
                return c;
        }
        return null;
    }
    
    ~character()
    {
        lock(characterLock)
            characters.Remove(this);
    }
}
