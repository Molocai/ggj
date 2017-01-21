using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    public float LeftRightMaxDistance = 20f;

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

            // Make it so the camera won't follow after we're out of bounds
            if (newPos.z > LeftRightMaxDistance)
                newPos.z = LeftRightMaxDistance;
            else if (newPos.z < -LeftRightMaxDistance)
                newPos.z = -LeftRightMaxDistance;

            transform.position = newPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (Target)
        {
            Gizmos.DrawLine(new Vector3(-30f, 3f, 20f), new Vector3(30f, 3f, 20f));
            Gizmos.DrawLine(new Vector3(-30f, 3f, -20f), new Vector3(30f, 3f, -20f));
        }
    }
}
