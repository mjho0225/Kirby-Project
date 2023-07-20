using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastWaii : MonoBehaviour
{

    public static LastWaii instance;

    public bool lastWaii = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            lastWaii = true;
        }
    }
}
