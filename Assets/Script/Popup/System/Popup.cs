using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class Popup<T> : PopupBase where T : class, new()
    {
        public override void ClosePopup()
        {
            setActive(false);
        }

        public override void Initialize()
        {
        }

        public override void setActive(bool active)
        {
            gameObject.SetActive(active);
        }

    }
}
