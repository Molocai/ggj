using UnityEngine;
using System.Collections;

public class ShipRaycast : MonoBehaviour
{
    public LayerMask TargetLayer;
    public float RaycastDistance = 20f;
    public MonsterController monsterController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Ray r = new Ray(transform.position, transform.forward);

        Physics.Raycast(r, out hitInfo, RaycastDistance, TargetLayer);
        Debug.DrawRay(r.origin, r.direction * RaycastDistance, Color.cyan);

        if (hitInfo.collider != null)
        {
            if(monsterController != null)
            {
                monsterController.TeleportToSpawn(hitInfo.transform);
            }   
        }
    }
}
