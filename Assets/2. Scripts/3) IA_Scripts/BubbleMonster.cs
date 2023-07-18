using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMonster : MonoBehaviour
{

    public GameObject bulletStar;
    float currTime;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    IEnumerator makeParticle()
    {
        Destroy(gameObject, 0.5f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(bulletStar, transform.position, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("SS");
            Instantiate(bulletStar, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
        //Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        currTime += Time.deltaTime;

        if (collision.gameObject.tag == "Ground")
        {
            if (currTime > 1f)
            {
                StartCoroutine(makeParticle());
                currTime = 0;
            }
        }
      
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
                currTime = 0;
        }
    }
}
