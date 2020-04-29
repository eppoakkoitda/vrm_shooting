using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using VRM;

public class BattleSceneInitializer : MonoBehaviour
{
    public CreateBullet _createBullet;
    public Transform _player;
    private EquipmentEditor _equipmentEditor;
    [SerializeField] BulletEditor _bulletEditor;
    [SerializeField] RuntimeAnimatorController _controller;

    void Awake()
    {    
        LoadBulletData();

        ImportVRMAsync();
    }

    private void LoadBulletData()
    {
        if(_equipmentEditor == null){
            _equipmentEditor = gameObject.AddComponent<EquipmentEditor>();
        }

        _equipmentEditor.Load();

        for(int i = 0; i < 5; ++i){
            _bulletEditor._bulletData._name = _equipmentEditor._equipmentData.bullets[i];
            _bulletEditor.Load();
            _createBullet.SetBulletData(_bulletEditor._bulletData);
            _createBullet._poolObjectsToList = _player.Find("Gun").transform.GetChild(i).GetComponent<PoolObjectsToList>();
            _createBullet.Generate();
        }
    }

    private void ImportVRMAsync()
    {
        //VRMファイルのパスを指定します
        var path = Application.dataPath + "/../VRM/" + _equipmentEditor._equipmentData.modelPass + ".vrm";

        //ファイルをByte配列に読み込みます
        var bytes = File.ReadAllBytes(path);

        //VRMImporterContextがVRMを読み込む機能を提供します
        var context = new VRMImporterContext();

        // GLB形式でJSONを取得しParseします
        context.ParseGlb(bytes);

        // VRMのメタデータを取得
        var meta = context.ReadMeta(false); //引数をTrueに変えるとサムネイルも読み込みます

        //読み込めたかどうかログにモデル名を出力してみる
        Debug.LogFormat("meta: title:{0}", meta.Title);

        //非同期処理で読み込みます
        context.LoadAsync(_ => OnLoaded(context));
    }

    private void OnLoaded(VRMImporterContext context)
    {
        //読込が完了するとcontext.RootにモデルのGameObjectが入っています
        var root = context.Root;

        //モデルをワールド上に配置します
        root.transform.SetParent(_player, false);
        root.transform.localPosition = new Vector3(0,-2.7f, 0);
        root.transform.localScale = Vector3.one * 2;

        //メッシュを表示します
        context.ShowMeshes();

        // vrmのGameObjectにｱﾆﾒｰｼｮﾝを設定します。
        var animator = root.GetComponent<Animator>();
        animator.runtimeAnimatorController = _controller;

        var animatorCtr = root.AddComponent<PlayerAnimationController>();
        animatorCtr._animator = animator;
    }

    /// <summary>
    /// リプレイを始める
    /// </summary>
    public void OnBeginReplay()
    {

        if(InputKeySender.replayMode){
            InputKeySender.replayMode = false;
            SceneManager.LoadScene("TitleScene");
        }else{
            InputKeySender.replayMode = true;
            InputKeySender.ReplayDataClose();
            SceneManager.LoadScene("BattleScene");
        }
    }

}
