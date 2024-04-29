using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;

    private Vector3 cameraAngls;
    private Vector3 cameraOffset;
    private Vector3 initialAngles;
    private Vector3 initialOffset;

    void Start()
    {
        initialAngles = cameraAngls = this.transform.eulerAngles;
        //cameraOffset = this.transform.position - cameraAnchor.transform.position;
        initialOffset = cameraOffset = this.transform.position - cameraAnchor.transform.position;

    }

    void Update()
    {
        cameraAngls.y += Input.GetAxis("Mouse X");
        cameraAngls.x -= Input.GetAxis("Mouse Y");
        if (Input.GetKeyUp(KeyCode.V))
        {
            cameraOffset = (cameraOffset == Vector3.zero) ? initialOffset : Vector3.zero;
        }

    }
    void LateUpdate()
    {
        this.transform.position = cameraAnchor.transform.position + Quaternion.Euler(0, cameraAngls.y - initialAngles.y, 0) * cameraOffset;
        this.transform.eulerAngles = cameraAngls;
    }
}
