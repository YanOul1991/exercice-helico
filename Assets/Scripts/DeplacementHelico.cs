using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* 
    Script de gestion des deplacments de l'helicoptere de deplacment, montee et descente en altitude et rotation sur lui-meme.
    Par : Yanis Oulmane
    Derniere Modification 31-08-2024
 */
public class DeplacementHelico : MonoBehaviour
{
    /// DECLARATION DES VARIABLES    
    public float vitesseAvant; // Variable public float pour memoriser la vitesse avant en temps reel de l'helico
    public float vitesseAvantMax; // Variable public float pour memoriser la vitesse maximal de deplacement avant de l'helico
    public float vitesseTourne; // Variable public float pour memoriser la vitesse de rotation en temps reel de l'helico
    public float vitesseTourneMax; // Variable public float pour memoriser la vitesse de rotation max de l'helico

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
