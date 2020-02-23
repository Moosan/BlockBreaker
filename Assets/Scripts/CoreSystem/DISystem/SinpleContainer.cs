using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreSystem.DISystem
{
    public class Container
    {
        private Dictionary<string, BindInfo> BindInfos { get; } = new Dictionary<string, BindInfo>();
        public void Bind<T>(string id,BindInfo<T>.CreateDelegate createDelegate)
        {
            var key = BindInfoId.Key<T>(id);
            if (BindInfos.ContainsKey(key))
            {
                Debug.Log(key + " is already binded.");
                // TODO bind type
                return;
            }
            BindInfos.Add(key,new BindInfo<T>(createDelegate));
        }

        public void Bind<T>(string id) where T : new()
        {
            Bind(id, c => new T());
        }
        public T Get<T>(string id) where T : class
        {
            var key = BindInfoId.Key<T>(id);
            if (!BindInfos.ContainsKey(key))
            {
                Debug.Log(key + " is not binded.");
                return null;
            }
            return ((BindInfo<T>)BindInfos[key]).Get(this);
        }

        private static class BindInfoId
        {
            public static string Key<T>(string BindId)
            {
                return BindId + ".." + typeof(T).Name;
            }
        }

        public abstract class BindInfo
        {

        }
        public class BindInfo<T> : BindInfo
        {
            public delegate T CreateDelegate(Container container);
            private CreateDelegate Create { get; }
            private T BindObj { get; set; }
            private bool Created = false;
            public BindInfo(CreateDelegate createDelegate)
            {
                Create = createDelegate;
            }
            public T Get(Container container)
            {
                if (!Created)
                {
                    BindObj = Create(container);
                    Created = true;
                }
                return BindObj;
            }
        }
    }
}