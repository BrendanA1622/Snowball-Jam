using Photon.Pun;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviourPun
{
    public Canvas playerCanvas;

    void Start()
    {
        // Get the Canvas component attached to the player prefab
        playerCanvas = GetComponentInChildren<Canvas>();

        if (photonView.IsMine)
        {
            // Enable the Canvas only for the local player
            playerCanvas.enabled = true;
        }
        else
        {
            // Disable the Canvas for other players
            playerCanvas.enabled = false;
        }
    }
}