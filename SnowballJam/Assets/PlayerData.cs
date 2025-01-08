using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance; // Singleton instance
    public Color SkinColor { get; set; } // Store skin color
    public string PlayerName { get; set; } // Store player name

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene change
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}