using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballMovement : MonoBehaviour
{
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject touchGObject;
    [SerializeField] ParticleSystem endParticles;


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

    private Vector3 startPosition;
    private Vector3 startScale;
    private MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
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
            touchingGround tgScript = touchGObject.GetComponent<touchingGround>();
            cameraMovement camScript = cameraObject.GetComponent<cameraMovement>();
            if (tgScript != null) {
                camScript.camDistance = 5.0f + rb.transform.localScale.magnitude * camDistScale;
                camScript.cameraHeightOffset = camScript.camDistance * (4.553655f/10.0f);
                if (tgScript.isGrounded) {
                    float direction = camScript.currentRotation;

                    rb.AddForce((float)(Time.deltaTime * forceMagnitude * (Mathf.Pow(rb.transform.localScale.magnitude,3)) * Input.GetAxis("Vertical") * Math.Sin((direction/360.0) * (2 * Math.PI))),0,(float)(Time.deltaTime * (forceMagnitude * (Mathf.Pow(rb.transform.localScale.magnitude,3))) * Input.GetAxis("Vertical") * Math.Cos((direction/360.0) * (2 * Math.PI))));


                    if (tgScript.onSnow || terrainLayerIndex == 1 || terrainLayerIndex == 2) {
                        speed = rb.velocity.magnitude;
                        scaleIncrease = speed * growthFactor * Time.deltaTime;
                        newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                        newScale = Vector3.Min(newScale, Vector3.one * maxScale);
                        rb.mass = baseMass + massScaling * Mathf.Pow(newScale.magnitude, 3);
                        rb.transform.localScale = newScale;
                    } else {
                        speed = rb.velocity.magnitude;
                        scaleIncrease = -speed * growthFactor * 2.5f * Time.deltaTime;
                        newScale = rb.transform.localScale + Vector3.one * scaleIncrease;
                        newScale = Vector3.Max(newScale, Vector3.one * minScale);
                        rb.mass = baseMass + massScaling * Mathf.Pow(newScale.magnitude, 3);
                        rb.transform.localScale = newScale;
                    }

                    if (terrainLayerIndex == 2) {
                        forceMagnitude = speedyForceMag;
                    } else {
                        forceMagnitude = normalForceMag;
                    }
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

    public void increaseScale(Vector3 scaleIncrease) {
        rb.transform.localScale += scaleIncrease;
    }

    public void KillPlayer() {
        StartCoroutine(EmitParticlesAndReset());
    }

    public IEnumerator EmitParticlesAndReset() {
        if (endParticles != null && firstEndEmit) {
            endParticles.Play();
            firstEndEmit = false;
            meshRenderer = ballObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            Collider colliderOfInterest = ballObject.GetComponent<Collider>();
            colliderOfInterest.enabled = false;
            rb.useGravity = false;
        }
        yield return new WaitForSeconds(2.0f);
        if (!firstEndEmit) {
            transform.localPosition = startPosition;
            transform.localScale = startScale;
            meshRenderer = ballObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            Collider colliderOfInterest = ballObject.GetComponent<Collider>();
            colliderOfInterest.enabled = true;
            rb.useGravity = true;
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
