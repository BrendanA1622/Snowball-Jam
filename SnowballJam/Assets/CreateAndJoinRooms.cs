using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private FlexibleColorPicker playerColorPicker;
    private int roomIndex = 0;

    void Start() {
        playerColorPicker.color = Color.white;
        playerNameInput.text = "Default Baller";

        var playerData = PhotonNetwork.LocalPlayer.CustomProperties;
        string nickname = "error";
        Vector3 colorVector = new Vector3(0,0,0);
        if (playerData.ContainsKey("Nickname")) {
            nickname = (string)playerData["Nickname"];
            playerNameInput.text = nickname;
        }
        if (playerData.ContainsKey("Color"))
        {
            colorVector = (Vector3)playerData["Color"];
            playerColorPicker.color = new Color(colorVector.x,colorVector.y,colorVector.z);

            // Renderer renderer = GetComponent<Renderer>();
            // Material targetMaterialInstance = new Material(targetMaterial); // Create an instance of the material
            // targetMaterialInstance.SetColor("_Color", new Color(colorVector.x, colorVector.y, colorVector.z));
            // renderer.material = targetMaterialInstance;
        }
    }

    public void JoinOrCreateSmallRoom()
    {
        ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable();
        playerData["Nickname"] = playerName.text; // Replace with actual player nickname
        playerData["Color"] = new Vector3(playerColorPicker.GetColor().r, playerColorPicker.GetColor().g, playerColorPicker.GetColor().b); // Replace with RGB values for color
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerData);

        PhotonNetwork.JoinOrCreateRoom("Small", new Photon.Realtime.RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
        roomIndex = 1;
    }

    public void JoinOrCreateLargeRoom()
    {
        ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable();
        playerData["Nickname"] = playerName.text; // Replace with actual player nickname
        playerData["Color"] = new Vector3(playerColorPicker.GetColor().r, playerColorPicker.GetColor().g, playerColorPicker.GetColor().b); // Replace with RGB values for color
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerData);

        PhotonNetwork.JoinOrCreateRoom("Large", new Photon.Realtime.RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
        roomIndex = 2;

    }

    void Update() {
        // playerName.text = "poopypants";
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // Set player data
        // ExitGames.Client.Photon.Hashtable playerData = new ExitGames.Client.Photon.Hashtable();
        // playerData["Nickname"] = playerName.text; // Replace with actual player nickname
        // playerData["Color"] = new Vector3(playerColorPicker.GetColor().r, playerColorPicker.GetColor().g, playerColorPicker.GetColor().b); // Replace with RGB values for color
        // PhotonNetwork.LocalPlayer.SetCustomProperties(playerData);

        if (roomIndex == 1) {
            // Load the game scene after joining a room
            PhotonNetwork.LoadLevel("Level1");
        } else if (roomIndex == 2) {
            PhotonNetwork.LoadLevel("Level3");
        }
        
    }
}
