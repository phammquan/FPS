using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public bool IsLocalPlayer;


    [Header("HealthText")] public TextMeshProUGUI healthText;


    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            if (IsLocalPlayer)
            {
                RoomManager.instance.SpawmnPlayer();
            }

            Destroy(gameObject);
        }
    }
}