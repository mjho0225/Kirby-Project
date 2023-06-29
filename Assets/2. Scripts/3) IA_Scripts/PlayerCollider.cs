using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // ���� ������ �� ���ʹ� �±׿� �ε����ٸ� Destroy
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("other.gameObject.layer" + other.gameObject.layer);
        //6: enemy
        //emptyState == true
        if (other.gameObject.layer == 6)
        {
            PlayerPull pk = GameObject.Find("PullCollider").GetComponent<PlayerPull>();
            
            if (pk.emptyState)
            {
                Destroy(other.gameObject);
                pk.emptyState = false;
            }
           
        }
    }
}
