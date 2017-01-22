using UnityEngine;
using System.Collections;

public class LevelChunk : MonoBehaviour
{
    public float DistanceForNextSpawn;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + DistanceForNextSpawn, transform.position.y, transform.position.z), new Vector3(0.5f, 5f, 40f));
    }
}
