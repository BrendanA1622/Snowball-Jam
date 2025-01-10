using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallSetup : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    private PhotonView PV;
    public int ballValue;
    public ballMovement myBall;

    
    private Leaderboard leaderboard;
    private bool canUpdateNow = false;


    // Start is called before the first frame update
    void Start()
    {
        
        GameObject leaderboardObject = GameObject.Find("LeaderboardLabel");
        leaderboard = leaderboardObject.GetComponent<Leaderboard>();
        // gameObject.name = "NewObjectName";
        PV = GetComponent<PhotonView>();
        if (PV.IsMine) {

            var playerData = PhotonNetwork.LocalPlayer.CustomProperties;
            string nickname = "error";
            Vector3 colorVector = new Vector3(0,0,0);
            if (playerData.ContainsKey("Nickname")) {
                nickname = (string)playerData["Nickname"];
            }
            if (playerData.ContainsKey("Color"))
            {
                colorVector = (Vector3)playerData["Color"];

                // Renderer renderer = GetComponent<Renderer>();
                // Material targetMaterialInstance = new Material(targetMaterial); // Create an instance of the material
                // targetMaterialInstance.SetColor("_Color", new Color(colorVector.x, colorVector.y, colorVector.z));
                // renderer.material = targetMaterialInstance;
            }

            PV.RPC("RPC_sendMessage", RpcTarget.AllBuffered, nickname);
            PV.RPC("RPC_updateMat", RpcTarget.AllBuffered, colorVector);
            leaderboard.setImportantName(gameObject.name);
            canUpdateNow = true;
        } else {
            canUpdateNow = true;
        }

        
    }

    void Update() {
        if(canUpdateNow) {
            if (PV.IsMine) {
                PV.RPC("RPC_updateScore", RpcTarget.All, gameObject.name, (int)myBall.score);
            }
        }
    }

    public void removeFromBoard(string playerName) {
        leaderboard.RemovePlayer(playerName);
    }

    public void kill() {
        Debug.Log(gameObject.name + " is killed !! ");
        myBall.KillPlayer();
    }

    public void upgrade() {
        Debug.Log(gameObject.name + " is upgraded ;)");
        myBall.addUpgrades(2);
    }

    [PunRPC]
    void RPC_removePlayerBoard(string playerName) {
        Debug.Log("Remove from board: " + playerName);
        GameObject playerOfInterest = GameObject.Find(playerName);
        BallSetup ballSetupOfInterest = playerOfInterest.GetComponent<BallSetup>();
        ballSetupOfInterest.removeFromBoard(playerName);
    }

    [PunRPC]
    void RPC_thisPlayerDead(string playerName) {
        Debug.Log("Shoulda killed: " + playerName);
        GameObject playerOfInterest = GameObject.Find(playerName);
        BallSetup ballSetupOfInterest = playerOfInterest.GetComponent<BallSetup>();
        ballSetupOfInterest.kill();
    }

    [PunRPC]
    void RPC_giveUpgrade(string playerName) {
        Debug.Log("Shoulda upgraded: " + playerName);
        GameObject playerOfInterest = GameObject.Find(playerName);
        BallSetup ballSetupOfInterest = playerOfInterest.GetComponent<BallSetup>();
        ballSetupOfInterest.upgrade();
    }


    [PunRPC]
    void RPC_updateScore(string playerName, int score) {
        // Debug.Log(playerName + "   scores: " + score);
        if(canUpdateNow) {
            // Debug.Log(playerName + " is making here");
            leaderboard.setPlayerScore(playerName, score);
        }
    }



    [PunRPC]
    void RPC_sendMessage(string message) {
        Debug.Log("My name is: " + message);
        gameObject.name = message;
    }


    [PunRPC]
    void RPC_updateMat(Vector3 colorOfInterest) {
        // Debug.Log("My color is: " + colorOfInterest);
        // Renderer renderer = myBall.GetComponent<Renderer>();
        // Material targetMaterialInstance = new Material(targetMaterial); // Create an instance of the material
        // targetMaterialInstance.SetColor("_Color", new Color(colorOfInterest.x, colorOfInterest.y, colorOfInterest.z));
        // renderer.material = targetMaterialInstance;


        if (myBall == null)
        {
            Debug.LogError("myBall is not assigned!");
            return;
        }

        Renderer renderer = myBall.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer not found on myBall!");
            return;
        }

        // Ensure targetMaterial is assigned
        if (targetMaterial == null)
        {
            Debug.LogError("targetMaterial is not assigned!");
            return;
        }

        // Use a new material instance only if necessary
        Material materialInstance = renderer.material; // This creates a unique instance for the GameObject if not already
        if (materialInstance != null)
        {
            Color newColor = new Color(colorOfInterest.x, colorOfInterest.y, colorOfInterest.z);
            if (materialInstance.HasProperty("_Color")) // Check if the property exists
            {
                materialInstance.SetColor("_Color", newColor);
                Debug.Log("Updated material color to: " + newColor);
            }
            else
            {
                Debug.LogError("_Color property not found in the material!");
            }
        }
        else
        {
            Debug.LogError("Failed to create or access material instance.");
        }
    }
}
