using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseController : MonoBehaviour
{
    public float RotationSpeed = 2f;
    public GameObject Boat;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
        Vector3 newPos = new Vector3(Boat.transform.position.x + 40, transform.position.y, Boat.transform.position.z);
        transform.position = newPos;
    }
}
