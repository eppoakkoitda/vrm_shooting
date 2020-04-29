using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスの位置にオブジェクトを移動させる　奥行きは指定する

public class MoveByMouse : MonoBehaviour, IUpdatable
{
    [SerializeField] private float _mouseZ = 100f;
    [SerializeField] float _correction = 0.5f;
    private Vector3 _origin = new Vector3(Screen.width/2, Screen.height/2, 0);
    private Vector3 _mousePos;

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }
    public void UpdateMe()
    {
        _mousePos = Input.mousePosition;
        
        Vector3 diff = _mousePos - _origin;
        _mousePos += _correction*diff;
        _mousePos.z = _mouseZ;
        
        transform.position = Camera.main.ScreenToWorldPoint(_mousePos);
        //print(_camera.ScreenToWorldPoint(_mousePos) + "\n" + Camera.main.ScreenToWorldPoint(_mousePos));
        //transform.position += _mouseZ*Camera.main.transform.forward;
        // if(_correction != null){
        //     Vector3 diff = _correction.position - Camera.main.transform.position;
        //     transform.GetChild(0).localPosition = 2*diff;
        // }
    }
}
