using UnityEngine;

/* 
    Script de gestion de la camera qui est a un distance constante de l'helico;

    Par : Yanis Oulmane

    Derniere Modification 09-09-2024
 */
public class CameraDistConst : MonoBehaviour
{
    public GameObject laCible; // public GameObject qui sera la cible que suivra la camera
    public Vector3 laDistance; // public Vector3 pour appliquer une distance de la camera par rapport a la cible

    // Deplacement de la camera selon le framerate
    void FixedUpdate()
    {
        // Positionne la camer a la position de la cible + la distance voulue
        transform.position = laCible.transform.position + laDistance;

        // Camera regarde la cible
        transform.LookAt(laCible.transform);
    }
}
