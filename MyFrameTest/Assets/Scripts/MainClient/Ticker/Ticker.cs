﻿using CommonLib;

namespace MainClient
{
    class TickerManager : Singleton<TickerManager>
    {
        private uint _fixedTickCount = 0;
        private delegate void UpdateDelegate(float delta);
        private delegate void LateUpdateDelegate();
        private delegate void FixedUpdateDelegate(uint fixedTickCount, float delta);

        private UpdateDelegate _ticks = null;
        private LateUpdateDelegate _lateTicks = null;
        private FixedUpdateDelegate _fixedTicks = null;

        public void Update(float delta)
        {
            if(_ticks != null)
            {
                _ticks(delta);
            }
        }
        public void LateUpdate()
        {
            if (_lateTicks != null)
            {
                _lateTicks();
            }
        }
        public void FixedUpdate()
        {
            ++_fixedTickCount;
            if (_fixedTicks != null)
            {
                _fixedTicks(_fixedTickCount,CommonFunction.GAME_TIME_PRE_FRAME);
            }
        }

        public void RemoveTick(ITick tick)
        {
            _ticks -= tick.Update;
        }
        public  void RemoveLateTick(ILateTick lateTick)
        {
            _lateTicks -= lateTick.LateUpdate;  
        }
        public void RemoveFixedTick(IFixedTick fixedTick)
        {
            _fixedTicks -= fixedTick.FixedUpdate;
        }
        public void AddTick(ITick tick)
        {
            _ticks += tick.Update;
        }
        public void AddLateTick(ILateTick lateTick)
        {
            _lateTicks += lateTick.LateUpdate;
        }
        public void AddFixedTick(IFixedTick fixedTick)
        {
            _fixedTicks += fixedTick.FixedUpdate;
        }
        public void ClearTicks()
        {
            _ticks -= _ticks;
            _lateTicks -= _lateTicks;
        }
        public void ClearFixedTicks()
        {
            _fixedTicks -= _fixedTicks;
        }
        public uint FixedTickCount
        {
            set
            {
                _fixedTickCount = value;
            }
        }
        public void DriveToTickCount(uint fixedTickCount)
        {
            while(fixedTickCount>_fixedTickCount)
            {
                FixedUpdate();
            }
        }
        public void ClearAllTicks()
        {
            ClearTicks();
            ClearFixedTicks();
                
        }
    }
}