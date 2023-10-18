using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public Transform teleportLocation;


    private void OnTriggerEnter(Collider collider) {
        Debug.Log("Trigger");
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Trigger2");
            collider.gameObject.transform.position = teleportLocation.position;
            // var rb = collider.gameObject.GetComponent<Rigidbody>();
            // if (rb != null) {
            //     Debug.Log("Trigger3");
            //     rb.MovePosition(teleportLocation.position);
            // }
        }
    }
}
