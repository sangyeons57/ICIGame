using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace ICI
{
    public class PersistantSingleton <T> : MonoBehaviour where T : Component
    {
        protected bool _enabled;

        public static T Instance { get; private set; } = null;

        protected virtual void Awake() 
        {
            if (!Application.isPlaying)
                return;

            if(Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(Instance);
                enabled = true;
            }
            else if (this != Instance)
                Destroy(Instance);
        }

        public void Reset()
        {
            if (Instance == null) return;
            Destroy(Instance);
        }
    }
}
