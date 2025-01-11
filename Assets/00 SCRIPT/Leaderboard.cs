using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Leaderboard : MonoBehaviour
{
    public GameObject playerList;
    [Header("Option")] public float refreshRate = 1f;

    [Space] public GameObject[] slot;
    [Space] public TextMeshProUGUI[] nameText;
    public TextMeshProUGUI[] ScoreText;


    void Start()
    {
        InvokeRepeating(nameof(Repeat), 1f, refreshRate);
    }

    public void Repeat()
    {
        foreach (var _slot in slot)
        {
            _slot.SetActive(false);
        }

        var sortedPlayerList =
            (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();
        int i = 0;
        foreach (var _player in sortedPlayerList)
        {
            slot[i].SetActive(true);
            if (_player.NickName == "")
            {
                _player.NickName = "Player " + i;
            }

            nameText[i].text = _player.NickName;
            ScoreText[i].text = _player.GetScore().ToString();
            i++;
        }
    }

    void Update()
    {
        if (RoomManager.Instance._isConnected)
            playerList.SetActive(Input.GetKey(KeyCode.Tab));
    }
}