using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    public Move move;
    public GameObject camera;

    private string _nickname;
    
    public TextMeshPro nicknameText;
    public void IsLocalPlayer()
    {
        move.enabled = true;
        camera.SetActive(true);
    }
    
    [PunRPC]
    public void SetNickname(string _name)
    {
        _nickname = _name;
        nicknameText.text = _nickname;
    }


}
