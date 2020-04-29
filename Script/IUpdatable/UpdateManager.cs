using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// http://unity-michi.com/post-660/

public interface IUpdatable {
    void UpdateMe();    
}

    public class UpdateManager : MonoBehaviour
    {
        const int InitialSize = 256;
        
        private int tail = 0;
        private IUpdatable[] updatableArray = new IUpdatable[InitialSize];
        private int _frame = 0;
        private float _deltaTime = 0;
        private float _time = 0;
        public static float FPS = 30;
        // private WaitForSeconds WaitForSeconds = new WaitForSeconds(1f/FPS);

        [SerializeField] bool reduceArraySizeWhenNeed = false;
        private GameObject replayCamera;
        private List<GameObject> uI = new List<GameObject>();
        
        public static bool ReduceArraySizeWhenNeed
        {
            get { return manager.reduceArraySizeWhenNeed; }
            set { manager.reduceArraySizeWhenNeed = value; }
        }

        public static GameObject ReplayCamera
        {
            set { manager.replayCamera = value; }
        }

        public static GameObject UI
        {
            set { manager.uI.Add(value); }
        }

        public static int frame
        {
            get { return manager._frame; }
        }

        public static float deltaTime
        {
            get { return manager._deltaTime; }
        }

        public static float time
        {
            get { return manager._time; }
        }

        private static UpdateManager manager
        {
            get
            {
                if (!_manager)
                {
                    _manager = FindObjectOfType<UpdateManager>();
                    if (!_manager)
                    {
                        _manager = new GameObject("UpdateManager").AddComponent<UpdateManager>();
                    }
                }
                return _manager;
            }
        }
        static UpdateManager _manager;

        void Awake()
        {
            if (manager && _manager != this)
                Destroy(gameObject);

            _deltaTime = 1f / FPS;

        }
        
        void FixedUpdate()
        {
            if(Time.fixedTime / _frame > _deltaTime){ //_freq >= _deltaTime
                for (int i = 0; i < tail; i++)
                {
                    if (updatableArray[i] == null) continue;
                    updatableArray[i].UpdateMe();
                }

                _time += _deltaTime;
                _frame++;
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.P)){

                if(Time.timeScale == 0){
                    if(replayCamera != null) replayCamera.SetActive(false);
                    if(uI.Count != 0) for(int i = 0; i < uI.Count; ++i){ uI[i].SetActive(true); } 
                    Time.timeScale = 1;

                }else{
                    if(replayCamera != null) replayCamera.SetActive(true);
                    if(uI.Count != 0) for(int i = 0; i < uI.Count; ++i){ uI[i].SetActive(false); } 
                    Time.timeScale = 0f;

                }
            }
        }

        /// <summary>
        /// Update 対象の追加.
        /// </summary>
        public static void AddUpdatable(IUpdatable updatable)
        {
            if (updatable == null) return;
            manager.addUpdatable(updatable);
        }

        void addUpdatable(IUpdatable updatable)
        {
            if(updatableArray.Length == tail)
            {
                Array.Resize(ref updatableArray, checked(tail * 2));
            }
            updatableArray[tail++] = updatable;
        }

        /// <summary>
        /// 指定した Updatable を Update 対象から除外する.
        /// </summary>
        public static void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable == null) return;
            manager.removeUpdatable(updatable);
        }

        void removeUpdatable(IUpdatable updatable)
        {
            for (int i = 0; i < updatableArray.Length; i++)
            {
                if (updatableArray[i] == updatable)
                {
                    updatableArray[i] = null;
                    refreshUpdatableArray();
                    return;
                }
            }
        }

        /// <summary>
        /// 配列整理.
        /// </summary>
        public static void RefreshUpdatableArray()
        {
            manager.refreshUpdatableArray();
        }

        void refreshUpdatableArray()
        {
            var j = tail - 1;

            // 指定した部分は null に,
            // null の部分には配列内の一番後ろにある要素を代入.
            for (int i = 0; i < updatableArray.Length; i++)
            {
                if (updatableArray[i] == null)
                {
                    while (i < j)
                    {
                        var fromTail = updatableArray[j];
                        if (fromTail != null)
                        {
                            updatableArray[i] = fromTail;
                            updatableArray[j] = null;
                            j--;
                            goto NEXTLOOP;
                        }
                        j--;
                    }

                    tail = i;
                    break;
                }

                NEXTLOOP:
                continue;
            }

            if (reduceArraySizeWhenNeed && tail < updatableArray.Length / 2)
                Array.Resize(ref updatableArray, updatableArray.Length / 2);
        }

    }
