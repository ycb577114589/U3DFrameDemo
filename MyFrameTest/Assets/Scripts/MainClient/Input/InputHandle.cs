using System;
using System.Collections.Generic;
using UnityEngine;
using CommonLib;

namespace MainClient
{
    public class InputHandle
    {
        private static Dictionary<UInt32, float> _Dir2RotateAngle = null;

        public InputHandle()
        {
            if (_Dir2RotateAngle == null)
            {
                _Dir2RotateAngle = new Dictionary<UInt32, float>
                {
                    {InputDefine.MOVE_UP,0f},
                    {InputDefine.MOVE_UP|InputDefine.MOVE_RIGHT,45f},
                    {InputDefine.MOVE_RIGHT,90f },
                    {InputDefine.MOVE_RIGHT|InputDefine.MOVE_DOWN,135f },
                    {InputDefine.MOVE_DOWN,-180f },
                    {InputDefine.MOVE_DOWN|InputDefine.MOVE_LEFT,-135f },
                    {InputDefine.MOVE_LEFT,-90f },
                    {InputDefine.MOVE_LEFT|InputDefine.MOVE_UP,-45f },
                };
            }
        }
        private IInputListener _listener = null;
        private UInt32 directionKey = 0;
        public void AddInputListener(IInputListener listener)
        {
            _listener = listener;
        }
        public void Update(float delta)
        {

            //key Down
            if (Input.GetKeyDown(KeyCode.A))
            {
                DirectionKeyDown(InputDefine.MOVE_LEFT);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                DirectionKeyDown(InputDefine.MOVE_RIGHT);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                DirectionKeyDown(InputDefine.MOVE_UP);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                DirectionKeyDown(InputDefine.MOVE_DOWN);
            }

            // key Up
            if (Input.GetKeyUp(KeyCode.A))
            {
                DirectionKeyUp(InputDefine.MOVE_LEFT);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                DirectionKeyUp(InputDefine.MOVE_RIGHT);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                DirectionKeyUp(InputDefine.MOVE_UP);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                DirectionKeyUp(InputDefine.MOVE_DOWN);
            }




            if (Input.GetKeyDown(KeyCode.J))
            {
                FunctionKeyDown(InputDefine.FUNCTION_KEY1);
            }
        }
        public void FunctionKeyDown(UInt16 key)
        {
            if (GameSyncManager.Instance.IsStepLockMode)
            {
                GameSyncManager.Instance.AddKeyInfo(key, 0, 1);
                return;
            }
            //if StepNormal
            if(_listener!=null)
            {
                _listener.OnFunctionKeyDown(key);
            }
        }   
        public void DirectionKeyUp(UInt32 keyCode)
        {
            UInt32 lastKey = directionKey;
            directionKey &= ~keyCode;
            OnMove(lastKey, directionKey);
        }
        public void DirectionKeyDown(UInt32 keyCode)
        {
            UInt32 lastKey = directionKey;
            switch (keyCode)
            {
                case InputDefine.MOVE_DOWN:
                    directionKey &= ~InputDefine.MOVE_UP;
                    break;
                case InputDefine.MOVE_UP:
                    directionKey &= ~InputDefine.MOVE_DOWN;
                    break;
                case InputDefine.MOVE_LEFT:
                    directionKey &= ~InputDefine.MOVE_RIGHT;
                    break;
                case InputDefine.MOVE_RIGHT:
                    directionKey &= ~InputDefine.MOVE_LEFT;
                    break;
            }
            directionKey |= keyCode;
            OnMove(lastKey, directionKey);
        }
        public void OnMove(UInt32 lastKey,UInt32 newKey)
        {
            if(lastKey!= newKey)
            {
                if (newKey == InputDefine.STOP)
                {
                    DirctionTouch(0f,MoveType.STOP);
                }
                else
                {
                    DirctionTouch(_Dir2RotateAngle[newKey], MoveType.MOVE_ANGLE);
                }
            }
        }

        public void DirctionTouch(float angle,MoveType moveType)
        {
            if (GameSyncManager.Instance.IsStepLockMode)
            {
                if (CommonFunction.LessZero(angle))
                {
                    angle += 360f;
                }
                GameSyncManager.Instance.AddKeyInfo(InputDefine.DIRCTION_KEY, (UInt16)Mathf.FloorToInt(angle + CommonFunction.EPS), (byte)moveType);
                return;
            }
            //if isStepNormal
            if (_listener != null)
            {
                _listener.OnMove(angle, moveType);
            }
        }

    }
    
}