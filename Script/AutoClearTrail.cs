using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClearTrail : MonoBehaviour
{
    private TrailRenderer _trail;
    void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    void OnDisable()
    {
        _trail.Clear();
    }
}
