using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Vector3 _beforeMouse;
    [SerializeField] float _rotateSpeed = 1;
    [SerializeField] float _speed = 1;

    void Start()
    {
        UpdateManager.ReplayCamera = gameObject;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Camera.main.targetDisplay = 1;
        GetComponent<Camera>().targetDisplay = 0;
        transform.position = Camera.main.transform.position - Camera.main.transform.forward;
        transform.LookAt(Camera.main.transform);
    }

    void OnDisable()
    {
        Camera.main.targetDisplay = 0;
        GetComponent<Camera>().targetDisplay = 1;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position += _speed * transform.forward;
        }else if(Input.GetKey(KeyCode.S)){
            transform.position -= _speed * transform.forward;
        }

        if(Input.GetKey(KeyCode.A)){
            transform.position -= _speed * transform.right;
        }else if(Input.GetKey(KeyCode.D)){
            transform.position += _speed * transform.right;
        }

        if(Input.GetKey(KeyCode.Mouse0)){
            Vector2 diff = (Input.mousePosition - _beforeMouse) * _rotateSpeed;
            transform.Rotate(new Vector3(-diff.y, diff.x, 0));
        }else{
            _beforeMouse = Input.mousePosition;
        }

        if(InputKeySender.replayMode){
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                if(Time.timeScale > 0) Time.timeScale -= 0.1f;
            }else if(Input.GetKeyDown(KeyCode.RightArrow)){
                if(Time.timeScale < 2) Time.timeScale += 0.1f;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow)){
                Time.timeScale = 1;
            }else if(Input.GetKeyDown(KeyCode.DownArrow)){
                Time.timeScale = 1;
            }
        }

    }
}
