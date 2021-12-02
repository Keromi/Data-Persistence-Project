using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class ScoreManager : MonoBehaviour
{

    [SerializeField]private List<TextMeshProUGUI> scoreTexts;
    Profile[] playerProfiles = new Profile[5];
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 1; i < 6; i++)
        {

            string fileName = PlayerProfile.GetSaveDataName(i);
            string json = File.ReadAllText(fileName);            
            playerProfiles[i - 1] = JsonUtility.FromJson<Profile>(json);

            scoreTexts[i - 1].text = "Rank" + i + ": " + "Name:" + playerProfiles[i - 1].name + " Score:" + playerProfiles[i - 1].score;


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
