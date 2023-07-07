using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스테이지 이동 ( 개발자 모드 ) 만들어 놓기


public class JH_GameManager : MonoBehaviour
{
    
    public static JH_GameManager instance;

    public GameObject respawnPos1;
    public GameObject respawnPos2;
    public GameObject respawnPos3;
    public GameObject respawnPos4;
    public GameObject respawnPos5;
    public GameObject respawnPos6;
    public GameObject respawnPos7;

    public GameObject playerKirby;
    //public GameObject playerCar;


    public GameObject StartVideo;
    public GameObject ButtonVideo;
    public GameObject EndingVideo;
    public GameObject StartButton;

    public bool changePlayer = false;

    public GameObject SavePanel;

    public float sceneTime = 0;
    float startTime = 0;
    bool playing = false;

    public bool savePanel_On = false;

    float degree = 0;

    public Image image_Fade;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

        
        ButtonVideo.SetActive(false);
        EndingVideo.SetActive(false);
    }

    void Start()
    {
        degree = 0;
        
        SavePanel.SetActive(false);

        //Time.timeScale = 0;


    }

    // Update is called once per frame
    void Update()
    {
        degree += Time.deltaTime;
        if (degree >= 360)
        {
            degree = 0;
        }

        RenderSettings.skybox.SetFloat("_Rotation", degree);


        if(savePanel_On == true)
        {
            sceneTime += Time.unscaledDeltaTime;
            SaveWaii();
        }

        //if (changePlayer == true)
        //{
        //    playerKirby.SetActive(false);
        //    playerCar.SetActive(true);
        //}

        //if (changePlayer == false)
        //{
        //    playerKirby.SetActive(true);
        //    playerCar.SetActive(false);
        //}

        GM_Mode();


        if (startTime >= 3 && playing == true)
        {
            StartCoroutine("FadeIn");
            if(startTime >= 6)
            {
                StartCoroutine("FadeOut");
                playing = false;
            }
            
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
        if(playerKirby != null)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                //페이드 인/아웃

                //플레이어 위치 변경
                playerKirby.transform.position = respawnPos1.transform.position;

                // 주의 : 카메라 위치 따라가야함
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                //페이드 인/아웃
                //플레이어 위치 변경
                playerKirby.transform.position = respawnPos2.transform.position;

                // 주의 : 카메라 위치 따라가야함
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                //페이드 인/아웃
                //플레이어 위치 변경
                playerKirby.transform.position = respawnPos3.transform.position;

                // 주의 : 카메라 위치 따라가야함
            }
        }
        
    }


    public void OnClickStartButton()
    {
        print("버튼클릭");
        StartVideo.SetActive(false);
        ButtonVideo.SetActive(true);
        Time.timeScale = 1;
        
        startTime = 0;
        playing = true;

        StartButton.SetActive(false);
    }

    


    public IEnumerator FadeOut()
    {

        Color color = image_Fade.color;
        while (color.a > 0)
        {
            color.a -= Time.unscaledDeltaTime;
            image_Fade.color = color;
            yield return null;
        }

    }

    public IEnumerator FadeIn()
    {

        Color color = image_Fade.color;
        while (color.a < 1)
        {
            color.a += Time.unscaledDeltaTime;
            image_Fade.color = color;
            yield return null;
        }
    }

}
