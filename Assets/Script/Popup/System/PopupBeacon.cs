using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class PopupBeacon : MonoBehaviour
    {
        private void Start()
        {
            PopupManager.Instance.SetBeacon(gameObject);
        }
    }
}

