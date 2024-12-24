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

    public Transform tpWeapon;
    
    public void IsLocalPlayer()
    {
        tpWeapon.gameObject.SetActive(false);
        move.enabled = true;
        camera.SetActive(true);
    }
    
    
    [PunRPC]
    public void SetTpweapon(int _weaponIndex)
    {
        foreach (Transform weapon in tpWeapon)
        {
            weapon.gameObject.SetActive(false);
        }
        tpWeapon.GetChild(_weaponIndex).gameObject.SetActive(true);
    }
    
    [PunRPC]
    public void SetNickname(string _name)
    {
        _nickname = _name;
        nicknameText.text = _nickname;
    }


}
