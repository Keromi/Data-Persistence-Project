
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

        //�������l���珇�ɕ���
        SortedList<int, Profile> profiles = new SortedList<int, Profile>( new DuplicateKeyComparer<int>());
        profile.score = score;
        profiles.Add(score, profile);
        string json = JsonUtility.ToJson(profile);


        //�X�R�A1�ʂ��珇��5�ʂ܂Ői�߂Ă���
        for (int i = 1; i < 6; i++)
        {

            string fileName = GetSaveDataName(i);
            if (File.Exists(fileName))//�t�@�C��������ꍇ
            {

                //�t�@�C����ǂݍ���
                string loadJson = File.ReadAllText(fileName);
                //�ǂݍ��񂾃t�@�C����ϊ�����
                Profile loadProfile = JsonUtility.FromJson<Profile>(loadJson);

                profiles.Add(loadProfile.score, loadProfile);


            }
            else
            {

                //�t�@�C�����Ȃ��ꍇ�X�R�A0���OError�ŋL�^����
                Profile nullProfile = new Profile();
                nullProfile.name = "NoName";
                profiles.Add(nullProfile.score, nullProfile);
            }
        }
        Debug.Log(profiles.Count);
        //��ԃX�R�A������������n�߂�
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
