using ICI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ICI
{

    public class PopupManager : Singleton<PopupManager>
    {
        public Transform Beacon;

        private Dictionary<Type, PopupBase> PopupDict;

        public void SetBeacon(GameObject beacon)
        {
            if (beacon == null)
                return;
            Beacon = beacon.transform;
            Debug.Log("setUibeacon");
        }

        public static PopupBase CreatePopup(string name)
        {
            GameObject popup = ResourcesManager.GetResources(eResourcesPath.Popup,name);
            if(popup == null ) 
                return null;

            GameObject popupInstance = UnityEngine.Object.Instantiate(popup, Instance.Beacon);

            if(popupInstance == null ) return null;
            Instance.setLayer(popupInstance.transform);

            PopupBase result = popupInstance.GetComponent<PopupBase>();
            if(result == null || Instance.PopupDict.ContainsKey(result.GetType()))
            {
                UnityEngine.Object.Destroy(popupInstance);
                return null;
            }

            Instance.PopupDict.Add(result.GetType(), result);
            result.setActive(true);
            Debug.Log("asfe0");
            return result;
        }

        public static T OpenPopup<T>(string popupName) where T : PopupBase
        {
            T popupObj;
            if (!Instance.PopupDict.ContainsKey(typeof(T)))
                popupObj = CreatePopup(popupName) as T;
            else
                popupObj = Instance.PopupDict[typeof(T)] as T;

            if (popupObj == null)
                return null;

            popupObj.setActive(true);
            popupObj.Initialize();

            return popupObj;
        }

        public static bool ClosePopup<T>() where T : PopupBase
        {
            return ClosePopup(GetPopup<T>());
        }

        public static bool ClosePopup(PopupBase T)
        {
            if(T == null)
                return false;
            T.ClosePopup();
            return true;
        }

        public static T GetPopup<T>() where T : PopupBase 
        {
            if(Instance.PopupDict == null || Instance.PopupDict.Count < 1 || ! Instance.PopupDict.ContainsKey(typeof(T)))
                return null;
            else
                return Instance.PopupDict[typeof(T)] as T;
        }

        private void setLayer(Transform parent)
        {
            if(parent == null)
                return;

            parent.gameObject.layer = LayerMask.NameToLayer("UI");
            
            int count = parent.childCount;
            for(int i = 0; i < count; i++)
            {
                Transform target = parent.GetChild(i);
                if (target != null)
                    setLayer(target);
            }

        }


        public override void Initialize()
        {
            PopupDict = new Dictionary<Type, PopupBase>();
        }
    }
}
