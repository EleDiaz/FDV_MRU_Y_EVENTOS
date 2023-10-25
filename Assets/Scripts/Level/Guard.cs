using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float radiusSeparation = 1f;

    public void SetTarget(GameObject t) {
        target = t;
    }

    // NOTE: This is a tricky function to be linked into an event. Bcs at the end of it the object 
    // no longer will live so we need to unlink function to the event dispatcher.
    public void Recall(Guards guards) {
        guards.withDraw -= Recall;
        Destroy(gameObject);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        var direction = target.transform.position - transform.position;

        if (direction.magnitude < radiusSeparation)
        {
            return;
        }

        transform.LookAt(target.transform);

        transform.Translate(direction.normalized * Time.deltaTime * speed, Space.World);
    }
}