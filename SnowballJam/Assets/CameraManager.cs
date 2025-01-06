using Photon.Pun;
using UnityEngine;
using System.Linq;

public class CameraManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Initially manage AudioListeners
        ManageCamera();
    }

    // This is automatically called when a new player joins the room
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ManageCamera();
    }

    void Update() {
        // Find all AudioListener components in the scene
        GameObject[] cameraObjects = FindObjectsOfType<Transform>()
            .Where(t => t.name == "MainCamera")
            .Select(t => t.gameObject)
            .ToArray();

        foreach (var cam in cameraObjects)
        {
            // Check if the listener is on the local player's GameObject
            PhotonView photonView = cam.GetComponentInParent<PhotonView>();
            if (photonView != null)
            {
                if (photonView.IsMine)
                {
                    // Enable the local player's AudioListener
                    cam.SetActive(true);
                    Debug.Log("is mine!!");
                    // Debug.Log("Enabled AudioListener on local player: " + listener.gameObject.name);
                }
                else
                {
                    // Disable other players' AudioListeners
                    cam.SetActive(false);
                    Debug.Log("not mine!!");
                    // Debug.Log("Disabled AudioListener on remote player: " + listener.gameObject.name);
                }
            }
            else
            {

                Debug.LogWarning("AudioListener found without a PhotonView parent: " + cam.gameObject.name);
            }
        }
    }

    private void ManageCamera()
    {
        // Find all AudioListener components in the scene
        GameObject[] cameraObjects = FindObjectsOfType<Transform>()
            .Where(t => t.name == "MainCamera")
            .Select(t => t.gameObject)
            .ToArray();

        foreach (var cam in cameraObjects)
        {
            // Check if the listener is on the local player's GameObject
            PhotonView photonView = cam.GetComponentInParent<PhotonView>();
            if (photonView != null)
            {
                if (photonView.IsMine)
                {
                    // Enable the local player's AudioListener
                    cam.SetActive(true);
                    Debug.Log("is mine!!");
                    // Debug.Log("Enabled AudioListener on local player: " + listener.gameObject.name);
                }
                else
                {
                    // Disable other players' AudioListeners
                    cam.SetActive(false);
                    Debug.Log("not mine!!");
                    // Debug.Log("Disabled AudioListener on remote player: " + listener.gameObject.name);
                }
            }
            else
            {

                Debug.LogWarning("AudioListener found without a PhotonView parent: " + cam.gameObject.name);
            }
        }
    }
}
