using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonLib;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MainClient {
    public class BattleUIManager : Singleton<BattleUIManager>
    {
        private GameObject _mainGO = null;
        private DirectionUIManager _direction = new DirectionUIManager();
        private AttackButtonUIManager _attackUI = new AttackButtonUIManager();
        public override bool Init()
        {
            Object obj = Resources.Load("Prefabs/ui/BattleUI");
            _mainGO = GameObject.Instantiate(obj) as GameObject;
            _mainGO.transform.SetParent(GameClient.Instance.mainCanvas, false);
            _direction.MainGO = _mainGO.transform.Find("TouchDirctionArea").gameObject;
            _attackUI.MainGO = _mainGO.transform.Find("TouchFunction").gameObject;

            _direction.Enable = false;
            _attackUI.Enable = false;
            return true;
        }
        public DirectionUIManager DirectionUI
        {
            get
            {
                return _direction;
            }
        }
        public AttackButtonUIManager AttackUI
        {
            get
            {
                return _attackUI;
            }
        }
        /// <summary>
        /// 方向盘
        /// </summary>

        public class DirectionUIManager
        {
            private GameObject _mainGO = null;
            private RectTransform _touchDirctionAreaRT = null;
            private RectTransform _touchDirctionRT = null;
            private RectTransform _touchInnerRT = null;

            private float _outerRadius = 0f;
            private float _innerRadius = 0f;

            private const float _deadZone = 15f;

            private float _maxDistance = 55f;
            private float _offset = 55f;

            private Vector2 _center = Vector2.zero;

            private float _lastAngle = 720f;
            private MoveType _lastMoveType = MoveType.NONE;
            public GameObject MainGO
            {
                set
                {
                    if (_mainGO != null || value == null)
                    {
                        return;
                    }
                    _mainGO = value;

                    _touchDirctionAreaRT = _mainGO.GetComponent<RectTransform>();
                    _touchDirctionRT = _touchDirctionAreaRT.Find("TouchDirction") as RectTransform;
                    _touchInnerRT = _touchDirctionRT.Find("TouchCircle") as RectTransform;
                    _outerRadius = _touchDirctionRT.rect.width / 2f;
                    _innerRadius = _touchInnerRT.rect.width / 2f;

                    EventTrigger et = _mainGO.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerDown;
                    entry.callback.AddListener(OnPointerDown);
                    et.triggers.Add(entry);

                    entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerUp;
                    entry.callback.AddListener(OnPointerUp);
                    et.triggers.Add(entry);

                    entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.Drag;
                    entry.callback.AddListener(OnMove);
                    et.triggers.Add(entry);

                    Enable = false;


                }
            }
            public bool Enable
            {
                set
                {
                    if (_mainGO != null)
                    {
                        _mainGO.SetActive(value);
                        if (value)
                        {
                            _maxDistance = _outerRadius - _innerRadius;
                            Clear();
                        }
                    }
                }
            }
            private void OnPointerDown(BaseEventData bed)
            {
                PointerEventData ped = bed as PointerEventData;

                Vector2 touchPos = GameClient.Instance.GetPointInCanvas(ped.position);

                _center.Set(_outerRadius + _offset, _outerRadius + _offset);
                Vector2 dir = touchPos - _center;
                float dis = dir.magnitude;

                _center = touchPos;
                _center.x = CommonFunction.Range(_center.x, _outerRadius + _offset, 440);
                _center.y = CommonFunction.Range(_center.y, _outerRadius + _offset, 410);
                _touchDirctionRT.anchoredPosition = _center;
                _lastAngle = 720f;
                _lastMoveType = MoveType.NONE;
            }

            private void OnPointerUp(BaseEventData bed)
            {
                Clear();
                GameClient.Instance.InputHandle.DirctionTouch(0f, MoveType.STOP);
            }

            private void OnMove(BaseEventData bed)
            {
                PointerEventData ped = bed as PointerEventData;
                Vector2 touchPos = GameClient.Instance.GetPointInCanvas(ped.position);
                if(CommonFunction.GreatZero((touchPos - _center ).magnitude - _deadZone))
                {
                    CalcMove(touchPos, MoveType.MOVE_ANGLE);
                }
                else
                {
                    CalcMove(touchPos, MoveType.PAUSE);
                }
            }

            private void CalcMove(Vector2 touchPos,MoveType moveType)
            {
                Vector2 dir = touchPos - _center;
                float len = dir.magnitude;
                if (CommonFunction.GreatZero(len - _maxDistance))
                {
                    dir = dir / len * _maxDistance;
                }
                _touchInnerRT.anchoredPosition = new Vector2(dir.x, dir.y);

                if(moveType != MoveType.MOVE_ANGLE)
                {
                    if( moveType != _lastMoveType)
                    {
                        GameClient.Instance.InputHandle.DirctionTouch(0f, moveType);
                        _lastMoveType = moveType;
                        _lastAngle = 720f;
                    }
                    return;
                }
                float ang = CommonFunction.GetAngle(dir.x, dir.y);
                ang = Mathf.Floor(ang);
                if (CommonFunction.GreatOrEqualZero(ang - 180f))
                {
                    ang =ang-360f;
                }
                if (!CommonFunction.IsZero(ang - _lastAngle))
                {
                    GameClient.Instance.InputHandle.DirctionTouch(ang, moveType);
                    _lastAngle = ang;
                    _lastMoveType = moveType;
                }
            }

            private void Clear()
            {
                _center.Set(_outerRadius + _offset, _outerRadius + _offset);
                _touchDirctionRT.anchoredPosition = _center;
                _touchInnerRT.anchoredPosition = Vector2.zero;
            }
        }

        public class AttackButtonUIManager
        {
            public GameObject _mainGO = null;

            public GameObject MainGO
            {
                set
                {
                    if(_mainGO != null || value == null)
                    {
                        return;
                    }
                    _mainGO = value;

                    var normalAtk = _mainGO.transform.Find("NormalAtk").GetComponent<Button>();
                    normalAtk.onClick.AddListener(OnNormalAttackUIPointerClick);
                    Enable = false;
                }
            }
            public bool Enable
            {
                set
                {
                    if (_mainGO != null)
                    {
                        _mainGO.SetActive(value);
                    }
                }
            }

            private void OnNormalAttackUIPointerClick()
            {
                GameClient.Instance.InputHandle.FunctionKeyDown(InputDefine.FUNCTION_KEY1);
            }
        }
    }
}
