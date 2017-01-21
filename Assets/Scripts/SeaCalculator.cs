using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCalculator : Singleton<SeaCalculator>
{
    public float Speed = 2f;
    public float Scale = 0.2f;
    public float NoiseStrength = 1f;

    protected SeaCalculator() { }

    public float GetWorldHeight(Vector3 vertice)
    {
        float height = 0f;
        height = Mathf.Sin(Time.time * Speed + vertice.x + vertice.z) * Scale;
        //height += Mathf.PerlinNoise(vertice.x, vertice.y + Mathf.Sin(Time.time * 0.1f) * NoiseStrength);

        return height;
    }

}
