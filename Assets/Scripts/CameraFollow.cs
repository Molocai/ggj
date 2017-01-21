using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;

    private Vector3 OffSet;

    void Start()
    {
        if (Target)
            OffSet = transform.position - Target.transform.position;
        else
            Debug.LogWarning("You need to define a target for the camera to follow");
    }

    void Update()
    {
        if (Target)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, Target.transform.position + OffSet, Time.deltaTime * 1f);
            transform.position = newPos;
        }
    }
}
