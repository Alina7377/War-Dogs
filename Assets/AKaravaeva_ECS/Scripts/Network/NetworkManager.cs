using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPref;
    [SerializeField] private List<Transform> _spawnPoints;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 4, IsVisible = false };
        PhotonNetwork.JoinOrCreateRoom("Main", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        int id = PhotonNetwork.LocalPlayer.ActorNumber;
        if (id > (_spawnPoints.Count + 1))
        {
            Debug.LogError(" Нет свободных точек спавна");
        }
        else
        {
            PhotonNetwork.Instantiate(_playerPref.name, _spawnPoints[id - 1].position, _spawnPoints[id - 1].rotation);
        }
    }
}
