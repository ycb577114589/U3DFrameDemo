using CommonLib;
using UnityEngine;

namespace MainClient
{
    class CameraManager : Singleton<CameraManager>
    {
        private Vector3 _offset;
        private Transform _cameraTrans = null;
        private Camera _camera = null;
        private Transform _target = null;
        private Vector3 _lastTargetPosition = Vector3.zero;
         
        public bool CanRotate
        {
            get
            {
                return _canRotate;
            }
            set
            {
                _canRotate = value;
            }
        }
        public Vector3 Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }
        public void SetCamera(Transform camera, Vector3 offset)
        {
            _cameraTrans = camera;
            _offset = offset;
            _camera = camera.gameObject.GetComponent<Camera>();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            _lastTargetPosition = target.position;
            _smoothRotate.Stop();
        }
        public void Clear()
        {
            _target = null;
            _camera = null;
            _cameraTrans = null;
            _smoothRotate.Stop();
        } 
        public void Update(float delta)
        {
            if (_target == null || _camera == null)
            {
                return;
            }

            Vector3 pos = _target.position;

            Vector3 dir = pos - _lastTargetPosition;

            if (!CommonFunction.IsZero(dir.magnitude))
            {
                dir = Vector3.zero;
            }
            _cameraTrans.position += dir;
            _lastTargetPosition = pos;

            Vector3 dest = pos + _offset;

            dir = dest - _cameraTrans.position;
            var distence = dir.magnitude;
            float speed = 10f;
            if (CommonFunction.LessOrEqualZero(distence))
            {
                _cameraTrans.position = dest;
            }
            else
            {
                _cameraTrans.position = Vector3.Lerp(_cameraTrans.position, dest, speed * delta);
            }

        } 
    }
}
