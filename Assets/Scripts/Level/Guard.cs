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

    public void Recall() {
        Debug.Log("Se destruye");
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