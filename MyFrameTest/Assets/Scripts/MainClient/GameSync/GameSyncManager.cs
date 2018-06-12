using System;
using System.Collections.Generic;
using CommonLib;
using UnityEngine;

namespace MainClient
{
    enum SyncMode
    {
        NORMAL,
        STEP_LOCK,
    }

    class GameSyncManager : Singleton<GameSyncManager>
    {
        private SyncMode _syncMode = SyncMode.NORMAL;
        private UInt32 _nextFrameStep = 0;
        private UInt64 _uin = 0;

        private ProtocolFrameNotify _frameUpdateReq;
        private Dictionary<UInt64, IInputListener> _inputListeners;
        private bool _needUpdate = false;
        private bool _isGaming = false;

        #region 基础属性
        public bool IsStepLockMode
        {
            get
            {
                return _syncMode == SyncMode.STEP_LOCK;
            }
        }
        public SyncMode CurrentMode
        {
            get
            {
                return _syncMode;
            }
        }
        public UInt64 Uin
        {
            get
            {
                return _uin;
            }
            set
            {
                _uin = value;
            }
        }
        #endregion
        public void Update(float delta)
        {
            if (!_isGaming)
            {
                return;
            }
            if (IsStepLockMode)
            {
                if (_needUpdate)
                {
                    ClientNetManager.Instance.Send(_frameUpdateReq);
                    _frameUpdateReq.data.Keys.Clear();
                    _needUpdate = false;
                }
            }
        }
        public GameSyncManager()
        {
            _frameUpdateReq = new ProtocolFrameNotify();
            _inputListeners = new Dictionary<ulong, IInputListener>();
        }
        public void BeforeStartGame(SyncMode mode)
        {
            _syncMode = mode;
            //初始化数据
            TickerManager.Instance.ClearAllTicks();
            CharacterManager.Instance.Clear();
            //第一帧是1
            _nextFrameStep = 1;
            TickerManager.Instance.FixedTickCount = 0;
            _frameUpdateReq.data.Uin = _uin;
            _frameUpdateReq.data.Keys.Clear();
            _inputListeners.Clear();
            _needUpdate = false;
            BattleUIManager.Instance.DirectionUI.Enable = true;
            BattleUIManager.Instance.AttackUI.Enable = true;
            CameraManager.Instance.SetTarget(GameClient.Instance.MainPlayer.ThisView.MainTransform);
        }
        public void AfterStartGame()
        {
            _isGaming = true;
        }
        public void StartBattle(SCStartGame res)
        {
            BeforeStartGame(SyncMode.STEP_LOCK);
            CharacterBase mainPlayer = GameClient.Instance.MainPlayer;
            CharacterBase currentPlayer = null;
            foreach(var p in res.Uins)
            {
                if (p == _uin)
                {
                    currentPlayer = mainPlayer;
                }
                else
                {
                    currentPlayer = CharacterManager.Instance.GetNewCharacter();
                }
                currentPlayer.ThisData.uin = p;
                currentPlayer.ChangeCharacter(1);
                currentPlayer.Init();
                _inputListeners.Add(currentPlayer.ThisData.uin, currentPlayer);
                currentPlayer.Position = Vector3.zero;
                TickerManager.Instance.AddTick(currentPlayer);
                TickerManager.Instance.AddFixedTick(currentPlayer);
            }
            AfterStartGame();
        }
        #region 帧同步消息
        public void AddKeyInfo(UInt16 keyCode ,UInt16 value,Byte type)
        {
            if (!_isGaming)
            {
                return;
            }
            //四位 key
            keyCode &= 0xF;
            value &= 0x3ff;
            type &= 0x3;
            keyCode <<= 10;
            keyCode |= value;
            keyCode <<= 2;
            keyCode |= type;
            _frameUpdateReq.data.Keys.Add(keyCode);
            _needUpdate = true;
        }
        public void OnFrameAsyn(SCFrameNotify frameAsyn)
        {
            if (!_isGaming)
            {
                return;
            }
            //无操作的帧直接跑
            if (_nextFrameStep < frameAsyn.CurrentFrame)
            {
                TickerManager.Instance.DriveToTickCount(frameAsyn.CurrentFrame - 1);
                _nextFrameStep = frameAsyn.CurrentFrame;
            }
            //当前帧判断内容
            if (_nextFrameStep == frameAsyn.CurrentFrame)
            {
                //按键
                foreach(var frameControlList in frameAsyn.Keys)
                {
                    IInputListener listener = _inputListeners[frameControlList.Uin];
                    Debug.Log(frameControlList.Uin);
                    foreach(var con in frameControlList.Keys)
                    {
                        #region 解析按键
                        UInt16 key = (UInt16)con;
                        //后两位是类型 up down
                        UInt16 type = (Byte)(key & 0x3);
                        key >>= 2;
                        //值 0~2^10 范围
                        UInt16 value = (UInt16)(key & 0x3ff);
                        key >>= 10;
                        //自定义类型
                        key &= 0xf;
                        #endregion
                        if (key == InputDefine.DIRCTION_KEY)
                        {
                            float angle = value;
                            if (CommonFunction.GreatOrEqualZero(angle - 180f))
                            {
                                angle -= 360f;
                            }
                            listener.OnMove(angle, (MoveType)type);
                        }
                        else
                        {
                            if (type > 0)
                            {
                                listener.OnFunctionKeyDown(key);
                            }
                            else
                            {
                                listener.OnFunctionKeyUp(key);
                            }
                        }
                    }
                }
                _nextFrameStep = frameAsyn.NextFrame;
                TickerManager.Instance.DriveToTickCount(_nextFrameStep - 1);
            }
        }
        #endregion
    }
}
