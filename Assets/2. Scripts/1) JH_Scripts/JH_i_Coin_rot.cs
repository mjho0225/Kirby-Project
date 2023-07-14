using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_i_Coin_rot : MonoBehaviour
{
    public static JH_i_Coin_rot instance; 

    public ParticleSystem wave;
    public ParticleSystem star;

    public GameObject mat_Coin;

    bool coinAct;

    public bool monsDrop = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(monsDrop == true)
        {
            coinAct = true;
            GetComponent<Collider>().enabled = false;
            Instantiate(wave, transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (coinAct == true)
        {
            StartCoroutine(CoinEffect());
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && monsDrop == false)
        {
            coinAct = true;
            GetComponent<Collider>().enabled = false;
            Instantiate(wave, transform.position, Quaternion.identity);
            //wave.Play();
        }
    }

    IEnumerator CoinEffect()
    {
        //ª˝º∫µ…∂ß ¿Ã∆Â∆Æ

        
        //wave.Play();
        

        //ø√∂Û∞°∏Èº≠ ¿Ã∆Â∆Æ
        transform.Translate(0, 1 * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, 400 * Time.deltaTime, 0));
        //¿Ã∆Â∆Æ


        yield return new WaitForSeconds(1.5f);

        //πﬂ±§
        mat_Coin.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", new Color(255 / 255, 255 / 255, 255 / 255));

        //¿Ã∆Â∆Æ
        
        //star.Play();
        Destroy(gameObject);
        coinAct = false;
    }

    private void OnDestroy()
    {
        //star.Play();
        Instantiate(star, transform.position, Quaternion.identity);
        monsDrop = false;
    }
}
