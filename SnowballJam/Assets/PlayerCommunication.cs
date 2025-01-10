using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerCommunication : MonoBehaviour
{
    [SerializeField] ballMovement ownBall;
    public string ownName;

    [PunRPC]
    public void SendMessageToPlayer(string message)
    {
        Debug.Log("Received Message: " + message);
    }

    public void CallSendMessage(string message)
    {
        // Call the RPC
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("SendMessageToPlayer", RpcTarget.All, message);
    }

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        ownName = "templateName";
        // Debug.Log("My own name is: " + ownName);
        ownBall.establishColor();
    }
}
