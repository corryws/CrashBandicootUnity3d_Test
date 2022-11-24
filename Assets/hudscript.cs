using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hudscript : MonoBehaviour
{
    public CharacterScript Crash;
    public Text lifeT1ext,scoreText,boxcountText;

    int TotalBox;
    void Awake()
    {
        TotalBox = GameObject.FindGameObjectsWithTag("normalBox").Length + 
                   GameObject.FindGameObjectsWithTag("tntBox")   .Length;
    }

    void Update()
    {
        lifeT1ext.text    = ""+Crash.life;
        scoreText.text    = ""+Crash.wumpascore;
        boxcountText.text = Crash.boxsmash + "/" + TotalBox;
        
        if(Input.GetKey("r"))SceneManager.LoadScene("SceneTest");
    }
}
