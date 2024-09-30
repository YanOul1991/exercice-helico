using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Scripte pour le dome avec les fonctionalites suivantes : 
        - Ouverture/Fermeture du dome avec les touches 'O' et 'F', en accedant a son Animator component;
        - Fonction qui joue le son d'ouverture qui sera appele avec un evenement personalise dans l'animator;

        Par : Yanis Oulmane
        Derniere modification : 30-09-2024
 */
public class GestionDome : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Ouverture/Fermeture du dome avec les touches 'O' et 'F'
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetComponent<Animator>().SetBool("ouvert", true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Animator>().SetBool("ouvert", false);
        }
    }

    // Fonction qui active le AudioSource du dome pour faire jouer le son de son ouverture
    // Fonction qui sera appele avec un custom event de son animator
    void JouerSonOuverture(AudioClip sonOuverture)
    {
        // Joue le son d'ouverture du dome
        GetComponent<AudioSource>().PlayOneShot(sonOuverture);
    }
}
