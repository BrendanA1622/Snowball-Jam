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
        PhotonNetwork.JoinOrCreateRoom("Small", new Photon.Realtime.RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        roomIndex = 1;
    }

    public void JoinOrCreateLargeRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Large", new Photon.Realtime.RoomOptions { MaxPlayers = 10 }, TypedLobby.Default);
        roomIndex = 2;
    }

    public override void OnJoinedRoom()
    {
        if (roomIndex == 1) {
            // Load the game scene after joining a room
            PhotonNetwork.LoadLevel("Level1");
        } else if (roomIndex == 2) {
            PhotonNetwork.LoadLevel("Level2");
        }
        
    }
}
