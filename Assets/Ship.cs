using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Transform Nose;
    public Transform Tail;

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

        Vector3 noseNewPos = new Vector3(Nose.position.x, Nose.position.y, Nose.position.z);
        Vector3 tailNewPos = new Vector3(Tail.position.x, Tail.position.y, Tail.position.z);

        noseNewPos.y = SeaCalculator.GetWorldHeight(Nose.position);
        tailNewPos.y = SeaCalculator.GetWorldHeight(Tail.position);  

        Vector3 magicVector = noseNewPos - tailNewPos;

        Debug.DrawLine(transform.position, transform.position + magicVector);

        //Quaternion pouet = Quaternion.FromToRotation(transform.up, magicVector);
        //Quaternion pouet = Quaternion.LookRotation(magicVector, transform.up);

        //transform.rotation = pouet;

        transform.LookAt(transform.position + magicVector);
    }
}
