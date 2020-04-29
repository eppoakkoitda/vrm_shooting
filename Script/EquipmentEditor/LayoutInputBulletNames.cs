using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutInputBulletNames : MonoBehaviour
{
    // -----------------------------------------------------------------
    // インスペクター

    [SerializeField] InputBulletNames _inputBulletNames;
    [SerializeField] float _height = 0;
    [SerializeField] float _width = 0;

    // -----------------------------------------------------------------

    void Start()
    {
        _height = _height * Screen.height / 720f;
        _width = _width * Screen.width / 1280f;
        int i = 0;
        foreach(Transform child in _inputBulletNames.transform){
            child.position += new Vector3(_width*i, _height*i, 0);
            i++;
        }
    }
}
