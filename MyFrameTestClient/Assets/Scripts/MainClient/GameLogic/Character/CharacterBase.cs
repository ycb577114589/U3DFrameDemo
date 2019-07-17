using System;
using CommonLib;
using UnityEngine;

namespace MainClient
{
    public class CharacterBase : IInputListener, IFixedTick, ITick, IObjectPool
    {
        private CharacterMoveUnit _move = null;
        private CharacterConfig _characterConfig = null;
        private CharacterActionUnit _action = null;
        private CharacterDataUnit _dataUnit = null;
        private CharacterView _view = null;


        #region 构造及其初始化
        /// <summary>
        /// 不要主动调用，通过CharacterMgr.Instance.GetNewCharacte()  创建
        /// </summary>
        public CharacterBase()
        {
            _move = new CharacterMoveUnit(this);
            _view = new CharacterView();
            _action = new CharacterActionUnit(this);
            _dataUnit = new CharacterDataUnit();
        }
        public void Init()
        {
            _move.Init();
            _view.Init();
            _dataUnit.Init();
            _action.Init();
            OnMove(0f, MoveType.STOP);
        }
        #endregion

        #region 切换资源
        public void ChangeCharacter(UInt32 roleID)
        {
            if(_characterConfig==null || _characterConfig.characterID != roleID)
            {
                var config = CharacterConfigManager.Instance.GetCharacterConfig(roleID);
                if (config != null)
                {
                    _view.ChangeView(config.resName, config);
                    _characterConfig = config;
                }
                else return;
            }
            _action.ChangeAction(CharacterAction.IDLE, true);
            Init();
        }
        #endregion

        #region 基础数据
        public CharacterDataUnit ThisData
        {
            get
            {
                return _dataUnit;
            }
        }
        public CharacterView ThisView
        {
            get
            {
                return _view;
            }
        }
        public CharacterConfig ThisConfig
        {
            get
            {
                return _characterConfig;
            }
        }
        public CharacterActionUnit ThisActionUnit
        {
            get
            {
                return _action;
            }
        }
        #endregion

        #region speed
        public void UpdateForwardSpeed()
        {
            _move.forwardSpeed = 0f;
            float moveSpeed = 0f;
            if (_dataUnit.lastMoveType == MoveType.STOP)
            {

            }
            else if (_action.CurrentMoveAction == CharacterAction.RUN)
            {
                moveSpeed = _characterConfig.runSpeed;
            }
            if(_action.CurrentActionConfig.canRotate)
            {
                _move.forwardSpeed = moveSpeed;
            }
        }
        #endregion

        #region 动作相关 
        public void OnActionChanged()
        {
            UpdateForwardSpeed();
            _move.UpdateViewPosition();

            if(_action.CurrentActionConfig.canRotate|| _action.CurrentAction == CharacterAction.IDLE)
            {
                _move.Update();
            }
        }
        #endregion

        #region IInputListener interface
        #region 方向键相关
        public void OnMove(float angle,MoveType moveType)
        {
            
            if (moveType == MoveType.NONE) { return; } 
            if (moveType == MoveType.STOP)
            {
                _action.ChangeMoveAction(CharacterAction.IDLE);
            }
            else
            {
                CharacterAction targetAction = CharacterAction.RUN;
                float dest = angle;
                if (CommonFunction.LessZero(dest))
                {
                    dest += 360f;
                }
                if (_action.CurrentMoveAction == targetAction && CommonFunction.IsZero(_move.Rotation.y - dest) && _dataUnit.lastMoveType == MoveType.MOVE_ANGLE && moveType == MoveType.MOVE_ANGLE)
                {
                    return;
                }
                _action.ChangeMoveAction(targetAction);
                if (moveType == MoveType.MOVE_ANGLE)
                {
                    _move.SetDestRotation(angle, _action.CurrentActionConfig.canRotate);
                }
            }
            _dataUnit.lastMoveType = moveType;
            UpdateForwardSpeed();
        }
        #endregion
        #region 功能按键
        public void OnFunctionKeyDown(UInt16 keyCode)
        {
            switch(keyCode)
            {
                case InputDefine.FUNCTION_KEY1:
                    _action.NormalAttackKeyDown();
                    break;
                
            }
        }
        public void OnFunctionKeyUp(UInt16 keyCode)
        {

        }
        #endregion
        #endregion
                                                                    
        #region IPoolObject
        public uint ObjectIndex
        {
            get;
            set;
        }
        public void Release()
        {
            TickerManager.Instance.RemoveFixedTick(this);
            TickerManager.Instance.RemoveTick(this);
            Init();
            _characterConfig = null;
            _action.Release();
            _view.Release();

        }
        #endregion
        #region IFiexedTick interface
        public void FixedUpdate (uint fixedTickCount,float delta)
        {
            #region 动作切换
            _action.Update(delta);
            #endregion

            #region 移动相关
            if (_move.HaveSpeed)
            {
                _move.Move(delta);
            }
            //如果显示逻辑分离太大，强制拉回显示
            if ((_move.position - _view.MainTransform.position).magnitude >= 10f)
            {
                _move.UpdateViewPosition();
            }
            #endregion
        }
        #endregion
        #region ITick interface
        public void Update(float delta)
        {
            if (_move.HaveSpeed)
            {
                _view.Move(delta, _move.forwardSpeed, _move.Forward);
            }
            _view.UpdateRotate(delta);
        }
        #endregion
        public Vector3 Position
        {
            get
            {
                return _move.position;
            }
            set
            {
                _move.SetPosition(value);
            }
        }
    }

}