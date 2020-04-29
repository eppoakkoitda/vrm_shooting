using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBulletNames : MonoBehaviour
{
    [SerializeField] Dropdown _template;
    [SerializeField] int _count;

    void Awake()
    {
        for(int i = 0; i < _count; ++i){
            var obj = Instantiate(_template);
            obj.gameObject.SetActive(true);
            obj.transform.parent = transform;
            obj.transform.Reset(true);
            obj.name = i.ToString();
        }
    }
}
