using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SmoothCameraTransition : MonoBehaviour
{
    [SerializeField] private ballMovement ballObject;
    [SerializeField] private cameraMovement camScript;
    [SerializeField] private GameObject gameUIElements;
    [SerializeField] private GameObject menuUIElements;
    [SerializeField] private cameraMovement cameraScript;
    public Transform targetTransform; // Target position and rotation
    public float duration = 2.0f; // Transition duration in seconds

    private bool isTransitioning = false;

    public Vector3 globTargetPosition;
    public Vector3 globTargetRotation;

    void Start() {
        transform.position = globTargetPosition;
        transform.rotation = Quaternion.Euler(globTargetRotation);
    }

    // public void LoadSceneByIndex(int sceneIndex)
    // {
    //     SceneManager.LoadScene(sceneIndex);
    // }

    public void LoadSceneByIndex(int sceneIndex)
    {
        // Use PhotonNetwork to load the scene across all clients in the room
        PhotonNetwork.LoadLevel(sceneIndex);
    }

    public void LeaveRoomToGo(int sceneIndex) {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(sceneIndex);
    }

    public void MoveCamera()
    {
        transform.position = globTargetPosition;
        transform.rotation = Quaternion.Euler(globTargetRotation);
        if (!isTransitioning)
        {
            camScript.inMovingGame = false;
            gameUIElements.SetActive(false);
            menuUIElements.SetActive(true);
            StartCoroutine(SmoothTransition());
        }
    }

    public void BackToGame() {
        camScript.inMovingGame = true;
        gameUIElements.SetActive(true);
        menuUIElements.SetActive(false);
    }



    private IEnumerator SmoothTransition()
    {
        if (!camScript.inMovingGame) {
            isTransitioning = true;

            Transform cameraTransform = Camera.main.transform;
            Vector3 startPosition = cameraTransform.position;
            Quaternion startRotation = cameraTransform.rotation;

            Vector3 targetPosition = targetTransform.position;
            Quaternion targetRotation = targetTransform.rotation;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                // Smoothstep for smoother transition
                t = t * t * (3f - 2f * t);

                cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
                cameraTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                yield return null; // Wait for the next frame
            }

            cameraTransform.position = targetPosition;
            cameraTransform.rotation = targetRotation;

            isTransitioning = false;
        } else {
            Transform cameraTransform = Camera.main.transform;
            Vector3 startPosition = cameraTransform.position;
            Quaternion startRotation = cameraTransform.rotation;

            Vector3 targetPosition = targetTransform.position;
            Quaternion targetRotation = targetTransform.rotation;

            cameraTransform.position = cameraScript.desiredPosition;
            cameraTransform.LookAt(ballObject.transform);

            isTransitioning = false;
        }
        
    }

    // void Update()
    // {
    //     transform.position = globTargetPosition;
    //     transform.rotation = Quaternion.Euler(globTargetRotation);
    //     isTransitioning = true;

    //     Transform cameraTransform = Camera.main.transform;
    //     Vector3 startPosition = cameraTransform.position;
    //     Quaternion startRotation = cameraTransform.rotation;

    //     Vector3 targetPosition = targetTransform.position;
    //     Quaternion targetRotation = targetTransform.rotation;

    //     cameraTransform.position = targetPosition;
    //     cameraTransform.rotation = targetRotation;

    //     isTransitioning = false;
    // }


}
