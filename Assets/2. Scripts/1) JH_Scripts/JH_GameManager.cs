using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스테이지 이동 ( 개발자 모드 ) 만들어 놓기


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


    // 씬 타임
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


    void GM_Mode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //페이드 인/아웃
            //플레이어 위치 변경

            // 주의 : 카메라 위치 따라가야함
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //페이드 인/아웃
            //플레이어 위치 변경

            // 주의 : 카메라 위치 따라가야함
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            //페이드 인/아웃
            //플레이어 위치 변경

            // 주의 : 카메라 위치 따라가야함
        }
    }
}
