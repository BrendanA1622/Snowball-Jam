using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySimpleMovement : MonoBehaviour
{
    [SerializeField] GameObject otherEnemy;
    [SerializeField] GameObject otherEnemy2;
    [SerializeField] GameObject otherEnemy3;
    [SerializeField] GameObject otherEnemy4;
    [SerializeField] GameObject otherEnemy5;
    [SerializeField] GameObject otherEnemy6;
    [SerializeField] GameObject otherEnemy7;
    [SerializeField] GameObject otherEnemy8;
    [SerializeField] GameObject otherEnemy9;
    [SerializeField] GameObject originalBallObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] GameObject ballObjectGhost;

    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject touchGObject;
    [SerializeField] ParticleSystem endParticles;


    private Vector3 startPosition;
    public Vector3 startScale;
    public float growthFactor = 0.01f;
    public float maxScale = 5.0f;
    public float minScale = 0.3f;
    public float camDistScale = 4.0f;

    public float forceMagnitude = 500.0f;
    public float speedyForceMag = 1000.0f;
    public float normalForceMag = 500.0f;
    public float baseMass;
    public float massScaling;


    public Terrain terrain; // Assign the terrain in the Inspector
    private TerrainData terrainData;
    private Vector3 terrainPosition;
    public int terrainLayerIndex = 0;
    private bool firstEndEmit = true;
    private float speed;
    private float scaleIncrease;
    private Vector3 newScale;

    [SerializeField] Leaderboard leaderboard;
    [SerializeField] int playerIndex;



    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        transform.localPosition = startPosition;
        transform.localScale = startScale;
        if (terrain != null)
        {
            terrainData = terrain.terrainData;
            terrainPosition = terrain.GetPosition(); // Terrain world position
        }
        else
        {
            Debug.LogError("Please assign the terrain in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        leaderboard.scores[playerIndex] = (int)(rb.transform.localScale.magnitude * 100f);
        
        
        if (terrainData != null)
        {
            Vector3 ballPosition = transform.position;

            // Get the terrain's local coordinates
            float relativeX = (ballPosition.x - terrainPosition.x) / terrainData.size.x;
            float relativeZ = (ballPosition.z - terrainPosition.z) / terrainData.size.z;

            // Get the corresponding texture index
            terrainLayerIndex = GetTerrainLayerAtPosition(relativeX, relativeZ);

            // Debug.Log("Current Terrain Layer Index: " + terrainLayerIndex);
        }
    }

    private void FixedUpdate() {
        if (touchGObject != null) {
            EnemyTG tgScript = touchGObject.GetComponent<EnemyTG>();
            if (tgScript != null) {
                if (tgScript.isGrounded) {
                    Vector3 directionToBall = originalBallObject.transform.position - transform.position;
                    Vector3 directionToEnemy = otherEnemy.transform.position - transform.position;
                    Vector3 directionToEnemy2 = otherEnemy2.transform.position - transform.position;
                    Vector3 directionToEnemy3 = otherEnemy3.transform.position - transform.position;
                    Vector3 directionToEnemy4 = otherEnemy4.transform.position - transform.position;
                    Vector3 directionToEnemy5 = otherEnemy5.transform.position - transform.position;
                    Vector3 directionToEnemy6 = otherEnemy6.transform.position - transform.position;
                    Vector3 directionToEnemy7 = otherEnemy7.transform.position - transform.position;
                    Vector3 directionToEnemy8 = otherEnemy8.transform.position - transform.position;
                    Vector3 directionToEnemy9 = otherEnemy9.transform.position - transform.position;
                    directionToBall.y = 0;
                    directionToEnemy.y = 0;
                    directionToEnemy2.y = 0;
                    directionToEnemy3.y = 0;
                    directionToEnemy4.y = 0;
                    directionToEnemy5.y = 0;
                    directionToEnemy6.y = 0;
                    directionToEnemy7.y = 0;
                    directionToEnemy8.y = 0;
                    directionToEnemy9.y = 0;
                    float direction;
                    float shortestLength = Mathf.Min(directionToBall.magnitude, directionToEnemy.magnitude, directionToEnemy2.magnitude, directionToEnemy3.magnitude, directionToEnemy4.magnitude);
                    if (shortestLength == directionToBall.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToBall, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy2.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy2, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy3.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy3, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy4.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy4, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy5.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy5, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy6.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy6, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy7.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy7, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy8.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy8, Vector3.up) + 180.0f;
                    } else if (shortestLength == directionToEnemy9.magnitude) {
                        direction = Vector3.SignedAngle(Vector3.forward, directionToEnemy9, Vector3.up) + 180.0f;
                    } else {
                        direction = 0f;
                    }
                    
                    // Debug.Log("X Force Direction: " + direction);

                    rb.AddForce((float)(Time.deltaTime * forceMagnitude * (Mathf.Pow(rb.transform.localScale.magnitude,3)) * -1.0 * Math.Sin((direction/360.0) * (2 * Math.PI))),0,(float)(Time.deltaTime * (forceMagnitude * (Mathf.Pow(rb.transform.localScale.magnitude,3))) * -1.0 * Math.Cos((direction/360.0) * (2 * Math.PI))));

                    speed = rb.velocity.magnitude;
                    scaleIncrease = speed * growthFactor * Time.deltaTime;
                    newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                    newScale = Vector3.Min(newScale, Vector3.one * maxScale);
                    rb.mass = baseMass + massScaling * Mathf.Pow(newScale.magnitude, 3);
                    rb.transform.localScale = newScale;

                    forceMagnitude = normalForceMag;

                    // if (tgScript.onSnow || terrainLayerIndex == 1 || terrainLayerIndex == 2) {
                        
                    // } else {
                    //     speed = rb.velocity.magnitude;
                    //     scaleIncrease = -speed * growthFactor * 2.5f * Time.deltaTime;
                    //     newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                    //     newScale = Vector3.Max(newScale, Vector3.one * minScale);
                    //     rb.mass = baseMass + massScaling * Mathf.Pow(newScale.magnitude, 3);
                    //     rb.transform.localScale = newScale;
                    // }

                    // if (terrainLayerIndex == 2) {
                    //     forceMagnitude = speedyForceMag;
                    // } else {
                    //     
                    // }
                }
                scaleIncrease = -growthFactor * 3f * Time.deltaTime;
                newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                newScale = Vector3.Max(newScale, Vector3.one * minScale);
                rb.mass = baseMass + massScaling * Mathf.Pow(newScale.magnitude, 3);
                rb.transform.localScale = newScale;
            }
        }
        if (rb.transform.localScale.magnitude <= (Vector3.one * minScale).magnitude) {
            StartCoroutine(EmitParticlesAndReset());
        }
    }

    public void KillEnemy() {
        StartCoroutine(EmitParticlesAndReset());
    }

    private IEnumerator EmitParticlesAndReset() {
        if (endParticles != null && firstEndEmit) {
            // ballMovement ballScript = ballObject.GetComponent<ballMovement>();
            // ballScript.increaseScale(transform.localScale);
                    
            endParticles.Play();
            firstEndEmit = false;
            MeshRenderer meshRenderer = ballObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            MeshRenderer meshRendererGhost = ballObjectGhost.GetComponent<MeshRenderer>();
            Collider colliderOfInterest = ballObject.GetComponent<Collider>();
            colliderOfInterest.enabled = false;
            meshRendererGhost.enabled = false;
        }
        yield return new WaitForSeconds(2.0f);
        if (!firstEndEmit) {
            transform.localPosition = startPosition;
            transform.localScale = startScale;
            MeshRenderer meshRenderer = ballObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            MeshRenderer meshRendererGhost = ballObjectGhost.GetComponent<MeshRenderer>();
            meshRendererGhost.enabled = true;
            Collider colliderOfInterest = ballObject.GetComponent<Collider>();
            colliderOfInterest.enabled = true;
            firstEndEmit = true;
        }
        
    }


    int GetTerrainLayerAtPosition(float x, float z)
    {
        // Convert normalized coordinates to TerrainData resolution
        int mapX = Mathf.Clamp((int)(x * terrainData.alphamapWidth), 0, terrainData.alphamapWidth - 1);
        int mapZ = Mathf.Clamp((int)(z * terrainData.alphamapHeight), 0, terrainData.alphamapHeight - 1);

        // Get the terrain layer weights at this point
        float[,,] alphaMap = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        float[] layerWeights = new float[alphaMap.GetLength(2)];

        for (int i = 0; i < layerWeights.Length; i++)
        {
            layerWeights[i] = alphaMap[0, 0, i];
        }

        // Find the layer with the maximum weight
        int dominantLayer = 0;
        float maxWeight = 0;

        for (int i = 0; i < layerWeights.Length; i++)
        {
            if (layerWeights[i] > maxWeight)
            {
                maxWeight = layerWeights[i];
                dominantLayer = i;
            }
        }

        return dominantLayer; // Return the index of the dominant terrain layer
    }
}
