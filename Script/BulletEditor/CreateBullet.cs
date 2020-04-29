using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateBullet : MonoBehaviour
{
    public BulletData _bulletData;
    public PoolObjectsToList _poolObjectsToList;

    public void SetBulletData(BulletData bulletData)
    {
        _bulletData = bulletData;
    }

    void Start(){
        Generate();
    }

    public void Generate()
    {
        // 親オブジェクト生成
        GameObject[] gameObjectArray = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject val in gameObjectArray){
            if(val.name == _bulletData._name)
                Destroy(val);
        }

        GameObject obj = new GameObject(_bulletData._name);
        // 子オブジェクト取得
        for(int i = 0; i < _bulletData.bulletCount; ++i){
            GameObject child = Instantiate(Resources.Load<GameObject>("Prefab/Bullet"), Vector3.zero, Quaternion.identity);
            child.transform.parent = obj.transform;
            // child.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // child.GetComponent<SphereCollider>().radius = 0.25f;
            Material material = child.GetComponent<Renderer>().material;
            material.color = _bulletData.color;
            TrailRenderer TR = child.GetComponent<TrailRenderer>();
            // TR.widthMultiplier = 0.05f;
            TR.material = material;
            TR.time = _bulletData.destroyTime;
            // child.AddComponent<AutoExplosion>();
            // child.AddComponent<AutoClearTrail>();
            // child.AddComponent<SimpleFractalCollision>();

        }
        ChildsMoveByFunc CMB = obj.AddComponent<ChildsMoveByFunc>();
        CMB._animationCurve = _bulletData.animationCurve;
        CMB._rateX = _bulletData.rateX;
        CMB._rateY = _bulletData.rateY;
        MoveChase MC = obj.AddComponent<MoveChase>();
        MC._speed = _bulletData.speed;
        MC._roteSpeed = _bulletData.rotateSpeed;
        MC._destroyTime = _bulletData.destroyTime;
        MC._limitAngle = _bulletData.limitAngle;
        MC._targetName = "DelayAim";

        if(_poolObjectsToList != null){
            _poolObjectsToList.Reset();
            _poolObjectsToList._prefab = obj;
        }

        obj.gameObject.SetActive(false);
    }

    public void _oldGenerate()
    {
        // Material material = new Material();
        // material.color = _color;
        
        // 親オブジェクト生成
        GameObject[] gameObjectArray = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject val in gameObjectArray){
            if(val.name == _bulletData._name)
                Destroy(val);
        }

        GameObject obj = new GameObject(_bulletData._name);
        // 子オブジェクト取得
        for(int i = 0; i < _bulletData.bulletCount; ++i){
            GameObject child = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            child.transform.parent = obj.transform;
            child.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            child.GetComponent<SphereCollider>().radius = 0.25f;
            Material material = child.GetComponent<Renderer>().material;
            material.color = _bulletData.color;
            TrailRenderer TR = child.AddComponent<TrailRenderer>();
            TR.widthMultiplier = 0.05f;
            TR.material = material;
            TR.time = _bulletData.destroyTime;
            
            child.AddComponent<AutoExplosion>();
            child.AddComponent<AutoClearTrail>();
            child.AddComponent<SimpleFractalCollision>();

        }
        ChildsMoveByFunc CMB = obj.AddComponent<ChildsMoveByFunc>();
        CMB._animationCurve = _bulletData.animationCurve;
        CMB._rateX = _bulletData.rateX;
        CMB._rateY = _bulletData.rateY;
        MoveChase MC = obj.AddComponent<MoveChase>();
        MC._speed = _bulletData.speed;
        MC._roteSpeed = _bulletData.rotateSpeed;
        MC._destroyTime = _bulletData.destroyTime;
        MC._limitAngle = _bulletData.limitAngle;
        MC._targetName = "DelayAim";

        if(_poolObjectsToList != null){
            _poolObjectsToList.Reset();
            _poolObjectsToList._prefab = obj;
        }

    }

}
