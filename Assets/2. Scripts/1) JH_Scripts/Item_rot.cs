using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_rot : MonoBehaviour
{
    public GameObject f_Paticle;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(f_Paticle, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        // ��ƼŬ �߰�
        
        
        //��� ȸ��
        transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));
    }
}