using ICI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ICI
{
    public enum eResourcesPath
    {
        None,
        Prefabs,
        Popup
    }
    public class ResourcesManager : Singleton<ResourcesManager>
    {
        static public GameObject GetResources(eResourcesPath resourcesPath, string name)
        {
            return Resources.Load<GameObject>(getPath(resourcesPath) + name);
        }

        static public GameObject GetResources(string path)
        {

            return Resources.Load<GameObject>(path);
        }



        static string getPath(eResourcesPath resourcesPath)
        {
            string path = resourcesPath switch
            {
                eResourcesPath.Prefabs => "Prefabs/",
                eResourcesPath.Popup => "Prefabs/Popup/",
                _=>""

            };

            return path;
        }

        public override void Initialize()
        {
        }
    }
}
