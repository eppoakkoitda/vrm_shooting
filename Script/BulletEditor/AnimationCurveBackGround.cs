using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCurveBackGround : MonoBehaviour
{ 
    [System.Serializable]
    public class CustomUnityEvent : UnityEvent<Vector2>{}
    public CustomUnityEvent ON_CLICK = new CustomUnityEvent();
    public void OnPointClick()
    {
        // print("clickdown");
        ON_CLICK.Invoke(Input.mousePosition);
    }
}
