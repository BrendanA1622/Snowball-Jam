using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float baseY;

    public Vector3 startRotation;

    private void Start() {
        
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), baseY, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.Euler(startRotation));
    }
}
