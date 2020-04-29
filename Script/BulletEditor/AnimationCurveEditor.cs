using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCurveEditor : MonoBehaviour
{
    [SerializeField] Transform _keys;
    [SerializeField] AnimationCurve _animationCurve;  
    public BulletEditor _bulletEditor;
    private GameObject _evaluatePoints;
    [SerializeField] Transform _backGround;
    public UnityEvent ON_END_EDIT = new UnityEvent();
    private float _nazo;

    void Start()
    {
        _nazo = 2048f / Screen.width;
    }

    public void OnClickBackGround(Vector2 vec2)
    {
        GameObject key = Instantiate(Resources.Load<GameObject>("Prefab/key"));
        key.transform.parent = _keys.transform;
        key.transform.localScale = Vector3.one * 0.3f;
        // key.transform.localPosition = Vector3.zero;
        key.transform.position = transform.position + new Vector3(
            vec2.x - transform.position.x,  // ワールド座標
            vec2.y - transform.position.y,
            0
        );
        // AnimationCurve座標
        float a_time = ((vec2.x - _backGround.position.x)/(_backGround.localScale.x/_nazo) + 50f)/100f;
        float a_value = ((vec2.y - _backGround.position.y)/_backGround.localScale.y + 50f)/100f;
        _animationCurve.AddKey(a_time, a_value - 0.5f);
        key.name = a_time.ToString();    

        DrawLine();

    }

    private void DrawLine()
    {
        // print("localscaleX: " + _backGround.localScale.x);
        if(_evaluatePoints == null){
            _evaluatePoints = new GameObject("EvaluatePoints");
            _evaluatePoints.transform.parent = _backGround;
            for(int i = 0; i < 200; ++i){
                GameObject point = Instantiate(Resources.Load<GameObject>("Prefab/point"));
                point.transform.parent = _evaluatePoints.transform;
                point.transform.localPosition = Vector3.zero;
                point.transform.localScale = Vector3.one * 0.08f;
            }
        }

        float time = 0;
        foreach(Transform point in _evaluatePoints.transform){
            float val = _animationCurve.Evaluate(time);
            // print("out -> time: " + time + ", val: " + val);
            point.position = _backGround.position + new Vector3(
                (time-0.5f)*100*(_backGround.localScale.x/_nazo),
                val*100*_backGround.localScale.y,
                0
            );
            time += 0.005f;
        }

        Save();
    }

    public void OnClickKey(int id)
    {
        print("clicked: " + name);
        // int i = 0;
        // foreach(Transform key in _keys){
        //     if(key.GetInstanceID() == id){        
        //         Destroy(key.gameObject);
        //         break;
        //     }
        //     i++;
        // }

        List<Transform> objList = new List<Transform>();
        foreach (Transform key in _keys) {
            objList.Add(key);
        }
 
        objList.Sort((obj1, obj2) => string.Compare(obj1.name, obj2.name));
 
        // ソート結果順にGameObjectの順序を反映
        int index = 0;
        foreach (Transform key in objList ) {
           if(key.GetInstanceID() == id){        
                Destroy(key.gameObject);
                break;
            }
            index++;
        }

        _animationCurve.RemoveKey(index);

        DrawLine();
    }

    public void Load()
    {
        // 現在のキーを削除
        if(_keys.childCount != 0){
            foreach(Transform key in _keys){
                Destroy(key.gameObject);
            }
        }

        _animationCurve = _bulletEditor._bulletData.animationCurve;

        foreach(Keyframe keyframe in _animationCurve.keys){
            GameObject key = Instantiate(Resources.Load<GameObject>("Prefab/key"));
            key.transform.parent = _keys.transform;
            key.transform.localScale = Vector3.one * 0.3f;
            
            key.transform.position = _backGround.position + new Vector3(
                (keyframe.time-0.5f)*100*(_backGround.localScale.x/_nazo),
                keyframe.value*100*_backGround.localScale.y,
                0
            );

            key.name = keyframe.time.ToString();
        }

        DrawLine();
    }

    public void Save()
    {
        _bulletEditor._bulletData.animationCurve = _animationCurve;
        ON_END_EDIT.Invoke();
    }
    
}
