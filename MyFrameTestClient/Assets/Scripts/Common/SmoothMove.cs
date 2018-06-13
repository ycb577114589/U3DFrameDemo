using UnityEngine;

namespace CommonLib
{
    public class SmoothMove
    {
        private float _speed = 90f;

        private float _origin = 0f;
        private float _dest = 0f;
        private float _needTime = 0f;
        private float _time = 0f;

        public float Dest
        {
            get
            {
                return _dest;
            }
        }
        public void SetValue(float origin,float dest,float speed)
        {
            _dest = dest;
            _origin = origin;
            float dis = Mathf.Abs(_dest - _origin);
            _speed = speed;
            _needTime = dis / _speed;
            _time = 0f;
        }

        public bool Update(float delta,ref float y)
        {
            if(CommonFunction.LessZero(_time- _needTime))
            {
                _time += delta;
                if (CommonFunction.GreatOrEqualZero(_time - _needTime))
                {
                    _time = _needTime;
                }
                y = Mathf.Lerp(_origin, _dest, _time / _needTime);
                return true;
            }
            _needTime = 0f;
            return false;
        }

        public void Stop()
        {
            _needTime = 0f;
        }
        public bool InMoving
        {
            get
            {
                return CommonFunction.GreatZero(_needTime);
            }
        }
    }
}
