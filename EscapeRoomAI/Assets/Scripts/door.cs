using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class door : MonoBehaviour
{
    public float interationDistance;
    public GameObject intText;
    public string doorOpenAnimName, doorCloseAnimName;

    void Update()
    {
       Ray ray =  new Ray(transform.position, transform.forward);
       RaycastHit hit;

       if(Physics.Raycast(ray, out  hit, interationDistance))
        {
            if (hit.collider.gameObject.tag == "door")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                intText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");
                    }
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                }
            }
            else
            {
                intText.SetActive(false);
            }
        }
    }

}
