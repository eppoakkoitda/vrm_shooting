using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPickerCtr : MonoBehaviour
{
    public GameObject _colorPicker;
    private GameObject _go;
    private ColorPickerTriangle _colorPickerTriangle;
    private bool _isShow = false;
    public InputField _inputField;

    [System.Serializable]
    public class CustomUnityEvent : UnityEvent<Color>{}
    public CustomUnityEvent ON_UPDATE = new CustomUnityEvent();

    public void ShowColorPicker(){
        if(_go == null){
            _go = Instantiate( _colorPicker, Camera.main.transform.position, Quaternion.identity);
            _go.transform.position += Camera.main.transform.forward * 2f;
            _go.transform.LookAt(Camera.main.transform.position);
            _colorPickerTriangle = _go.GetComponent<ColorPickerTriangle>();
        }else{
            _go.SetActive(true);
        }
        // _colorPickerTriangle.SetNewColor();
    }

    public void HideColorPicker(){
        _go.SetActive(false);
    }

    public void SwitchIsShow()
    {
        if(_isShow){
            _isShow = false;
            HideColorPicker();
            ON_UPDATE.Invoke(_colorPickerTriangle.TheColor);
        }else{
            _isShow = true;
            ShowColorPicker();
        }
    }

    void Update()
    {
        if(_isShow){
            _inputField.text = _colorPickerTriangle.TheColor.ToString();
            _inputField.GetComponent<Image>().color = _colorPickerTriangle.TheColor;
        }
    }
}
