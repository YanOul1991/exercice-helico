using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/* 
    Script de gestion de la camera qui est a un distance constante de l'helico
    Par : Yanis Oulmane
    Derniere Modification 09-09-2024
 */
public class CameraDistConst : MonoBehaviour
{
    public GameObject laCible; // public GameObject qui sera la cible que suivra la camera
    public UnityEngine.Vector3 laDistance; // public Vector3 pour appliquer une distance de la camera par rapport a la cible

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Positionne la camer a la position de la cible + la distance voulue
        transform.position = laCible.transform.position + laDistance;

        // Camera regarde la cible
        transform.LookAt(laCible.transform);
    }
}
