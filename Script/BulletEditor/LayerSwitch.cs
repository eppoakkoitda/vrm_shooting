using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerSwitch : MonoBehaviour
{
    public BulletEditor _bulletEditor;
    public Transform _buttons;
    public ParameterEditor _parameterEditor;
    public CreateBullet _createBullet;
    public Transform _cubes;
    private int _now = 1;
    private string[] _names = new string[5];

    public void OnClickButton(int i)
    {
        _bulletEditor.Load();

        _createBullet._poolObjectsToList = _cubes.GetChild(i-1).GetComponent<PoolObjectsToList>();
        // _bulletEditor.Save();
        _names[_now-1] = _bulletEditor._bulletData._name;
        _bulletEditor._bulletData._name = _names[i-1];
        _bulletEditor.Load();
        // _parameterEditor.OnLoadBulletData();
        _now = i;

        Color selectedColor = new Color32(125,125,125,255);
        Color normalColor = new Color32(255,255,255,255);

        int index = 1;
        foreach(Transform child in _buttons){
            if(index == i){
                child.GetComponent<Image>().color = selectedColor;
            }else{
                child.GetComponent<Image>().color = normalColor;
            }
            index++;
        }

    }
}
