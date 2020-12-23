using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class playerController : MonoBehaviour {
    Animator anim;
    Rigidbody2D mrb;

    [SerializeField]
    float movementSpeed;

    character c;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        mrb = GetComponent<Rigidbody2D>();
        c = new character("Player", gameObject, 3);
        gameManager.reset();
        observer.Inst.listen("playerTeleport", onTeleport);
	}

    void onTeleport(List<object> parameters)
    {
        StartCoroutine(teleportDelay());
    }

    IEnumerator teleportDelay()
    {
        gameManager.Inst.setControls(false);
        anim.SetInteger("Direction", 3);
        anim.SetBool("Idle", true);
        c.Direction = 3;
        anim.Play("Walk_Down");
        yield return new WaitForSeconds(.25f);
        gameManager.Inst.setControls(true);
    }
	
	// Update is called once per frame
	void Update () {

        if (!gameManager.Inst.ControlsEnabled)
            return;

        // chat
        if(Input.GetKeyDown(KeyCode.T))
        {
            gameManager.Inst.setControls(false);
            Application.LoadLevelAdditiveAsync("chat");
        }

        // movement
        if(Math.Abs(Input.GetAxis("Horizontal")) > Math.Abs(Input.GetAxis("Vertical"))) // horizontal movement
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetInteger("Direction", 2);
                c.Direction = 2;
                anim.Play("Walk_Right"); // for instant transition
                transform.Translate(movementSpeed, 0f, 0f);
            } else if (Input.GetAxis("Horizontal") < 0)
            {
                anim.SetInteger("Direction", 4);
                c.Direction = 4;
                anim.Play("Walk_Left");
                transform.Translate(-movementSpeed, 0f, 0f);
            }
            anim.SetBool("Idle", false);
        } else // vertical or idle
        {

            if(Input.GetAxis("Vertical") > 0)
            {
                anim.SetInteger("Direction", 1);
                c.Direction = 1;
                anim.SetBool("Idle", false);
                anim.Play("Walk_Up");
                transform.Translate(0f, movementSpeed, 0f);
            } else if (Input.GetAxis("Vertical") < 0)
            {
                anim.SetInteger("Direction", 3);
                c.Direction = 3;
                anim.SetBool("Idle", false);
                anim.Play("Walk_Down");
                transform.Translate(0f, -movementSpeed, 0f);

            } else // idle
            {
                anim.SetBool("Idle", true);
            }
        }
	}
}
