using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject[] LevelChunks;
    public LevelChunk LastChunk;

    private void OnTriggerEnter(Collider other)
    {
        GameObject chunk = LevelChunks[Random.Range(0, LevelChunks.Length - 1)];
        GameObject.Instantiate(chunk, new Vector3(transform.position.x + LastChunk.DistanceForNextSpawn, transform.position.y, transform.position.z), Quaternion.identity);

        transform.Translate(new Vector3(LastChunk.DistanceForNextSpawn, 0));

        LastChunk = chunk.GetComponent<LevelChunk>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
    }
}
