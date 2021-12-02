using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIHandler : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputField;
    public void GameStart()
    {
        
        PlayerProfile.GetInstance().Name = inputField.text;
        SceneManager.LoadScene(1);


    }
    public void SceneChange(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
