using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class ReplayData<T> {
    private List<int> _frameLog = new List<int>();
    private List<T> _TLog = new List<T>();
    private string _fileName;
    private int _index = -1;
    private StringBuilder _sb;
    public ReplayData(string fileName){
        _fileName = fileName;
        
        try{
            using (StreamReader sr = new StreamReader(Application.dataPath + "/../" + fileName))
            {
                _frameLog.Clear(); _TLog.Clear();

                string line;
                int i = 0;
                while((line = sr.ReadLine()) != null)  
                {  
                    string[] temp = line.Split('|');
                    _frameLog.Add(int.Parse(temp[0]));
                    _TLog.Add(JsonUtility.FromJson<T>(temp[1]));
                    i++;

                }

                _sb = new StringBuilder();

            }
        }catch(FileNotFoundException reigai){
            File.CreateText(Application.dataPath + "/../" + _fileName);
            _sb = new StringBuilder();
            Debug.Log(reigai);
        }
    }

    public void Write(T e)
    {
        _sb.AppendLine(UpdateManager.frame + "|" + JsonUtility.ToJson(e));        
    }

    public void Close()
    {
        Debug.Log("Closing...");
        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/../" + _fileName))
        {
            Debug.Log("保存しました\n\n" + _sb.ToString());
            // ファイルへの書き込み
            sw.Write(_sb.ToString());
        }
    }

    public T Load(int frame)
    {
        if(_index == -1) _index = GetIndex(frame);

        if(_index == -1) return default(T);

        if(_index == _frameLog.Count - 1) return _TLog[_index];

        if(_frameLog[_index] == frame){
            _index++;
            return _TLog[_index - 1];
        }else{
            // Debug.Log(
            //     "Error\n" + 
            //     "nowFrame: " + frame + "\n" +
            //     "index: " +_index + "\n" +
            //     "frameLog: " + _frameLog[_index] + "\n" +
            //     "_from_" + _fileName
            // );
            _index = GetIndex(frame);
            return Load(frame);
        }

    }

    private int GetIndex(int frame)
    {
        for(int i = 0; i < _frameLog.Count; ++i){
            if(_frameLog[i] == frame){
                Debug.Log("Get: " + i);
                return i;
            }
        }

        return -1;
    }

}
