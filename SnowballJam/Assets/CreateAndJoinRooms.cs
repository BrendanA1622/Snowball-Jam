using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    private int roomIndex = 0;
    public void JoinOrCreateSmallRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Small", new Photon.Realtime.RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
        roomIndex = 1;
    }

    public void JoinOrCreateLargeRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Large", new Photon.Realtime.RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
        roomIndex = 2;
    }

    // public override void OnConnectedToMaster()
    // {
    //     Debug.Log("Connected to Master Server");
    //     PhotonNetwork.JoinLobby(); // Optional, depending on your setup
    // }

    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("Joined Lobby");
    //     PhotonNetwork.JoinOrCreateRoom("RoomName", new RoomOptions(), TypedLobby.Default);
    // }

    public override void OnJoinedRoom()
    {
        if (roomIndex == 1) {
            // Load the game scene after joining a room
            PhotonNetwork.LoadLevel("Level1");
        } else if (roomIndex == 2) {
            PhotonNetwork.LoadLevel("Level3");
        }
        
    }
}
