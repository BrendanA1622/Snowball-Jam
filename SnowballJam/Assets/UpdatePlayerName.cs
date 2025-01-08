using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerName : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;

    void Update()
    {
        PlayerData.Instance.PlayerName = playerName.text;
        // Debug.Log(PlayerData.Instance.PlayerName);
    }
}
