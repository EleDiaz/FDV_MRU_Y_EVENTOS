using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorMovement : MonoBehaviour
{
    public PlayerInteract playerInteract = null;

    public GameObject finalDoorPosition = null;

    private bool _openingDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInteract == null) {
            Debug.Log("DoorMovement requires a reference to a PlayerInteract");
        }

        playerInteract.trigger += OpenDoor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_openingDoor) {
            transform.Translate((finalDoorPosition.transform.position - transform.position).normalized * Time.deltaTime);
        }
    }

    public void OpenDoor() {
        _openingDoor = true;
    }
}
