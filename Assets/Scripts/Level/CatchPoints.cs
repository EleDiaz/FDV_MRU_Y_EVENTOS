using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPoints : MonoBehaviour
{
    private GameplaySystem _gameplay;

    void Start() {
        _gameplay = GameSystem.Instance.GetComponent<GameplaySystem>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_gameplay != null)
            {
                _gameplay.Score += 1;
                Destroy(gameObject);
            }
        }
    }
}