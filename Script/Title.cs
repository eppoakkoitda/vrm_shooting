using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void OnClickButton(int i)
    {
        switch(i){
            case 0:
                SceneManager.LoadScene("EquipmentEditScene");
                break;
            case 1:
                SceneManager.LoadScene("EditScene");
                break;
            case 2:
                Application.Quit();
                break;

        }
    }
}
