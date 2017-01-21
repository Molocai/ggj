using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SeaCalculator
{
    public static float Speed = 2f;
    public static float Scale = 0.2f;
    public static float NoiseStrength = 1f;

    public static float GetWorldHeight(Vector3 vertice)
    {
        float height = 0f;
        height = Mathf.Sin(Time.time * Speed + vertice.x + vertice.z) * Scale;
        //height += Mathf.PerlinNoise(vertice.x, vertice.y + Mathf.Sin(Time.time * 0.1f) * NoiseStrength);

        return height;
    }

}
