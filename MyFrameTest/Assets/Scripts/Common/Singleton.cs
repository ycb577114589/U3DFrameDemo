using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonLib
{
    public abstract class BaseSingleton {
        public abstract bool Init();
        public abstract void Uninit();

    }
    public abstract class Singleton<T>: BaseSingleton where T : new()
    {

        private static readonly T _Instance = new T();

        protected Singleton()
        {
            if (null != _Instance)
            {
                throw new ApplicationException(_Instance.ToString() + @" can not be created again.");
            }
        }

        public static T Instance
        {
            get
            {
                return _Instance;
            }
        }


        public override bool Init()
        {
            return true;
        }

        public override void Uninit()
        {
        }

    }
}
