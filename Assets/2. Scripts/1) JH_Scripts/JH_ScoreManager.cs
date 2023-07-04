using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JH_ScoreManager : MonoBehaviour
{
    public static JH_ScoreManager instance;

    int coinScore;

    public TextMeshProUGUI textScore;
    readonly string saveKey = "COIN_SCORE";


    public int COIN_SCORE
    {
        get
        {
            return coinScore;
        }
        set
        {
            coinScore = value;
            textScore.text = " " + coinScore;
            PlayerPrefs.SetInt(saveKey, COIN_SCORE);
        }
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        COIN_SCORE = PlayerPrefs.GetInt(saveKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            COIN_SCORE = 0;
        }
    }
}
