using UnityEngine;
using System;
using System.IO;

public interface IInputKeyReceiver {
    void OnKeys(KeyEvent e);    
}

public struct KeyEvent {
    public bool W, A, S, D, Space, LeftShift, Mouse0;
    public Vector3 MousePosition;
}

    public class InputKeySender : MonoBehaviour, IUpdatable
    {
        const int InitialSize = 16;
        private int tail = 0;
        private bool pose = false;
        private KeyEvent keyEvent = new KeyEvent();
        private string replayFile = "replay_key";
        private ReplayData<KeyEvent> _replayData;
        private IInputKeyReceiver[] receiverArray = new IInputKeyReceiver[InitialSize];
        public static bool replayMode = false;
        [SerializeField] bool reduceArraySizeWhenNeed = false;
    
        
        public static bool ReduceArraySizeWhenNeed
        {
            get { return sender.reduceArraySizeWhenNeed; }
            set { sender.reduceArraySizeWhenNeed = value; }
        }

        private static InputKeySender sender
        {
            get
            {
                if (!_sender)
                {
                    _sender = FindObjectOfType<InputKeySender>();
                    if (!_sender)
                    {
                        _sender = new GameObject("InputKeySender").AddComponent<InputKeySender>();
                    }
                }
                return _sender;
            }
        }
        static InputKeySender _sender;

        void Awake()
        {
            if (sender && _sender != this)
                Destroy(gameObject);

            UpdateManager.AddUpdatable(this);

            _replayData = new ReplayData<KeyEvent>(replayFile);
        }

        public void OnApplicationQuit()
        {
            if(!replayMode)
            _replayData.Close();
        }

        public static void ReplayDataClose()
        {
            sender._replayData.Close();
        }
        
        public void UpdateMe()
        {
            if(!replayMode){
                InputKeyEvent();
            }else{
                ReplayInputKeyEvent();
            }

            for (int i = 0; i < tail; i++)
            {
                if (receiverArray[i] == null) continue;
                receiverArray[i].OnKeys(keyEvent);
            }

        }

        private void InputKeyEvent()
        {
            keyEvent.W = Input.GetKey(KeyCode.W);
            keyEvent.A = Input.GetKey(KeyCode.A);
            keyEvent.S = Input.GetKey(KeyCode.S);
            keyEvent.D = Input.GetKey(KeyCode.D);
            keyEvent.Space = Input.GetKey(KeyCode.Space);
            keyEvent.LeftShift = Input.GetKey(KeyCode.LeftShift);
            keyEvent.Mouse0 = Input.GetKey(KeyCode.Mouse0);
            keyEvent.MousePosition = Input.mousePosition.Floor();

            _replayData.Write(keyEvent);
        }

        private void ReplayInputKeyEvent()
        {
            keyEvent = _replayData.Load(UpdateManager.frame);
        }

        /// <summary>
        /// Update 対象の追加.
        /// </summary>
        public static void Add(IInputKeyReceiver re)
        {
            if (re == null) return;
            sender.add(re);
        }

        void add(IInputKeyReceiver re)
        {
            if(receiverArray.Length == tail)
            {
                Array.Resize(ref receiverArray, checked(tail * 2));
            }
            receiverArray[tail++] = re;
        }

        /// <summary>
        /// 指定した Updatable を Update 対象から除外する.
        /// </summary>
        public static void Remove(IInputKeyReceiver updatable)
        {
            if (updatable == null) return;
            sender.remove(updatable);
        }

        void remove(IInputKeyReceiver updatable)
        {
            for (int i = 0; i < receiverArray.Length; i++)
            {
                if (receiverArray[i] == updatable)
                {
                    receiverArray[i] = null;
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
            sender.refreshUpdatableArray();
        }

        void refreshUpdatableArray()
        {
            var j = tail - 1;

            // 指定した部分は null に,
            // null の部分には配列内の一番後ろにある要素を代入.
            for (int i = 0; i < receiverArray.Length; i++)
            {
                if (receiverArray[i] == null)
                {
                    while (i < j)
                    {
                        var fromTail = receiverArray[j];
                        if (fromTail != null)
                        {
                            receiverArray[i] = fromTail;
                            receiverArray[j] = null;
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

            if (reduceArraySizeWhenNeed && tail < receiverArray.Length / 2)
                Array.Resize(ref receiverArray, receiverArray.Length / 2);
        }
    }
