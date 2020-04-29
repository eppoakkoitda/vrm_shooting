using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    // 指定したオブジェクトを子にプールする GetPoolObjで取得可能
    [SerializeField] private GameObject _prefab;

    public GameObject GetPoolObj()
    {
        if(transform.childCount > 0){
            foreach(Transform child in transform){
                if(child.gameObject.activeSelf == false){
                    return child.gameObject;
                }
            }
        }

        return CreatePoolObj();
    }

    public GameObject CreatePoolObj()
    {
        GameObject obj = Instantiate<GameObject>(_prefab, transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        return obj;
    }

    void Start()
    {
        InvokeRepeating("Test", 0f, 1f);
    }

    void Test()
    {
        GetPoolObj().SetActive(true);
    }
}
