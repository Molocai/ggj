using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject[] LevelChunks;
    public float NextChunkDistance;

    private void OnTriggerEnter(Collider other)
    {
        GameObject chunk = LevelChunks[Random.Range(0, LevelChunks.Length - 1)];
        
        GameObject.Instantiate(LevelChunks[Random.Range(0, LevelChunks.Length - 1)], new Vector3(transform.position.x + NextChunkDistance, transform.position.y, transform.position.z), Quaternion.identity);
        transform.Translate(new Vector3(chunk.GetComponent<LevelChunk>().DistanceForNextSpawn, 0));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + NextChunkDistance, transform.position.y, transform.position.z), new Vector3(0.5f, 5f, 40f));
    }
}
