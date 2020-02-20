using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreSystem.DISystem
{
    public class Container
    {
        private Dictionary<Type, BindInfo> BindInfos { get; } = new Dictionary<Type, BindInfo>();
        public void Bind<T>(BindInfo<T>.CreateDelegate createDelegate) where T : class
        {
            var key = typeof(T);
            if (BindInfos.ContainsKey(key))
            {
                Debug.Log(key + " is already binded.");
                // TODO bind type
                return;
            }
            BindInfos.Add(typeof(T),new BindInfo<T>(createDelegate));
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);
            if (!BindInfos.ContainsKey(key))
            {
                Debug.Log(key + " is not binded.");
                return null;
            }
            return ((BindInfo<T>)BindInfos[typeof(T)]).Get(this);
        }

        public abstract class BindInfo
        {

        }
        public class BindInfo<T> : BindInfo where T : class
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