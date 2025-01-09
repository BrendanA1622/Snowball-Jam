using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallSetup : MonoBehaviour
{
    private PhotonView PV;
    public int ballValue;
    public ballMovement myBall;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine) {
            PV.RPC("updateMatColor", RpcTarget.AllBuffered, new Vector3(0.5f,1.0f,0.5f));
            Debug.Log("WORKING ON THIS");
        }
    }

    [PunRPC]
    void updateMatColor(Vector3 vecColor) {
        if(myBall) {
            myBall.setColor(vecColor);
        }
    }
}
