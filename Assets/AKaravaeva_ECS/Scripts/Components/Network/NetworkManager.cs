using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 4, IsVisible = false };
        PhotonNetwork.JoinOrCreateRoom("Main", options, TypedLobby.Default);
    }

}
