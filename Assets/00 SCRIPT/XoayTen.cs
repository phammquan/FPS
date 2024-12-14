using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XoayTen : MonoBehaviour
{
   

    // Update is called once per fra
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
