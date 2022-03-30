using System;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] Points { get; private set; }

    private void Awake()
    {
        int childCount = transform.childCount;
        Points = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }
}
