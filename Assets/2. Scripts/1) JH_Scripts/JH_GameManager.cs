using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_GameManager : MonoBehaviour
{

    public static JH_GameManager instance;


    public GameObject SavePanel;

    public float sceneTime = 0;
    public bool savePanel_On = false;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        

        SavePanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(savePanel_On == true)
        {
            sceneTime += Time.unscaledDeltaTime;
            SaveWaii();
        }


        
    }


    // ╬ю е╦юс
    void SaveWaii()
    {
        
        if(sceneTime > 3 )
        {
            SavePanel.SetActive(false);
            Time.timeScale = 1;
            sceneTime = 0;
            savePanel_On = false;
        }

    }
}
