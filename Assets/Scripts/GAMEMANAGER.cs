using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
    Scripte de l'objet GAMEMANAGER qui gere les fonctionalites suivantes :

        - Fonction public qui appel la coroutine qui gere l'intro;
        - Fonction coroutine de gestion scene d'intro;
        - Fonction coroutine de gestion de scene de victoire;
            
    Par : Yanis Oulmane
    Derniere Modification 02-10-2024
 */
public class GAMEMANAGER : MonoBehaviour
{
    /* ====================================== VARIABLES  ======================================*/
    public GameObject objetJeu; // Reference a l'objet qui contient les element principaux du jeu;
    public GameObject cameraIntro; // Reference a l'objet de camera d'intro;
    public AnimationClip animCameraIntro; // Reference a l'animation de la camera d'intro;
    public GameObject[] lesUI; // Liste des UI qui changerons d'etat


    // Fonction public qui demmare la coroutine d'intro du jeu qui sera appele lorsquon clique sur le btn commencer
    public void CommencerJeu()
    {
        // Demmare la coroutine de scene d'intro
        StartCoroutine(AnimIntro());
    }

    // Fonction couroutine de gestion de scene d'intro
    public IEnumerator AnimIntro()
    {
        // desactive le UI d'intro
        lesUI[0].SetActive(false);
        // Active le UI du jeu
        lesUI[1].SetActive(true);
        // Active l'animation de la camera de depart 
        cameraIntro.GetComponent<Animator>().enabled = true;
        // Attendre la dure de l'animation de la camera intro + 1 sec
        yield return new WaitForSeconds(animCameraIntro.length + 1);
        // Desactivation de la camera d'intro
        cameraIntro.SetActive(false);
        // active les objets du jeu
        objetJeu.SetActive(true);
        // Appel de la fonction qui demmare le chorno
        GetComponent<GestionCompteur>().ActivationCompteur();
    }

    // Fonction coroutine de gestion de scene de victoire
    public IEnumerator SceneVictoire()
    {
        // Arrete la coroutine de gestion du temps (timer)
        StopCoroutine(GetComponent<GestionCompteur>().coroutineGestionTemps);
        // Desactive le UI du jeu
        lesUI[1].SetActive(false);
        // Active le UI de victoire
        lesUI[2].SetActive(true);
        // Attendre 3 secondes
        yield return new WaitForSeconds(3);
        // Redemarrer le jeu
        SceneManager.LoadScene(0);
    }
}
