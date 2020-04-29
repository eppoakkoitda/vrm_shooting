using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof (Dropdown))]
public class DirToDropdownList : MonoBehaviour
{
    [SerializeField] string _dirPass;
    [SerializeField] string _extension;
    

    void Start()
    {
        var dropdown = GetComponent<Dropdown>();
        dropdown.options.Clear();
        string[] files = Directory.GetFiles(Application.dataPath + "/../" + _dirPass, "*." + _extension);
        foreach(string pass in files){
            dropdown.options.Add(new Dropdown.OptionData { text = Path.GetFileNameWithoutExtension(pass) });
        }
        
        dropdown.RefreshShownValue();

    }

}
