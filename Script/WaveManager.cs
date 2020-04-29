using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public void ChangeWave()
    {
        // print("hantei: " + transform.childCount);
        for(int i = 0; i < transform.childCount; ++i){
            if(transform.GetChild(i).gameObject.activeSelf){
                transform.GetChild(i).gameObject.SetActive(false);
                if(i != transform.childCount-1){
                    transform.GetChild(i+1).gameObject.SetActive(true);
                    return;
                }
            }
        }
    }


}
