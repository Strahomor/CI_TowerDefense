using UnityEngine;

public class EndPoints : GameManager
{
    public static Transform[] endpoints;

    void Awake()
    {
        endpoints = new Transform[transform.childCount];
        for (int i = 0; i < endpoints.Length; i++)
        {
            endpoints[i] = transform.GetChild(i);
        }
    }
}

