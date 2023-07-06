using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �̵� ( ������ ��� ) ����� ����


public class JH_GameManager : MonoBehaviour
{
    
    public static JH_GameManager instance;


    public GameObject SavePanel;

    public float sceneTime = 0;
    public bool savePanel_On = false;

    float degree = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        degree = 0;
        
        SavePanel.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //���̵� ��/�ƿ�
            //�÷��̾� ��ġ ����

            // ���� : ī�޶� ��ġ ���󰡾���
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //���̵� ��/�ƿ�
            //�÷��̾� ��ġ ����

            // ���� : ī�޶� ��ġ ���󰡾���
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            //���̵� ��/�ƿ�
            //�÷��̾� ��ġ ����

            // ���� : ī�޶� ��ġ ���󰡾���
        }
    }
}
