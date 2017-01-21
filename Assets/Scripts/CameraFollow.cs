using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;

    private Vector3 OffSet;

    void Start()
    {
        OffSet = transform.position - Target.transform.position;
    }

    void Update()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, Target.transform.position + OffSet, Time.deltaTime * 1f);
        transform.position = newPos;
    }
}
