using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    abstract public class PopupBase : MonoBehaviour
    {
        abstract public void setActive(bool active);
        abstract public void ClosePopup();
        abstract public void Initialize();
    }
}
