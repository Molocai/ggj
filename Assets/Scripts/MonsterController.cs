using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public GameObject Boat;
    public float ChaseRange = 20f;
    public LayerMask TargetLayer;

    public float ChaseMaximumHeight;

    public float MovementSpeed = 0.5f;
    public bool Chasing = false;
    public float ChaseCooldown = 6f;

    public float _cooldownElapsedTime = 0.0f;
    public RandomClipPlayer LostPlayerSounds;

    // Update is called once per frame
    void Update()
    {
        if (Chasing)
        {
            RaycastHit hitInfo;
            Ray r = new Ray(transform.position, (Boat.transform.position - transform.position));
            Physics.Raycast(r, out hitInfo, 1000f, TargetLayer);

            if (Vector3.Distance(transform.position, Boat.transform.position) <= ChaseRange && hitInfo.collider != null && hitInfo.collider.gameObject == Boat)
            {
                Chasing = true;
                Vector3 direction = Boat.transform.position - transform.position;
                direction.y = 0;
                Vector3 newPos = transform.position;

                newPos += direction * MovementSpeed * Time.deltaTime;
                newPos.y = Mathf.Lerp(newPos.y, ChaseMaximumHeight, Time.deltaTime); // transform.position.y;

                transform.LookAt(Boat.transform);
                transform.position = newPos;
            }
        }
        else
        {
            _cooldownElapsedTime += Time.deltaTime;
        }
    }

    public void LeftPlayArea()
    {
        _cooldownElapsedTime = 0.0f;
        Chasing = false;
        if (LostPlayerSounds != null)
            LostPlayerSounds.Play();

        Debug.Log("Left Play area");
    }

    public void TeleportToSpawn(Transform tpTarget)
    {
        if (Chasing || _cooldownElapsedTime < ChaseCooldown) return;

        transform.position = tpTarget.position;
        Chasing = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }
}
