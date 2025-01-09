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

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(ownName + ", " + PlayerData.Instance.PlayerName);
        // if (!view.IsMine) {
        //     PlayerData.Instance.globalScoreInt = (int)ownBall.score;
        //     PlayerCommunication playerCommunication = GetComponent<PlayerCommunication>();
        //     playerCommunication.CallSendMessage("My name is " + PlayerData.Instance.PlayerName + "   Score: " + PlayerData.Instance.globalScoreInt);
        // }
        // if(!PlayerData.Instance.PlayerName.Equals(ownName)) {
        //     PlayerData.Instance.globalScoreInt = 69;
        //     PlayerCommunication playerCommunication = GetComponent<PlayerCommunication>();
        //     playerCommunication.CallSendMessage("My name is " + PlayerData.Instance.PlayerName + "   Score: " + PlayerData.Instance.globalScoreInt);
        // } else {

        // }
        // PlayerData.Instance.globalScoreInt = (int)ownBall.score;
    }
}
