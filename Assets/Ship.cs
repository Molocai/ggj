using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newPos.y = SeaCalculator.GetWorldHeight(transform.position);

        if (Input.GetKey(KeyCode.Z))
            newPos.x += Time.deltaTime * 3f;

        if (Input.GetKey(KeyCode.S))
            newPos.x -= Time.deltaTime * 3f;

        if (Input.GetKey(KeyCode.Q))
            newPos.z += Time.deltaTime * 3f;

        if (Input.GetKey(KeyCode.D))
            newPos.z -= Time.deltaTime * 3f;

        transform.position = newPos;
    }
}
