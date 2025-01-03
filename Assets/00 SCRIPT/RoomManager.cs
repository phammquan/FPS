using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject player;
    [Space]
    public Transform[] spawnPoints;
    public GameObject RoomCamera;
    [Space] 
    public GameObject nicknameUI;

    public GameObject connectUI;

    private string _nickname = "Null";
    
    
    void Awake()
    {
        instance = this;
    }

    public void ChangeName(string _name)
    {
        _nickname = _name;
    }

    public void ButtonJoinRoom()
    {
        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
        nicknameUI.SetActive(false);
        connectUI.SetActive(true);
    }
    
    void Start()
    {
       

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
        Transform SpawnPoints = this.spawnPoints[Random.Range(0, this.spawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, SpawnPoints.position, Quaternion.identity);
        _player.GetComponent<PlayerSetUp>().IsLocalPlayer();
        _player.GetComponent<Health>().IsLocalPlayer = true;
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, _nickname);
        PhotonNetwork.NickName = _nickname;
    }
}