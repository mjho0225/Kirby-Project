using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbTrigger : MonoBehaviour
{
    GameObject absorbItem;
    float currTime;
    [SerializeField] public bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        absorbItem = GetComponentInParent<PlayerAbsorb>().absorbItem;
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

            print("absorbItemabsorbItem" + other.gameObject + absorbItem);
            string absorbItemTag = GetComponentInParent<PlayerAbsorb>().absorbItemTag;
            float distance = Vector3.Distance(absorbItem.transform.position, transform.position);
            //���� �ִϸ��̼� �ð� üũ �ʿ�
            if (distance < 0.5f)
            {
                print("destroy" + absorbItemTag);
                Destroy(other.gameObject);
                isDestroy = true;

                GetComponentInParent<PlayerAbsorb>().state = PlayerAbsorb.AbsorbState.Absorbed;
                currTime += Time.deltaTime;
                if (absorbItemTag == "e_ranger")
                {
                    GetComponentInParent<PlayerController>().attackState = PlayerController.AttackState.RANGER;
                    currTime = 0;    
                }
            }
        }
    }
}
