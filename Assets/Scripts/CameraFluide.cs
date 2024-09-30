using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Script de gestion de la camera qui suit l'helico de facon plus fluide
    Par : Yanis Oulmane
    Derniere Modification 09-09-2024
 */
public class CameraFluide : MonoBehaviour
{
    public GameObject laCible; // public GameObject qui sera la cible que la camera va suivre

    public Vector3 laDistance; // public Vector3 qui memorisera la distance de la camera a l'helico

    // Deplacement de la camera selon le framerate
    void FixedUpdate()
    {
        // Positionnement finale de la camera selon la position relaive de la cible et en additionnant la distance par rapport a celle-ci
        Vector3 posFinale = laCible.transform.TransformPoint(laDistance);   

        // Deplacement de la cemra de sa position vers sa destination de facon fluide
        transform.position = Vector3.Lerp(transform.position, posFinale, 0.15f);

        // Camera regarde la cible
        transform.LookAt(laCible.transform);
    }
}
