using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Script de gestion de la camera de surveillance
    Par : Yanis Oulmane
    Derniere Modification 09-09-2024
 */
public class CameraSurveillance : MonoBehaviour
{
    public Transform laCibleASuivre; // Variable Transform que la camera regardera

    // Update is called once per frame
    void FixedUpdate()
    {
        // Transforme les rotation de la camera pour qu'elle s'aligne a la cible
        transform.LookAt(laCibleASuivre);
    }
}
