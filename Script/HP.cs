using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// HPが0になるとイベント発火 
public class HP : SeenValue
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;
    public int maxHp { get{ return _maxHp;}}
    [SerializeField] string _tag;
    [SerializeField] AudioClip _SE;
    private AudioSource _audioSource;


    public UnityEvent zero = new UnityEvent();

    void Start()
    {
        hp = _maxHp;
        if(_SE != null) _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public int hp {
        set {
            _hp = value;
            if(_audioSource != null){
                _audioSource.Stop();
                _audioSource.PlayOneShot(_SE);
            }

            if(value <= 0){
                zero.Invoke();
                
            }
            if(Call != null)Call(value);
        }
        get {
            return _hp;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == _tag){
            //print(transform.name + " is damaged by " + _tag);
            hp -= 1;
        }
    }

    
}
