using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    public Move move;
    public GameObject camera;

    public void IsLocalPlayer()
    {
        move.enabled = true;
        camera.SetActive(true);
    }


}
