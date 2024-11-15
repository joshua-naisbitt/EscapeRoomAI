using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Interactable winston;
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    //[SerializeField] private GameObject winstonObj;
    // Start is called before the first frame update
    void Start()
    {
        winston = GameObject.FindGameObjectWithTag("Winston").GetComponent<Interactable>();
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            winston.BaseInteract();
        }
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction*distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            
            if(hitInfo.collider.GetComponent<Interactable>() != null){
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                if(inputManager.onFoot.Interact.triggered){
                    interactable.BaseInteract();
                }
            }
        }
    }
}
