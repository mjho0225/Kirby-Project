using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_i_Coin_rot : MonoBehaviour
{

    bool coinAct;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (collision.gameObject.tag == "Player")
        {
            coinAct = true;
            GetComponent<Collider>().enabled = false; 
        }
    }

    IEnumerator CoinEffect()
    {
        
        //�ö󰡸鼭 ����Ʈ
        transform.Translate(0, 1 * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, 300 * Time.deltaTime, 0));
        //����Ʈ

        yield return new WaitForSeconds(1.5f);

        //����Ʈ

        Destroy(gameObject);
        coinAct = false;
    }
}
