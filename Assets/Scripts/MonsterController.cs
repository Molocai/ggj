using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public GameObject Boat;
    public float ChaseRange = 20f;
    public LayerMask TargetLayer;

    public float MovementSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Ray r = new Ray(transform.position, (Boat.transform.position - transform.position));
        Physics.Raycast(r, out hitInfo, 1000f, TargetLayer);

        if (Vector3.Distance(transform.position, Boat.transform.position) <= ChaseRange && hitInfo.collider != null && hitInfo.collider.gameObject == Boat)
        {
            Vector3 direction = Boat.transform.position - transform.position;
            Vector3 newPos = transform.position;

            newPos += direction * MovementSpeed * Time.deltaTime;

            transform.LookAt(Boat.transform);
            transform.position = newPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }
}
