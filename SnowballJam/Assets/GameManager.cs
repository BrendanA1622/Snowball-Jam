using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score; // Example variable to transfer

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}