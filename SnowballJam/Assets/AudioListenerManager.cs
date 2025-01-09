using Photon.Pun;
using UnityEngine;

public class AudioListenerManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Initially manage AudioListeners
        ManageAudioListeners();
    }

    // This is automatically called when a new player joins the room
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ManageAudioListeners();
    }

    private void ManageAudioListeners()
    {
        // Find all AudioListener components in the scene
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        foreach (var listener in audioListeners)
        {
            // Check if the listener is on the local player's GameObject
            PhotonView photonView = listener.GetComponentInParent<PhotonView>();
            if (photonView != null)
            {
                if (photonView.IsMine)
                {
                    // Enable the local player's AudioListener
                    listener.enabled = true;
                    // Debug.Log("Enabled AudioListener on local player: " + listener.gameObject.name);
                }
                else
                {
                    // Disable other players' AudioListeners
                    listener.enabled = false;
                    // Debug.Log("Disabled AudioListener on remote player: " + listener.gameObject.name);
                }
            }
            else
            {
                // Debug.LogWarning("AudioListener found without a PhotonView parent: " + listener.gameObject.name);
            }
        }
    }
}
