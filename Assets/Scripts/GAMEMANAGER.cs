using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/* 
    Scripte de l'objet GAMEMANAGER qui gere les fonctionalites suivantes :
        - Activation de la scene du jeu lorsque le bouton commer est clique;
        - Activation du compteur au commencement de la partie 
    Par : Yanis Oulmane
    Derniere Modification 30-09-2024
 */
public class GAMEMANAGER : MonoBehaviour
{
    /* ====================================== VARIABLES  ======================================*/

    public Button boutonCommencer;

    void Update()
    {
        
    }

    void CommencerJeu(){

        StartCoroutine(AnimIntro());
    }

    IEnumerator AnimIntro()
    {
        while (true)
        {
            GetComponent<GestionCompteur>().ActivationCompteur();
        }
    }
}
