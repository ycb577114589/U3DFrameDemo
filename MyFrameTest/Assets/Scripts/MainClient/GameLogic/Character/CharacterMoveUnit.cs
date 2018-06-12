﻿using UnityEngine;
using CommonLib;

namespace MainClient
{
    class CharacterMoveUnit
    {
        public Vector3 _rotation = Vector3.zero;
        private float _destRotateY = 555f;
        public Vector3 position = Vector3.zero;
        
        CharacterBase _host = null;

        public float forwardSpeed;
        public float rotateSpeed = 900f;

        private CharacterMoveUnit() { }
        public CharacterMoveUnit(CharacterBase host)
        { _host = host; }

        public Vector3 Rotation
        {
            get { return _rotation; }
            set
            {

                _rotation = value;
                SetDestRotation(_rotation.y, true);
                _host.ThisView.UpdateRotation(_rotation);
            }
        }

        public bool HaveSpeed
        {
            get
            {
                return !CommonFunction.IsZero(forwardSpeed);
            }
        }
        public Vector3 Forward
        {
            get
            {
                return Quaternion.Euler(_rotation) * Vector3.forward;
            }
            set
            {
                float angle = CommonFunction.GetAngle(value.x, value.y);
                SetDestRotation(CommonFunction.ChangeAnge(angle), true);
                _host.ThisView.UpdateRotation(_rotation);
            }
           
        }
        public void Init()
        {
            forwardSpeed = 0f;
            _destRotateY = 555f;
            position = Vector3.zero;
            _rotation=Vector3.zero;
            _host.ThisView.UpdateRotation(_rotation);
        }
        public bool SetDestRotation(float destRotation,bool canRotate)
        {
            float destY = _rotation.y;
            if(!CommonFunction.IsZero(destRotation - destY)){
                if (canRotate)
                {
                    _rotation.y = destRotation;
                    _host.ThisView.SetDestRotation(destRotation, rotateSpeed);
                }
                else
                {
                }
                return true;
            }

            return false;
        }
        public void Update()
        {
            if (_destRotateY < 500f)
            {
                _rotation.y = _destRotateY;
                _host.ThisView.SetDestRotation(_destRotateY, rotateSpeed);
                _destRotateY = 555f;
            }
        }
        public void Move(float delta)
        {
            position = CommonMoveUnit.Move(position, Forward, delta, forwardSpeed);
        }
        public void UpdateViewPosition()
        {
            _host.ThisView.MoveUpdate(position);
        }
        public void SetPosition(Vector3 pos)
        {
            position = pos;
            UpdateViewPosition();
        }


    }
}
