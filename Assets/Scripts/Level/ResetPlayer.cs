using System;
using UnityEngine;


// Spawn a Guard that will follow you after the player getting far from the playable zone.
public class ResetPlayer : MonoBehaviour {

    private Vector3 initialPosition;

    private Rigidbody _rigidbody;

    void Start() {
        initialPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetPosition() {
    }
}