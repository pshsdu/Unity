using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PlayerObject = null;
    float interval = 3f;

    // Update is called once per frame
    void Update()
    {
        if(null != PlayerObject)
        {
            transform.position = new Vector3(0f, PlayerObject.transform.position.y - interval, PlayerObject.transform.position.z - 10f);
        }
        
    }
}