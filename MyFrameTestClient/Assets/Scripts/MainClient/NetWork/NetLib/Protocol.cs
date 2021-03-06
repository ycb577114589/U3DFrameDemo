﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MainClient
{
    public abstract class Protocol
    {
        public class ProtocolFactory
        {
            public Type protocolType;
            public Queue<Protocol> queue;
            public ProtocolFactory(Type type)
            {
                if(null == type)
                {
                    Debug.LogError("type can't be null");
                }
                protocolType = type;
                queue = new Queue<Protocol>();
            }
            public Protocol Create()
            {
                if(null!= protocolType)
                {
                    System.Object ret = Activator.CreateInstance(protocolType);
                    if(ret is Protocol)
                    {
                        return ret as Protocol;
                    }
                    Debug.LogError("type isn't Protocol");
                }
                return null;
            }
            public Protocol Get()
            {
                if (queue.Count > 0)
                {
                    return queue.Dequeue();
                }
                return Create();
            }
            public void Return (Protocol protocol)
            {
                queue.Enqueue(protocol); 
            }
        }
        #region ProtoclFactory 相关
        public static Dictionary<int, ProtocolFactory> RegisterProtocolFactory = new Dictionary<int, ProtocolFactory>();
        public static Protocol GetProtocolThreadSafe(int type)
        {
            Protocol protocol = null;
            lock (RegisterProtocolFactory)
            {
                ProtocolFactory factory = null;
                if(RegisterProtocolFactory.TryGetValue(type,out factory))
                {
                    protocol = factory.Get();
                }
            }
            return protocol;
        }
        public static void ReturnProtocolThreadSafe(Protocol protocol)
        {
            if(RegisterProtocolFactory!=null && protocol != null)
            {
                lock (RegisterProtocolFactory)
                {
                    ProtocolFactory factory = null;
                    if (RegisterProtocolFactory.TryGetValue(protocol.GetMessageID(),out factory))
                    {
                        factory.Return(protocol);
                    }
                }
            }
        }
        public static bool RegistProtocol(Protocol protocol)
        {
            if(null == protocol)
            {
                return false;
            }
            if (RegisterProtocolFactory.ContainsKey(protocol.GetMessageID()))
            {
                return false;
            }
            RegisterProtocolFactory.Add(protocol.GetMessageID(), new ProtocolFactory(protocol.GetType()));
            return true;
        }
        #endregion
        protected ProtocolErrorCode _threadErrorCode = ProtocolErrorCode.NO_ERROR;
        
        public ProtocolErrorCode ThreadErrorCode
        {
            get
            {
                return _threadErrorCode;
            }
            set
            {
                _threadErrorCode = value;
            }
        }   
        public void SerializeWithHead(MemoryStream stream)
        {   
            long begin = stream.Position;
            ProtocolHead head = ProtocolHead.ShareHead;
            head.Reset();
            head.msgId = GetMessageID();
            head.Serialize(stream);
            Serialize(stream);

            long position = stream.Position;
            int length = (int)(position - begin - 4);
            stream.Position = begin;
            stream.Write(head.GetBytes(length), 0, 4);
            stream.Position = position;
        }
        #region virtual methods
        public virtual UInt16 GetMessageID()
        {
            return 0;
        }
        public virtual bool CheckVaild()
        {
            if (_threadErrorCode == ProtocolErrorCode.DESERIALIZE_ERROR)
            {
                Debug.LogError("Ptc EDeSerializeErr Type:" + GetMessageID().ToString());
                return false;
            }
            return true;
        }
        public virtual void Process()
        {

        }
        #endregion
        #region  abstract
        public abstract void Serialize(MemoryStream stream);
        public abstract void DeSerialize(MemoryStream stream);
        #endregion
    }
}