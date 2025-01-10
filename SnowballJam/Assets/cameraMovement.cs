using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    public AudioSource audioSource; // The AudioSource to play music
    public AudioClip[] songs;       // Array of songs to loop through

    private int currentSongIndex = 0; // Index of the currently playing song


    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] Rigidbody ballRigidBody;
    public float camDistance;
    public float cameraHeightOffset;
    public float rotationSpeed = 100.0f;

    public float currentRotation = 180.0f;


    private Vector3 travelDirection = new Vector3(0,0,1);
    public bool inMovingGame = true;
    public Vector3 desiredPosition;

    private void Start() {
        travelDirection = new Vector3(0,0,1);

        if (songs.Length > 0 && audioSource != null)
        {
            PlayNextSong(); // Start with the first song
        }
    }

    private void Update() {
        // Debug.Log(camDistance);
        float horizontalInput = Input.GetAxis("Horizontal");
        currentRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 direction = rotation * Vector3.back;


        desiredPosition = ballObject.transform.position + direction * camDistance;
        desiredPosition.y += cameraHeightOffset;
        
        if (inMovingGame) {
            cameraObject.transform.position = desiredPosition;
            cameraObject.transform.LookAt(ballObject.transform);
        } else {
            transform.position = GameObject.Find("StationaryPoint").GetComponent<Transform>().position;
        }

        // Check if the current song has finished playing
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            PlayNextSong();
        }
        
    }

    public void changeCamDistance(float dist) {
        camDistance = 10.0f + dist;
    }

    private void PlayNextSong()
    {
        // Set the AudioSource to play the next song
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        // Update the index to loop back to the first song if needed
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
    }
}
