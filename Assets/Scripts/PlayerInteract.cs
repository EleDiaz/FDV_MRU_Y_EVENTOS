using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private static string PLAYER_TAG = "Player";
    public delegate void SwitchTrigger();
    public event SwitchTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionTrigger(Collision collision) {
        if (collision.gameObject.tag == PLAYER_TAG) {
            trigger.Invoke();
        }
    }
}

