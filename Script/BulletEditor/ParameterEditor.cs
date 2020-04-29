using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterEditor : MonoBehaviour
{
    public BulletEditor _bulletEditor;
    public CreateBullet _createBullet;

    public void OnLoadBulletData()
    {
        int i = 0;
        foreach(Transform child in transform){
            switch(i){
                case 0:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.rateX.ToString();
                break;
                case 1:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.rateY.ToString();
                break;
                case 2:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.speed.ToString();
                break;
                case 3:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.rotateSpeed.ToString();
                break;
                case 4:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.destroyTime.ToString();
                break;
                case 5:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.limitAngle.ToString();
                break;
                case 6:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.bulletCount.ToString();
                break;
                case 7:
                    child.GetComponentInChildren<InputField>().text = _bulletEditor._bulletData.color.ToString();
                    child.GetComponentInChildren<Image>().color = _bulletEditor._bulletData.color;
                break;
                case 8:
                    child.GetComponent<InputField>().text = _bulletEditor._bulletData._name.ToString();
                break;
            }
            i++;
        }
    }

    public void OnRateXChanged(string str)
    {
        _bulletEditor._bulletData.rateX = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnRateYChanged(string str)
    {
        _bulletEditor._bulletData.rateY = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnSpeedChanged(string str)
    {
        _bulletEditor._bulletData.speed = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnRotateSpeedChanged(string str)
    {
        _bulletEditor._bulletData.rotateSpeed = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnDestroyTimeChanged(string str)
    {
        _bulletEditor._bulletData.destroyTime = int.Parse(str);
        _bulletEditor._bulletData.rateX = 1.0f / int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnLimitAngleChanged(string str)
    {
        _bulletEditor._bulletData.limitAngle = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnBulletCountChanged(string str)
    {
        _bulletEditor._bulletData.bulletCount = int.Parse(str);
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnNameChanged(string str)
    {
        _bulletEditor._bulletData._name = str;
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

    public void OnColorChanged(Color color)
    {
        _bulletEditor._bulletData.color = color;
        _createBullet.SetBulletData(_bulletEditor._bulletData);
        _createBullet.Generate();
    }

}
