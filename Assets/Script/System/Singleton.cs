using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;

namespace ICI
{
    public abstract class Singleton<T> : ISingleton where T : class, ISingleton , new()
    {
        private static T instance = null;
        public static T Instance
        {
            get 
            {
                if(instance == null)
                {
                    instance = new T();
                    instance.Initialize();
                }
                return instance;
            }
        }

        public abstract void Initialize();
    }
}
