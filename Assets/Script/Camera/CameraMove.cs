using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICI
{
    public class CameraMove : MonoBehaviour
    {

        public float camSpeed = 10f;
        public float screenSizeThickness = 10f;

        public float limitMoveDistance_width = 4; 
        public float limitMoveDistance_height = 5;

        private void Update()
        {
            Vector3 pos = transform.position;

            //Up
            if(Input.mousePosition.y >= Screen.height - screenSizeThickness &&
               transform.position.z < limitMoveDistance_height //limit
                )
            {
                pos.z += camSpeed * Time.deltaTime;
            }

            //Down
            if(Input.mousePosition.y <= screenSizeThickness &&
                transform.position.z > -limitMoveDistance_height //limit
                )
            {
                pos.z -= camSpeed * Time.deltaTime;
            }

            //Right
            if(Input.mousePosition.x >= Screen.width - screenSizeThickness &&
                transform.position.x < limitMoveDistance_width //limit
                )
            {
                pos.x += camSpeed * Time.deltaTime;
            }

            //Left
            if(Input.mousePosition.x <= screenSizeThickness &&
                transform.position.x > -limitMoveDistance_width //limit
                )
            {
                pos.x -= camSpeed * Time.deltaTime;
            }

            transform.position = pos;
        }

    }
}
