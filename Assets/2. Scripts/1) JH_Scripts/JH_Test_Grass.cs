using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_Test_Grass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Enemy" || collision.transform.tag != "Ground")
        {
            //Èçµé¸°´Ù.
            StartCoroutine("Wind");
        }
    }

    IEnumerator Wind()
    {
        transform.rotation = Quaternion.Euler(-90, 3 * 5 * Time.deltaTime, 0);
        yield return new WaitForSeconds(0.1f);
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
