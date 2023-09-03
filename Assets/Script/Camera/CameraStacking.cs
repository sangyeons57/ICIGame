using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraStacking : MonoBehaviour
{
    private void Start()
    {
        var cameraData =
            gameObject.GetComponent<Camera>().GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(UICanvas.Instance.getUICamera());
    }
}
