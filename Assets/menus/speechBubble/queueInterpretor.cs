using UnityEngine.UI;
using UnityEngine;

public class queueInterpretor : MonoBehaviour {

    Text author;
    Text comment;

	// Use this for initialization
	void Start () {
        author = GameObject.Find("author").GetComponent<Text>();
        comment = GameObject.Find("comment").GetComponent<Text>();
        updateBubble();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {
            updateBubble();
        } 
	}

    void updateBubble()
    {
        speechQueue.speech s = speechQueue.Inst.getNext();
        if(s!= null)
        {
            author.text = s.name;
            comment.text = s.comment;
        } else
        {
            Application.UnloadLevel("speechBubble");
            observer.Inst.invoke("speechQueueFinished", null);
        }
    }
}
