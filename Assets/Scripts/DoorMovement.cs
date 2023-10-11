using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorMovement : MonoBehaviour
{
    public PlayerInteract playerInteract = null;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInteract == null) {
            Debug.Log("DoorMovement requires a reference to a PlayerInteract");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
