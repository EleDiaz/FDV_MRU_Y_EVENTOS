using System;
using UnityEngine;


// Spawn a Guard that will follow you after the player getting far from the playable zone.
public class Guards : MonoBehaviour {

    [SerializeField]
    private GameObject watchOver;

    [SerializeField]
    private GameObject guard;

    public delegate void WithDraw();

    public event WithDraw withDraw;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == watchOver) {
            withDraw();
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject == watchOver) {
            SpawnGuard();
        }
    }

    void SpawnGuard() {
        // TODO: instance guards around player
    }

}