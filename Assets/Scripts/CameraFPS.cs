using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Script de gestion de la camera en vue fps, fesant trembler legerement 
    le point de vue lorsque l'utilisateur regarde a travers celle-ci, si le moteur est en marche
    Par : Yanis Oulmane
    Derniere Modification 11-09-2024
 */
public class CameraFPS : MonoBehaviour
{
    public float trembleMax; // Variable float pour enregistrer la force de tremblement max
    public Vector3 posInitiale; // Veriable vector 3 qui enregistre la position local de la camera
    public GameObject refHelice; // Variable GameObject afin de voir si l'helice est en marche dans son scripte

    // Start is called before the first frame update
    void Start()
    {
        // Enregistre la position locale initale de la camera
        posInitiale = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Variable locale pour la position vers laquel la camera bougera
        // D'abord elle recoit comme valeur la position intiale par defaut de la camera
        Vector3 posFinale = posInitiale;

        // Si le moteur tourne
        if (refHelice.GetComponent<TournerHelice>().moteurEnMarche)
        {
            // Additionne les vecteurs x et y de la position selon une valeur aleatoire se situant 
            // Entre - et + la variation de tremblement maximale
            posFinale.x += Random.Range(-trembleMax, trembleMax);
            posFinale.y += Random.Range(-trembleMax, trembleMax);
        }

        // Applique le nouveau vecteur a la position locale de la camera
        transform.localPosition = posFinale;

    }
}
