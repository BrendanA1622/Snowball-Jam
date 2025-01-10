using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class Leaderboard : MonoBehaviour
{
    private List<string> botNames = new List<string>() {"Capn Obvius",
    "MoustachedWar",
    "OxfordComma",
    "JDzX",
    "BereBery",
    "Dotso",
    "Laney03",
    "CentBox",
    "Gabler",
    "Golemri",
    "Linget",
    "Prattin",
    "SoccerLyfe",
    "TrackerRoz",
    "Boltex",
    "Crawlerildr",
    "Gemmagy",
    "Haymisab",
    "LummoAbove",
    "Revini",
    "Stegoty",
    "weeddi",
    "Briconia",
    "Dinged",
    "Godatro",
    "LawnExtra",
    "MessagesWitch",
    "Scannoyer",
    "TenPrecise",
    "GoodPlayer223",
    "Everma",
    "Lindebasi",
    "Plotiona",
    "Shardis",
    "TigerBoosh",
    "BanditFix",
    "Inextsoft55",
    "Medtershe",
    "RollXan",
    "Washton",
    "Amesiani",
    "BoostMura",
    "Insides",
    "NearlyPool97",
    "ScoobyWow",
    "Truestem",
    "Bracess",
    "LessChronos",
    "NeoRadiant",
    "UnderWa",
    "BristleKemp",
    "TheSnail2",
    "PapaHeadline",
    "RealMore",
    "SpunkyBroadway",
    "xVengeans",
    "Forumenti",
    "Nanosakim",
    "Studison",
    "Bentlor",
    "CowPow1",
    "BeefyCheesy",
    "Landerne",
    "Percsha",
    "Solidgene",
    "TacticAngles",
    "Wizewiz5",
    "Callarts",
    "Hondatash",
    "Lightshma",
    "Pinkin",
    "Spydersans",
    "Talenta",
    "UntamedLlama",
    "QuotePerson87",
    "Stonefire",
    "Biocalo",
    "Currica",
    "Borgizen",
    "Florrekko",
    "Lentiva",
    "MrWar",
    "TigerNees",
    "DragonQ",
    "Godanque",
    "Pongle557",
    "UpforceSign",
    "McNephew",
    "Pandee",
    "Venuest9",
    "SweetieDown",
    "TwinkleStarSprit",
    "TickleMeBatman",
    "Casualte42",
    "JoshXoo",
    "Postic",
    "DatingSimEsport",
    "NaanViolence09",
    "MotoHead",
    "InAMeeting",
    "AlexUhPlayDespot",
    "BloodbathAndBeyo",
    "1v1 Me",
    "CapnTanktop",
    "8BitPoultry",
    "UnstoppableMenbu",
    "GoatOnaMission",
    "SheepOfFury",
    "UntamedSheep",
    "GoatAteMyHomewo",
    "AlarminglyPeppy",
    "GooglyEyeballs",
    "BruhBruh",
    "ReinDoge",
    "SaladCat",
    "WhichButtonIsT",
    "HardcoreCasual",
    "NoJons",
    "ItsMonday"};

    [SerializeField] private List<TextMeshProUGUI> placementLabels;
    private List<string> playingBots = new List<string>() {};
    private List<string> originalNames = new List<string>(); // Optional if needed later
    public bool leaderboardNeedsUpdate = true;
    private string importantName = "";

    public List<int> scores = new List<int>()
    {
        10,9,8,7,6,5,4,3,2,1,0
    };
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    

    private void Start()
    {
        // Randomly select 10 unique bot names
        HashSet<int> selectedIndices = new HashSet<int>();
        while (selectedIndices.Count < 10)
        {
            selectedIndices.Add(Random.Range(0, botNames.Count));
        }

        foreach (int index in selectedIndices)
        {
            playingBots.Add(botNames[index]);
        }

        // playingBots.Add("placeholderName");

        // Initialize random scores for the bots
        for (int i = 0; i < playingBots.Count; i++)
        {
            scores.Add(Random.Range(0, 100)); // Example: Random scores between 0 and 100
        }

        // Keep a copy of the original names (if needed)
        originalNames = new List<string>(playingBots);

        // Sort and update the leaderboard
        UpdateLeaderboard();

    }

    private float updateInterval = 1.0f;
    private float timer = 2.8f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval) {
            leaderboardNeedsUpdate = true;
            timer = 0.0f;
        }



        if (leaderboardNeedsUpdate)
        {
            UpdateLeaderboard();
            leaderboardNeedsUpdate = false; // Prevent redundant updates
        }
    }

    public void RemovePlayer(string playerName)
    {
        // Find the index of the player
        int index = playingBots.IndexOf(playerName);
        if (index >= 0)
        {
            // Remove the player from all relevant lists
            playingBots.RemoveAt(index);
            originalNames.RemoveAt(index);
            scores.RemoveAt(index);

            // Mark leaderboard for update
            leaderboardNeedsUpdate = true;
        }
    }

    private void UpdateLeaderboard()
    {
        // Combine names and scores into pairs
        List<KeyValuePair<string, int>> pairedList = new List<KeyValuePair<string, int>>();
        for (int i = 0; i < playingBots.Count; i++)
        {
            pairedList.Add(new KeyValuePair<string, int>(playingBots[i], scores[i]));
        }

        // Sort by scores in descending order
        pairedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

        // Update the UI directly using the sorted list
        for (int i = 0; i < placementLabels.Count; i++)
        {
            if (i < pairedList.Count)
            {
                string botName = pairedList[i].Key;
                int botScore = pairedList[i].Value;

                placementLabels[i].text = $"{i + 1}. {botName} - {botScore}";
                placementLabels[i].color = botName.Equals(importantName) ? Color.yellow : Color.white;
            }
            else
            {
                // Clear unused labels
                placementLabels[i].text = "";
                placementLabels[i].color = Color.white;
            }
        }
    }

    // private void UpdateLeaderboard()
    // {
    //     // Combine names and scores into pairs
    //     List<KeyValuePair<string, int>> pairedList = new List<KeyValuePair<string, int>>();
    //     for (int i = 0; i < playingBots.Count; i++)
    //     {
    //         pairedList.Add(new KeyValuePair<string, int>(originalNames[i], scores[i]));
    //     }

    //     // Sort by scores in descending order
    //     pairedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

    //     // Update the original lists
    //     playingBots = pairedList.Select(pair => pair.Key).ToList();
    //     scores = pairedList.Select(pair => pair.Value).ToList();

    //     // Update UI labels
    //     for (int i = 0; i < placementLabels.Count; i++)
    //     {
    //         placementLabels[i].text = $"{i + 1}. {playingBots[i]} - {scores[i]}";
    //         // Debug.Log("the string: " + playingBots[i] +"     and the string: " + importantName + "   give the bool: " + playingBots[i].Equals(importantName));
    //         if (playingBots[i].Equals(importantName)) {
    //             placementLabels[i].color = Color.yellow;
    //         } else {
    //             placementLabels[i].color = Color.white;
    //         }
    //     }
    // }

    // Call this method whenever a score changes
    public void UpdateScore(string botName, int newScore)
    {
        int index = playingBots.IndexOf(botName);
        if (index >= 0)
        {
            scores[index] = newScore;
            leaderboardNeedsUpdate = true; // Mark leaderboard for update
        }
    }

    public void setImportantName(string impnm) {
        importantName = impnm;
    }

    public void setPlayerScore(string playerName, int score) {
        int index = playingBots.IndexOf(playerName);

        if (index >= 0)
        {
            scores[index] = score;
        }
        else
        {
            playingBots.Add(playerName);
            originalNames.Add(playerName);
            scores.Add(score);
        }
        // leaderboardNeedsUpdate = true;
    }
}