using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbTrigger : MonoBehaviour
{
    GameObject absorbItem;
    // Start is called before the first frame update
    void Start()
    {
        absorbItem = GetComponentInParent<PlayerAbsorb>().absorbItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //������ �з� �� ȹ��
            if (other.gameObject.tag == "item")
            {

            }
        }

        //print("other check" + other.gameObject);
        //print("layer check" + layer);

        if (other.gameObject == absorbItem)
        {


            //���� ������Ʈ ���� �� �Ҹ�
            Destroy(other.gameObject);

            GetComponentInParent<PlayerAbsorb>().IsAttackItem(other.gameObject.tag);

        }
    }
}
