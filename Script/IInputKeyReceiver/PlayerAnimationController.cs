using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour, IInputKeyReceiver
{
    public Animator _animator;

    public void Start()
    {
        InputKeySender.Add(this);
    }

    public void OnKeys(KeyEvent e)
    {
        if(e.Mouse0){
            _animator.SetBool("Attack", true);
        }else if(e.W || e.A || e.S || e.D){
            _animator.SetBool("Move", true);
        }else{
            _animator.SetBool("Attack", false);
            _animator.SetBool("Move", false);
        }
    }
}
