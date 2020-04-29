using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using System; //Exception
using UnityEngine.SceneManagement;

public class BulletEditor : MonoBehaviour
{
    public BulletData _bulletData;
    [System.Serializable]
    public class CustomUnityEvent : UnityEvent<BulletData>{}
    public CustomUnityEvent ON_END_LOAD = new CustomUnityEvent();

    public void Save()
    {
        string jsonstr = JsonUtility.ToJson(_bulletData);
        StreamWriter writer = new StreamWriter(Application.dataPath + "/../BulletsData/" + _bulletData._name + ".json");
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    // bulletData._nameを元にほかのbulletDataのメンバ変数を書き換える
    // ロード完了時にイベント発火
    public void Load()
    {
        try{
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/../BulletsData/" + _bulletData._name + ".json");
            string jsonstr = reader.ReadToEnd();
            reader.Close();
            _bulletData = JsonUtility.FromJson<BulletData>(jsonstr);
        }catch (Exception e){
            
        }

        ON_END_LOAD.Invoke(_bulletData);
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("TitleScene");
        }
    }

    
}
