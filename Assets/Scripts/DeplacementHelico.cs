using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* 
    Script de gestion des deplacments de l'helicoptere de deplacment, montee et descente en altitude et rotation sur lui-meme.
    Gestion des sons de helices de l'helico et du son globale
    Par : Yanis Oulmane
    Derniere Modification 11-09-2024
 */
public class DeplacementHelico : MonoBehaviour
{
    /// DECLARATION DES VARIABLES    
    float multiplicateurForce; // variable qui memorize un multiplicateur de force qui sera applique aux forces qui font deplacer l'helico
    float forceDeplacement; // Variable qui memorise les forces de deplacement sur les axes
    float forceTorsion; // Variable qui memorise la force de torsion/rotation sur l'axe Y
    public float vitesseAvant; // Variable qui memorize la vitesse de deplacment vers l'avant de l'helico
    public float vitesseAvantMax; // Variable qui memorize la vitesse de deplacement avant maximal de l'helico
    public GameObject heliceRef; // Variables Public GameObject qui sera reference aux helices de l'helico pour acceder a leur propriete de vitesse

    // Update is called once per frame
    // Update() pour detecter les touches, pour appliquer les forces c'est dans le FixedUpdate()
    void Update()
    {

        /* ==========================  FORCES DES DEPLACEMENTS ========================== */
        // Le multiplicatuer de force dependra de la vitesse des helices
        multiplicateurForce = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y * 2;

        // DEPLACEMENT DE MONTEE ET DESCENTE
        // Input.GetAxis("Horizontal");
        float valeurAxeV = Input.GetAxis("Vertical"); // Retourne valeur entre -1 et 1
        forceDeplacement = valeurAxeV * multiplicateurForce;

        // DEPLACEMENT DE ROTATION  
        // Input.GetAxis("Horizontal");
        float valeurAxeH = Input.GetAxis("Horizontal"); // Retourne valeur entre -1 et 1
        forceTorsion = valeurAxeH * multiplicateurForce;

        // DEPLACEMENT AVANT
        // Ajustement de la vitesse de deplacement avant de helico 
        // La touche Q accelere le deplacement avant avec 4 niveaux de vitesse jusqua atteindre la vitesse max
        // La touche E ralenti le deplacement avant avec 4 niveaux de vitesse jusqua atteindre la vitesse de 0

        // Vitesse peut augmenter seulement si le moteur est en marche
        if (Input.GetKeyDown(KeyCode.Q) && heliceRef.GetComponent<TournerHelice>().moteurEnMarche)
        {
            // Augmentation de la vitesse avant
            vitesseAvant = Mathf.Clamp(vitesseAvant += vitesseAvantMax / 4, 0, vitesseAvantMax);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Diminution de la vitesse avant 
            vitesseAvant = Mathf.Clamp(vitesseAvant -= vitesseAvantMax / 4, 0, vitesseAvantMax);
        }

        /* ===================================== SONS ===================================== */

        // SONS DES HELICES
        // Lorsque on demmare le moteur, et qu'il n'est pas deja en train de jouer demmarage du son des helices
        if (heliceRef.GetComponent<TournerHelice>().moteurEnMarche && !GetComponent<AudioSource>().isPlaying)
        {
            // Fait jouer le son des helices
            GetComponent<AudioSource>().Play();
        }

        // Vitesse reel / vitesse max  = une valeur entre 0 et 1 ce qui sera la valeur du volume de l'AudioSource
        GetComponent<AudioSource>().volume = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y / heliceRef.GetComponent<TournerHelice>().vitesseRotationMax;
        // La vitesse de pitch = 0.5 * la moitier du volume (entre 0 et 0.5). Pitch varie donc entre 0.5 en 1
        GetComponent<AudioSource>().pitch = 0.5f + (heliceRef.GetComponent<TournerHelice>().vitesseHelice.y / heliceRef.GetComponent<TournerHelice>().vitesseRotationMax / 2);

    }

    // Fonction stable a 50FPS, reservee aux objets physiques
    void FixedUpdate()
    {
        // Memorise la vitesse en temps reel de la reference a l'helice en accedant a la proprite de son script
        float vitesseHelice = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y;

        // Si les helice tournent(vitesse > 0)
        if (vitesseHelice > 0)
        {
            // Desactivation de la gravite
            GetComponent<Rigidbody>().useGravity = false;
        }
        else // Si les helice ne tournent plus 
        {
            // Reactivation de la gravite
            GetComponent<Rigidbody>().useGravity = true;
        }

        // Applique la force au Rigidbody
        // On utilise seulment une fois AddRelativeForce() / AddRelativeTorque();
        GetComponent<Rigidbody>().AddRelativeForce(0f, forceDeplacement, vitesseAvant);
        GetComponent<Rigidbody>().AddRelativeTorque(0, forceTorsion, 0);

        // Arrange les rotations de l'helico pour evite qu'il penche sur les axes X et Z
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0);
    }
}
