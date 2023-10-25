using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public delegate void SwitchTrigger();
    public event SwitchTrigger trigger;

    public int scoreCost = 3;

    private GameplaySystem _gameplay;

    // Start is called before the first frame update
    void Start()
    {
        _gameplay = GameSystem.Instance.GetComponent<GameplaySystem>();
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Player") {
            if (_gameplay.Score >= scoreCost) {
                _gameplay.Score -= scoreCost;
                trigger.Invoke();
            }
        }
    }
}
