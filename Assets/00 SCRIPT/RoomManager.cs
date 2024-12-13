using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject player;
    [Space]
    public Transform spawnPoints;

    public GameObject RoomCamera;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        RoomCamera.SetActive(false);
        SpawmnPlayer();

    }
    public void SpawmnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoints.position, Quaternion.identity);
        _player.GetComponent<PlayerSetUp>().IsLocalPlayer();
        _player.GetComponent<Health>().IsLocalPlayer = true;
    }
}