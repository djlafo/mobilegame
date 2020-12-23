using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class chatScript : MonoBehaviour {

    Text text;

    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("InputField"));
    }

    public void submitSpeech()
    {
        if (text.text.Equals(""))
            return;

        character player = character.getByName("Player");
        List<character> chars = character.getCharacterList();

        float shortestDist = 100f;
        character poss = null;

        foreach (character c in chars)
        {
            if (c.inDistance(player.getObject().transform.position, 1.5f) && !c.getName().Equals("Player"))
            {
                if (player.directionTo(c.getObject().transform.position) == player.Direction && player.distanceFrom(c.getObject().transform.position) < shortestDist)
                {
                    poss = c;
                    shortestDist = player.distanceFrom(c.getObject().transform.position);
                }
            }
        }

        if (poss != null)
        {
            poss.conversationFrom(player, text.text);
        } else
        {
            gameManager.Inst.setControls(true);
        }

        Destroy(gameObject);
    }
}
