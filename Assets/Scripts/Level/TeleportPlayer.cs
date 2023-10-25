using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public Transform teleportLocation;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            // We use the rigidbody position to teleport the player.
            // You could use the transform but if would be slower.
            var rb = collider.gameObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.position = teleportLocation.position;
                StartCoroutine(Teleport());
            }
        }
    }

    IEnumerator Teleport() {

        yield return new WaitForSeconds(2);

    }
}
