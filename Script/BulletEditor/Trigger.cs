using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    public Transform _isShowSwitch;
    public Transform _cubes;

    public void OnClick()
    {
        int i = 0;
        foreach(Transform child in _isShowSwitch){
            bool tr = child.GetComponent<Toggle>().isOn;
            if(tr){
                _cubes.GetChild(i).GetComponent<PoolObjectsToList>().EnableFalsePoolObj();
            }
            i++;
        }
    } 
}
