using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipmentEditor : MonoBehaviour
{
    public EquipmentData _equipmentData;
    [SerializeField] Transform _inputBulletNames;
    [SerializeField] Dropdown _VRMDropdown;
    // [SerializeField] UnityEvent ON_END_LOAD = new UnityEvent();
    void Start()
    {
        Load();
    }
    public void Save()
    {
        string jsonstr = JsonUtility.ToJson(_equipmentData);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/../equipment.json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();

        OnSaved();
    }

    private void OnSaved()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void Load()
    {
        try{
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/../equipment.json");
            string jsonstr = reader.ReadToEnd();
            reader.Close();
            _equipmentData = JsonUtility.FromJson<EquipmentData>(jsonstr);
            OnLoaded();
            // ON_END_LOAD.Invoke();
        }catch (Exception e){
            // print(e);
        }
    }

    private void OnLoaded()
    {
        Dropdown dropdown;

        int bulletIndex = 0;
        foreach(Transform child in _inputBulletNames){
            dropdown = child.GetComponent<Dropdown>();

            for(int i = 0; i < dropdown.options.Count; ++i){
                if(dropdown.options[i].text == _equipmentData.bullets[bulletIndex]){
                    dropdown.value = i;
                }
            }

            bulletIndex++;
            
        }

        for(int i = 0; i < _VRMDropdown.options.Count; ++i){
            if(_VRMDropdown.options[i].text == _equipmentData.modelPass){
                _VRMDropdown.value = i;
            }
        }
    }

    public void OnChangedDropdown(Dropdown dropdown)
    {
        var val = dropdown.value;
        var index = int.Parse(dropdown.name);

        _equipmentData.bullets[index] = dropdown.options[val].text;

    }

    public void OnChangedVRMDropdown(Dropdown dropdown)
    {
        var val = dropdown.value;
        // var index = int.Parse(dropdown.name);

        _equipmentData.modelPass = dropdown.options[val].text;

    }

}
