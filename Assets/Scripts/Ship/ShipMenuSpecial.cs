using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMenuSpecial : MonoBehaviour
{
    public Transform Nose;
    public Transform Tail;

    void Update()
    {
        ApplyBuoyancy();
        TiltBoat();
    }

    private void ApplyBuoyancy()
    {
        // Calculate the new height according to sea level
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newPos.y = SeaCalculator.GetWorldHeight(transform.position);

        // Apply it
        transform.position = newPos;
    }

    private void TiltBoat()
    {
        // Retrieve current nose/tail position
        Vector3 noseNewPos = new Vector3(Nose.position.x, Nose.position.y, Nose.position.z);
        Vector3 tailNewPos = new Vector3(Tail.position.x, Tail.position.y, Tail.position.z);

        // Calculate the new height according to sea level
        noseNewPos.y = SeaCalculator.GetWorldHeight(Nose.position);
        tailNewPos.y = SeaCalculator.GetWorldHeight(Tail.position);

        Vector3 direction = noseNewPos - tailNewPos;

        transform.LookAt(transform.position + direction);
    }
}
