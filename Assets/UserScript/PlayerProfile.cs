
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;


public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
{
    #region IComparer<TKey> Members

    public int Compare(TKey x,TKey y)
    {
        int result = x.CompareTo(y);

        if (result == 0) return 1;
        else return result;

    }


    #endregion
}

[System.Serializable]
 class Profile
{

    public string name = "NoName";
    public int score = 0;

}

public class PlayerProfile : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerProfile GetInstance()
    {
        return Instance;
    }
   
    public static string GetSaveDataName(int i)
    {
        string fileName = Application.persistentDataPath + "/savefile" + i + ".json";

        return fileName;

    }

    
    private Profile profile;
    private static PlayerProfile Instance;
    int currentBestScore;
    

    void Start()
    {

       
        if(Instance != null)
        {
            
            Destroy(gameObject);
            return;
        }

        Instance = this;

        profile = new Profile();

        Score(0);

        DontDestroyOnLoad(gameObject);

    }
    


        void Score(int score)
        {

        //小さい値から順に並ぶ
        SortedList<int, Profile> profiles = new SortedList<int, Profile>( new DuplicateKeyComparer<int>());
        profile.score = score;
        profiles.Add(score, profile);
        string json = JsonUtility.ToJson(profile);


        //スコア1位から順に5位まで進めていく
        for (int i = 1; i < 6; i++)
        {

            string fileName = GetSaveDataName(i);
            if (File.Exists(fileName))//ファイルがある場合
            {

                //ファイルを読み込む
                string loadJson = File.ReadAllText(fileName);
                //読み込んだファイルを変換する
                Profile loadProfile = JsonUtility.FromJson<Profile>(loadJson);

                profiles.Add(loadProfile.score, loadProfile);


            }
            else
            {

                //ファイルがない場合スコア0名前Errorで記録する
                Profile nullProfile = new Profile();
                nullProfile.name = "NoName";
                profiles.Add(nullProfile.score, nullProfile);
            }
        }
        Debug.Log(profiles.Count);
        //一番スコアが高い順から始める
        for (int i = profiles.Count - 1; i >= 1; i--)
        {
            var values = profiles.Values;
            string toJson = JsonUtility.ToJson(values[i]);
            string fileName = GetSaveDataName(6 - i);
            File.WriteAllText(fileName, toJson);
            Debug.Log(fileName);
            Debug.Log("Wrire SaveData");

        }
    }

    public void GameEnd(int score)
    {
        Score(score);

        currentBestScore = score;


    }

    public int GetBestScore()
    {
        return currentBestScore;
    }
    public string Name { get { return profile.name; } set { profile.name = value; } }
   
    public int GetScore()
    {
        return profile.score;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
