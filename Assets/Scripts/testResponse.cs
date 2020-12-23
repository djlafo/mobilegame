using UnityEngine;
using System.Collections.Generic;

public class testResponse : MonoBehaviour {

    character c;
    characterReaction reactions;

    [SerializeField]
    string npcName;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        c = new character(npcName, gameObject, 3);
        c.talkedTo += C_talkedTo;

        reactions = new characterReaction();

        reactions.add(characterReaction.statementType.greeting, "Hello");
        reactions.add(characterReaction.statementType.compliment, "Thank you");
        reactions.add(characterReaction.statementType.farewell, "Bye");
        reactions.add(characterReaction.statementType.insult, "You are a very mean person");
        reactions.add(characterReaction.statementType.sarcasm, "I don't need your sarcasm");
        reactions.add(characterReaction.statementType.apology, "I accept the apology");
        reactions.add(characterReaction.statementType.thank, "You're welcome");
        reactions.add(characterReaction.statementType.question, "I don't know the answer to that");
        reactions.add(characterReaction.statementType.sellOffer, "I don't want to buy anything");
        reactions.add(characterReaction.statementType.buyOffer, "I'm not selling anything");
        reactions.add(characterReaction.statementType.tradeOffer, "I'm broke");
        reactions.add(characterReaction.statementType.unknown, "...");
    }

    private void C_talkedTo(character ch, string speech)
    {
        anim.SetInteger("Direction", c.directionTo(ch.getObject().transform.position));

        bool reacted = false;
        if (reactions.getStatementType(speech) == characterReaction.statementType.question)
        {
            string speechLower = speech.ToLower();
            if (speechLower.Contains("move"))
            {
                speechQueue.Inst.add(npcName, "I am entirely too lazy for that");
                speechQueue.Inst.add(npcName, "However, you can push me out of the way if you really want to");
                reacted = true;
            }
        }


        if(!reacted)
            speechQueue.Inst.add(npcName, reactions.reactTo(speech, true));

        speechQueue.Inst.begin();
    }


    bool hasCollided = false;

    void OnCollisionEnter2D(Collision2D co)
    {
        if(co.collider.tag=="Player" && !hasCollided)
        {
            hasCollided = true;
            speechQueue.Inst.add(npcName, "Hey, watch it buddy.");
            speechQueue.Inst.begin();
        }

        if(co.collider.tag=="Player" && hasCollided)
        {
            character ch = character.getByName("Player");
            anim.SetInteger("Direction", c.directionTo(ch.getObject().transform.position));
            switch(ch.Direction)
            {
                case 1:
                    co.rigidbody.AddForce(new Vector2(0f, -80f));
                    break;
                case 2:
                    co.rigidbody.AddForce(new Vector2(-80f, 0f));
                    break;
                case 3:
                    co.rigidbody.AddForce(new Vector2(0f, 80f));
                    break;
                case 4:
                    co.rigidbody.AddForce(new Vector2(80f, 0f));
                    break;
            }
        }
    }
}
