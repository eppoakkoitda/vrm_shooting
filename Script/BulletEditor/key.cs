using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public void OnClick()
    {
        int id = transform.GetInstanceID();
        transform.parent.parent.GetComponent<AnimationCurveEditor>().OnClickKey(id);
    }
}
