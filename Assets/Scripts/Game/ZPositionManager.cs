using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionManager : MonoBehaviour
{

    public Transform referencePoint;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, referencePoint.position.y * 0.5f);
    }
}
