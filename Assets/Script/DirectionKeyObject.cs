using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionKeyObject : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.parent == null) { ; }
                else if (hit.transform.parent.gameObject == gameObject)
                    CharacterMove.Instance.inputMove(hit.transform.gameObject.name);
            }
        }
    }
}
