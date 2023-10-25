using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using UnityEngine;

public class PointGeneration : MonoBehaviour
{

    private Collider _collider;


    [SerializeField]
    private GameObject point;

    [SerializeField]
    private float spacing = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();

        GenerateInsideBounds(_collider.bounds);
    }

    // TODO: Do not change scale, bcs it will be heritage by their childrens.
    void GenerateInsideBounds(Bounds bounds) {
        var min = bounds.min;
        var max = bounds.max;

        var centerX = spacing / 2 * (Mathf.Abs(max.x - min.x) % spacing / spacing);
        var centerZ = spacing / 2 * (Mathf.Abs(max.z - min.z) % spacing / spacing);
        for (float i = min.x; i < max.x; i += spacing)
        {
            for (float j = min.z; j < max.z; j += spacing) {
                var gameObject = Instantiate(point, new Vector3(centerX + i, transform.position.y, centerZ + j), Quaternion.identity, transform);
                gameObject.name = "Point " + i;
            }
        }
    }
}
