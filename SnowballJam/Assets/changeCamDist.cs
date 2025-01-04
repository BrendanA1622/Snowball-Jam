using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeCamDist : MonoBehaviour
{
    // [SerializeField] GameObject cameraObject;
    
    // [SerializeField] GameObject ballObject;
    // public Slider slider;

    // private void Start() {
    //     slider.value = PlayerPrefs.GetFloat("camDistance");
    // }




    // public void Save() {
    //     PlayerPrefs.SetFloat("camDistance",slider.value);
    // }

    [SerializeField] Slider distSlider;
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject ballObject;
    [SerializeField] public float DefaultCamDistance;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializePlayerPrefs()
    {
        // Debug log to confirm initialization
        Debug.Log("Initializing PlayerPrefs...");

        // Reset specific PlayerPrefs keys for testing (or set defaults)
        if (!PlayerPrefs.HasKey("camDistance"))
        {
            PlayerPrefs.SetFloat("camDistance", 0.1f); // Default value for camDistance
            Debug.Log("Set default camDistance to 0.25f.");
        }
        else
        {
            Debug.Log($"camDistance already exists with value: {PlayerPrefs.GetFloat("camDistance")}");
            PlayerPrefs.SetFloat("camDistance", 0.1f);
        }

        PlayerPrefs.Save(); // Save changes to disk
    }



    
    void Start() {
        // Initialize PlayerPrefs with a default value if "camDistance" doesn't exist
        if (!PlayerPrefs.HasKey("camDistance"))
        {
            PlayerPrefs.SetFloat("camDistance", DefaultCamDistance); // Default value
            PlayerPrefs.Save(); // Save changes to disk
            Debug.Log("MADE HERE!");
        }
        // PlayerPrefs.SetFloat("camDistance", 0.2f);
        ballMovement ballScript = ballObject.GetComponent<ballMovement>();
        ballScript.camDistScale = 0.0f + (distSlider.value * 25.0f);
        distSlider.value = PlayerPrefs.GetFloat("camDistance");
        Load();
        Debug.Log("dist val: " + DefaultCamDistance);
    }

    public void ChangeCamDist() {
        ballMovement ballScript = ballObject.GetComponent<ballMovement>();
        ballScript.camDistScale = 0.0f + (distSlider.value * 25.0f);
    }

    private void Load() {
        distSlider.value = PlayerPrefs.GetFloat("camDistance");
    }

    public void Save() {
        PlayerPrefs.SetFloat("camDistance",distSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log("PREF: " + PlayerPrefs.GetFloat("camDistance"));
        ballMovement ballScript = ballObject.GetComponent<ballMovement>();
        ballScript.camDistScale = 0.0f + (distSlider.value * 25.0f);
    }
}
