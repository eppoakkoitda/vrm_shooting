using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour, IUpdatable
{
    // パラメーター
    [SerializeField] private HP _hp;
    private Transform[] _enemyTransforms;
    [SerializeField] Transform _radar;

    // ビュー
    [SerializeField] private Slider _slider;

    // 内部変数
    Vector3 _zAngle = Vector3.zero;
    Vector2 _screenPosition;
    Vector3 _enemy, _player, _mae, _toTarget;
    float _angle;

    private Vector2 _origin = new Vector2(Screen.width/2, Screen.height/2);

    void Awake()
    {
        _hp.SetListener(ChangedHp);
    }

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    int ChangedHp(float hp)
    {
        _slider.value = hp/_hp.maxHp;
        return 0;
    }

    public void SetEnemies(Wave wave)
    {
        _enemyTransforms = wave.GetChildTransforms();
    }

    public void UpdateMe()
    {

        for(int i = 0; i < _radar.childCount; ++i){

            if(i >= _enemyTransforms.Length){
                _radar.GetChild(i).gameObject.SetActive(false);
                continue;
            }else{
                _radar.GetChild(i).gameObject.SetActive(true);
            }


            _enemy = _enemyTransforms[i].position;
            _enemy.y = 0;
            _player = Camera.main.transform.position;
            _player.y = 0;
            _mae = Camera.main.transform.forward;
            _mae.y = 0;

            _toTarget = _enemy - _player;
            _angle = Vector3.Angle(_mae, _toTarget);

            if(Vector3.Cross(_mae, _toTarget).y < 0) _angle = 360 - _angle;

            _zAngle.z = -_angle;
            _radar.GetChild(i).eulerAngles = _zAngle;

            _screenPosition.x = -Mathf.Cos((_angle+90)*Mathf.Deg2Rad);
            _screenPosition.y = Mathf.Sin((_angle+90)*Mathf.Deg2Rad);
            //print(i + ": " + angle + ", " + screen);

            _radar.GetChild(i).position = _origin + 300 * _screenPosition;
            
        }
    }


}
