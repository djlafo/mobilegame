using UnityEngine;
using System.Collections;

public class teleportTo : MonoBehaviour {

    [SerializeField]
    GameObject target;

	void OnTriggerEnter2D(Collider2D c)
    {
        float oldZ = c.gameObject.transform.position.z;
        c.gameObject.transform.position = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y, c.gameObject.transform.position.z);
        observer.Inst.invoke("playerTeleport", null);
    }
}
