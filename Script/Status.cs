using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Status : MonoBehaviour
{
    [System.Serializable]
    public class mass {
        public string name;
        public bool isStr;
        public string stringValue;
        public int intValue;
    }

    public int _number = 1;
    public List<mass> _mass = new List<mass>(1);

    public void Set<T>(string name, T value)
    {
        foreach(mass m in _mass){
            if(m.name == name){
                if(m.isStr){
                    if(typeof(T) == typeof(string)){
                        m.stringValue = (string)(object)value;
                    }
                }else{
                    if(typeof(T) == typeof(int)){
                        m.intValue = (int)(object)value;
                    }
                }
            }
        }
    }

    public void Set<T>(int index, T value)
    {
        if(_mass[index].isStr){
            if(typeof(T) == typeof(string)){
                _mass[index].stringValue = (string)(object)value;
            }
        }else{
            if(typeof(T) == typeof(int)){
                _mass[index].intValue = (int)(object)value;
            }
        }
    }

    public int GetInt(string name)
    {
        foreach(mass m in _mass){
            if(m.name == name){
                if(m.isStr){
                    return int.Parse(m.stringValue);
                }else{
                    return m.intValue;
                }
            }
        }
        return -1;
    }

    public int GetInt(int index)
    {
        if(_mass[index].isStr){
            return int.Parse(_mass[index].stringValue);
        }else{
            return _mass[index].intValue;
        }

    }

    public string GetString(string name)
    {
        foreach(mass m in _mass){
            if(m.name == name){
                if(m.isStr){
                    return m.stringValue;
                }else{
                    return m.intValue.ToString();
                }
            }
        }
        return null;
    }

    public string GetString(int index)
    {
        if(_mass[index].isStr){
            return _mass[index].stringValue;
        }else{
            return _mass[index].intValue.ToString();
        }

    }

}

#if UNITY_EDITOR

[CustomEditor(typeof(Status))]
[CanEditMultipleObjects]
public class StatusEditor : Editor
{
    private Status _target;
    public Vector2 _scrollPosition = Vector2.zero;

    private void Awake()
    {
        _target = target as Status;
    }

    public override void OnInspectorGUI()
    {
        _target._number = EditorGUILayout.IntField("個数", _target._number);
        while(_target._mass.Count < _target._number){
            _target._mass.Add(new Status.mass());
        }

        if(_target._number <= 0){
            _target._number = 1;
        }else{
            while(_target._mass.Count > _target._number){
                _target._mass.RemoveAt(_target._number);
            }
        }

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,GUI.skin.box);

        for(int i = 0; i < _target._mass.Count; ++i){
            EditorGUILayout.LabelField( "No." + i );   

            EditorGUILayout.BeginVertical(GUI.skin.box);       

            if (_target._mass[i].isStr){
                bool button = GUILayout.Button("文字列 (string)");
                if(button) _target._mass[i].isStr = !_target._mass[i].isStr;
                _target._mass[i].name = EditorGUILayout.TextField("名前", _target._mass[i].name);
                // _target._mass[i].isStr = EditorGUILayout.ToggleLeft("文字列(string)で入力", _target._mass[i].isStr);
                _target._mass[i].stringValue = EditorGUILayout.TextField("文字列", _target._mass[i].stringValue);
            }else{
                bool button = GUILayout.Button("整数 (int)");
                if(button) _target._mass[i].isStr = !_target._mass[i].isStr;
                _target._mass[i].name = EditorGUILayout.TextField("名前", _target._mass[i].name);
                // _target._mass[i].isStr = EditorGUILayout.ToggleLeft("整数値(int)で入力", _target._mass[i].isStr);
                _target._mass[i].intValue = EditorGUILayout.IntField("数値", _target._mass[i].intValue);
            }

            EditorGUILayout.EndVertical();

        }

        EditorGUILayout.EndScrollView();
    }
}
#endif
