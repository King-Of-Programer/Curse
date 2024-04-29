using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    [SerializeField]
    private GameObject destination;


    void Start()
    {

    }

    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.transform.position);
        CharacterController controller = other.gameObject.GetComponent<CharacterController>();

        if (controller != null)
        {
            //controller.enabled = false;
            //other.gameObject.transform.position =
            //destination.transform.position;
            //controller.enabled = true;

            controller.Move(destination.transform.position - other.transform.position);
        }

        other.gameObject.transform.position = destination.transform.position;

        //Physics.SyncTransforms();
        
        //Debug.Log(other.gameObject.transform.position);
    }
}
