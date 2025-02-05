using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/* 
    Script de gestion des camera aui aura les fonctionalites suivantes : 

        - Activation/desactivation des camera selon l'input de facon dynamique;
        - Mute/unmute le son globale du jeu en switchant l'etat actif des audio listeners des cameras;

    Par : Yanis Oulmane

    Derniere Modification 16-09-2024
 */
public class GestionnnaireCameras : MonoBehaviour
{
    public GameObject[] lesCameras; // Array de GameObjects qui contientra tous les objets cameras
    public string[] numCamera; // Array de string des numeros des cameras
    public bool sonGlobale = true; // Vairbla pour activer ou mute le son global, est a true par defaut
    public GameObject refHelico; // Reference a l'objet helico pour acceder a sa variable finJeu de son scripte

    // Start is called before the first frame update
    void Start()
    {
        // Assigne la longueur du array numCamera selon le nombre de camera dans l'array lesCameras
        numCamera = new string[lesCameras.Length];

        // Chaque numero de camera sera au meme index dans le array numCamera que celle qu'elle a dans l'array lesCameras
        for (int i = 0; i < numCamera.Length; i++)
        {
            // Numero de camera sera son index plus 1, convertir en string
            numCamera[i] = (i + 1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* ====================================== CAMERAS ACTIVES ====================================== */

        // Regarde si la touche M est appuye
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Change l'etat du bool sonGlobale
            sonGlobale = !sonGlobale;
        }

        // Regarde si la touche fait parties des numero de camera (1, 2, 3 ,4) et que la partie n'est pas termine
        if (numCamera.Contains(Input.inputString) && !refHelico.GetComponent<DeplacementHelico>().finJeu)
        {
            // Desactivation de la camera active
            Camera.main.gameObject.SetActive(false);

            // Activation de la camera selon le numero de camera choisit
            lesCameras[Array.IndexOf(numCamera, Input.inputString)].SetActive(true);
        }

        // Si partie est termine
        // Applique l'etat du AudioListener de la camera active selon l'etat du bool sonGlobale
        Camera.main.GetComponent<AudioListener>().enabled = sonGlobale;
    }
}
