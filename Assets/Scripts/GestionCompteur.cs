using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 
    Scripte de gestion du compteur ayant les fonctionalites suivantes :

       - Faire decrementer la valuer du compteur a chaque seconde a l'aide d'une fonction coroutine ayant un boucle infinit while(true){...}
       - Mettre a jour l'affichage du compteur a chaque fois que le compteur baisse

    Par : Yanis Oulmane
    Derniere Modification 25-09-2024
 */
public class GestionCompteur : MonoBehaviour
{
    /* =============================================================================================== */
	/* ================================== DECLARATION DES VARIABLES ================================== */	
	/* =============================================================================================== */
    public TextMeshProUGUI textCompteur; // public TextMeshProUGUI qui accede au component TextMeshPro de l'objet assigne, qui sera celui qui affiche le compteur
    public int leCompteur; // public int qui memorisera le temps de depart assigne et qui sera la valeur a afficher dans le texte du compteur
    private IEnumerator coroutineGestionTemps; // variable private IEnumerator pour assigner et mieux gerer la fonction coroutine
    public DeplacementHelico helico; // Reference au scripte DeplacementHelico

    // fonction public void qui gere l'activation du compteur
    public void ActivationCompteur()
    {
        // Assigne la fonction coroutine a la variable coroutineGestionTemps
        coroutineGestionTemps = Decompte();

        // Demarrage de la coroutine
        StartCoroutine(coroutineGestionTemps);

        // La premiere valeur du compteur sera le nombre choisit
        textCompteur.text = leCompteur.ToString();
    }

    /* Fonction coroutine Decompte(), qui va decrement la valeur du compteur et mettre a jour son affichage dans le text du UI a chaque seconde*/
    IEnumerator Decompte()
    {
        // Boucle infinie qui va se repeter apres le demmarage de la coroutine
        while(true)
        {
            // Retourne une attente de 1 seconde
            yield return new WaitForSeconds(1);

            // Decremente le compteur
            leCompteur--;

            // Mise a jour du texte affichant le compteur
            textCompteur.text = leCompteur.ToString();

            // Lorsque le compteur est a 0
            if (leCompteur == 0)
            {
                // Demmarage de la coroutine PartieTermine() du scripte de deplacement de l'helico
                StartCoroutine(helico.PartieTermine());
                // Arrete la coroutine Decompte()
                StopCoroutine(coroutineGestionTemps);
            }
        }
    }
}
