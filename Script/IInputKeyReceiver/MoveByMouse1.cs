using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスの位置にオブジェクトを移動させる　奥行きは指定する

public class MoveByMouse1 : MonoBehaviour, IInputKeyReceiver
{
    [SerializeField] private float _mouseZ = 100f;
    [SerializeField] float _correction = 0.5f;
    private Vector3 _origin = new Vector3(Screen.width/2, Screen.height/2, 0);
    private Vector3 _mousePos, _diff;

    void OnEnable()
    {
        InputKeySender.Add(this);
    }
    public void OnKeys(KeyEvent e)
    {
        _mousePos = e.MousePosition;
        
        _diff = _mousePos - _origin;
        _mousePos += _correction*_diff;
        _mousePos.z = _mouseZ;
        
        transform.position = Camera.main.ScreenToWorldPoint(_mousePos);
    }
}
