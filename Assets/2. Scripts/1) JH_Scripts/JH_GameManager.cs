using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �������� �̵� ( ������ ��� ) ����� ����


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


    // �� Ÿ��
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
                //���̵� ��/�ƿ�

                //�÷��̾� ��ġ ����
                playerKirby.transform.position = respawnPos1.transform.position;

                // ���� : ī�޶� ��ġ ���󰡾���
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                //���̵� ��/�ƿ�
                //�÷��̾� ��ġ ����
                playerKirby.transform.position = respawnPos2.transform.position;

                // ���� : ī�޶� ��ġ ���󰡾���
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                //���̵� ��/�ƿ�
                //�÷��̾� ��ġ ����
                playerKirby.transform.position = respawnPos3.transform.position;

                // ���� : ī�޶� ��ġ ���󰡾���
            }
        }
        
    }


    public void OnClickStartButton()
    {
        print("��ưŬ��");
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
