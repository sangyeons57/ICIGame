using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : PersistantSingleton<UICanvas>
{
    [SerializeField] Camera UICamera;

    public Camera getUICamera()
    {
        return UICamera;
    } 
}
