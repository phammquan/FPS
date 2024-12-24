using System;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool IsLocalPlayer;


    [Header("HealthText")] public TextMeshProUGUI healthText;
    public bool deadzone;


    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            deadzone = false;
            if (IsLocalPlayer)
            {
                RoomManager.instance.SpawmnPlayer();
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(deadzone)
            TakeDamage(100);
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("DeadZone"))
        {
            deadzone = true;
        }
    }
}