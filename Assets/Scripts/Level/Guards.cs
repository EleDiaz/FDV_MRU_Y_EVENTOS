using System;
using System.Collections;
using UnityEngine;


// Spawn a Guard that will follow you after the player getting far from the playable zone.
public class Guards : MonoBehaviour {

    [SerializeField]
    private GameObject watchOver;

    [SerializeField]
    private GameObject guard;

    private bool outside = false;

    public float spawnRate = 3f;

    public delegate void WithDraw();

    public event WithDraw withDraw;


    void OnTriggerEnter(Collider collision) {
        Debug.Log("entramos");
        Debug.Log(outside);
        if (collision.gameObject == watchOver) {
            outside = false;
            withDraw();
        }
    }

    void OnTriggerExit(Collider collision) {
        if (collision.gameObject == watchOver) {
            outside = true;
            StartCoroutine(Spawner());
        }
    }

    void SpawnGuard() {
        var go = Instantiate(guard, watchOver.transform.position + Vector3.forward * 3, Quaternion.identity, transform);
        var gobj = go.GetComponent<Guard>();
        if (gobj != null) {
            gobj.SetTarget(watchOver);
            withDraw += gobj.Recall;
        }
    }

    IEnumerator Spawner() {
        if (outside) {
            yield return new WaitForSeconds(spawnRate);
            SpawnGuard();
            yield return Spawner();
        }
    }
}