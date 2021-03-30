using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicCam : MonoBehaviour
{

    public GameObject vCam2;

    //Sistema de Camera dinamica
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)  // vai mudar quando colidir com o CamTrigger que é o objeto criado 
        {
            case "CamTrigger":
                vCam2.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                vCam2.SetActive(false);
                break;
        }
    }
    // Fim sistema de Camera Dinamida
}
