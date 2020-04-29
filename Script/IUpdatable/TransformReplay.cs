using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TransformReplay : MonoBehaviour, IUpdatable
{
    [SerializeField] bool _play = false;
    private ReplayData<PositionAndRotation> _replayData;
    private string _fileName;
    private PositionAndRotation _nowTrans = new PositionAndRotation();

    [System.Serializable]
    public class PositionAndRotation {
        public Vector3 position;
        public Quaternion rotation;
    }

    void Awake()
    {
        if(_play){
            var component0 = GetComponent<CharaCtr1>();
            if(component0 != null) component0.enabled = false;
            var component1 = GetComponent<ChaseTarget>();
            if(component1 != null) component1.enabled = false;
            var component2 = GetComponent<MoveByMouse1>();
            if(component2 != null) component2.enabled = false;

        }
    }

    void Start()
    {
        _fileName = "replay_transform_" + transform.name;
        _replayData = new ReplayData<PositionAndRotation>(_fileName);
    }
    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }

    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public void UpdateMe()
    {
        if(_play){
            _nowTrans = _replayData.Load(UpdateManager.frame);
            if(_nowTrans != null){
                transform.SetPositionAndRotation(_nowTrans.position, _nowTrans.rotation);
            }
        }else{
            if(_replayData == null) return;
            _nowTrans.position = transform.position;
            _nowTrans.rotation = transform.rotation;
            _replayData.Write(_nowTrans);
        }
    }
}
