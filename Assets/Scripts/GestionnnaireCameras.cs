using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

/* 
    Script de gestion des camera. Activation et desactivation des camera appropries selon les inputes de l'utilisateur
    Par : Yanis Oulmane
    Derniere Modification 09-09-2024
 */
public class GestionnnaireCameras : MonoBehaviour
{

    public GameObject[] lesCameras; // Array de GameObjects qui contientra tous les objets cameras

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int clee =  Int32.Parse(UnityEngine.Input.inputString);

        if (clee >= 1 && clee <= lesCameras.Length)
        {
            Camera.main.gameObject.SetActive(false);

            lesCameras[clee - 1].SetActive(true);
        }


        // switch (clee)
        // {
        //     case(1):
        //         Camera.main.gameObject.SetActive(false);
        //         lesCameras[clee - 1].SetActive(true);
        //         break;
        //     case(2):
        //         Camera.main.gameObject.SetActive(false);
        //         lesCameras[clee - 1].SetActive(true);
        //         break;
        //     case(3):
        //         Camera.main.gameObject.SetActive(false);
        //         lesCameras[clee - 1].SetActive(true);
        //         break;
        //     case(4):
        //         Camera.main.gameObject.SetActive(false);
        //         lesCameras[clee - 1].SetActive(true);
        //         break;
        //     default:
        //         break;
        // }
    }
}
