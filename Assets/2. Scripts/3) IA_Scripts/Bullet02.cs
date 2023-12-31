using UnityEngine;

public class Bullet02 : MonoBehaviour
{
    public ParticleSystem HitEffect;
    int hitCount;
    public GameObject bulletStar;
    // Start is called before the first frame update
    GameObject starCharge;

 
    void Start()
    {
        starCharge = transform.GetChild(1).gameObject;
        starCharge.SetActive(true);
        Rigidbody bulletRb = GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * 30;
      

    }

    // Update is called once per frame
    void Update()
    {
        starCharge.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * 50f * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        //GameObject.Find("PlayerRanger").GetComponent<PlayerFire>().UpdateClear();

        Destroy(gameObject);
        if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (hitCount < 1)
            {
                Instantiate(HitEffect, collision.transform.position, Quaternion.identity);
                Instantiate(bulletStar, collision.contacts[0].point, Quaternion.identity);
                Destroy(gameObject, 2f);
                hitCount++;
            }

        }
        Destroy(gameObject, 3);
    }

    private void OnCollisionExit(Collision collision)
    {
        hitCount = 0;
    }
    
}
