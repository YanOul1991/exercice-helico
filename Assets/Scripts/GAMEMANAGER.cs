using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Scripte de l'objet GAMEMANAGER qui gere s'occupe d'appeler les fonction du sripte GestionCompteur se trouvant dans le meme objet parent:
    
    Par : Yanis Oulmane
    Derniere Modification 23-09-2024
 */
public class GAMEMANAGER : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // Appel de la fonction qui active le compteur
        GetComponent<GestionCompteur>().ActivationCompteur();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
