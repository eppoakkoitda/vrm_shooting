using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class SeenValue : MonoBehaviour {
    protected Func<float, int> Call;
    
    public void SetListener(Func<float, int> f)
    {
        Call = f;
    }

}