using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public GameObject Boat;
    public float ChaseRange = 5f;
    public float AttackRange = 2.5f;
    public LayerMask TargetLayer;

    public float ChaseMaximumHeight;
    public float BaseHeight;

    public float MovementSpeed = 0.5f;
    public float AttackSpeed = 0.9f;
    public bool Chasing = false;
    public float ChaseCooldown = 6f;

    public float _cooldownElapsedTime = 0.0f;
    public RandomClipPlayer LostPlayerSounds;

    public bool WasShown = false;
    public float LighthouseWidth = 0.5f;
    public Vector3 LighthouseLocation;
    public Vector3 LighthouseVector;
    public float VisibleUnderwaterThreshold = 0.1f;
    public RandomClipPlayer JumpscareSounds;

    public AudioSource SwimAudioSource;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Chasing)
        {
            if(!WasShown && IsVisibleUnderwater())
            {
                WasShown = true;
                if(JumpscareSounds != null)
                {
                    JumpscareSounds.Play();
                }
            }

            RaycastHit hitInfo;
            Ray r = new Ray(transform.position, (Boat.transform.position - transform.position));
            Physics.Raycast(r, out hitInfo, 1000f, TargetLayer);

            if (hitInfo.collider != null && hitInfo.collider.gameObject == Boat)
            {
                if(SwimAudioSource != null)
                    SwimAudioSource.Play();
                //Debug.Log("djkslmqdfsé");

                if (Vector3.Distance(transform.position, Boat.transform.position) <= AttackRange)
                {
                    if(_animator != null)
                        _animator.Play("Attack");

                    Vector3 direction = Boat.transform.position - transform.position;
                    direction.y = 0;
                    Vector3 newPos = transform.position;

                    newPos += direction * AttackSpeed * Time.deltaTime;
                    newPos.y = Mathf.Lerp(newPos.y, ChaseMaximumHeight, Time.deltaTime); // transform.position.y;

                    transform.LookAt(Boat.transform);
                    transform.position = newPos;
                }
                if (Vector3.Distance(transform.position, Boat.transform.position) <= ChaseRange)
                {
                    Vector3 direction = Boat.transform.position - transform.position;
                    direction.y = 0;
                    Vector3 newPos = transform.position;

                    newPos += direction * MovementSpeed * Time.deltaTime;
                    newPos.y = Mathf.Lerp(newPos.y, ChaseMaximumHeight, Time.deltaTime); // transform.position.y;

                    transform.LookAt(Boat.transform);
                    transform.position = newPos;
                }
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
        WasShown = false;
        if (LostPlayerSounds != null)
            LostPlayerSounds.Play();

        if(SwimAudioSource != null)
            SwimAudioSource.Stop();

        Debug.Log("Left Play area");
    }

    public void TeleportToSpawn(Transform tpTarget)
    {
        if (Chasing || _cooldownElapsedTime < ChaseCooldown) return;

        transform.position = tpTarget.position;
        transform.position = new Vector3(transform.position.x, BaseHeight, transform.position.z);
        Chasing = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    //Use to isolate a feature, or as cheap replacement for gaussian
    private float cubicImpulse(float c, float w, float x)
    {
        x = Mathf.Abs(x - c);
        if (x > w) return 0.0f;
        x /= w;
        return 1.0f - x * x * (3.0f - 2.0f * x);
    }

    private bool IsVisibleUnderwater()
    {
        Vector3 vect = Vector3.Normalize(transform.position - LighthouseLocation);
        float val = cubicImpulse(1.0f, LighthouseWidth, Mathf.Abs(Vector3.Dot(vect, LighthouseVector)));
        return val > VisibleUnderwaterThreshold;
    }
}
