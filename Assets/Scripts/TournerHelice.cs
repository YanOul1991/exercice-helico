using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
	Script de gestion des helices ayant les fonctionalites suivantes : 

		- Controle de la rotation avec la methode transform.Rotate();
		- Alternance marche / arret du moteur avec la touche 'Return';
		- Effets d'accelerations au demarage et arret des helices;

	Par : Yanis Oulmane
	Derniere modification : 25-09-2024
 */
public class TournerHelice : MonoBehaviour
{
	/* =============================================================================================== */
	/* ================================== DECLARATION DES VARIABLES ================================== */	
	/* =============================================================================================== */
	public Vector3 vitesseHelice; // Variable Public Vector3 qui memorisera les vitesses des helices
	public bool moteurEnMarche; // Variable Priavte bool qui memorise l'etat des helice
	public float vitesseRotationMax; // Variable public float pour la vitesse de rotation max des helices
	public float acceleration; // Variable private float pour memoriser les accelerations des helices
	public GameObject refHelico; // Reference a l'objet helico pour acceder a sa variable finJeu de son scripte

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Si on appuit sur la touche Retour, changement de l'etat du moteur et que le jeu n'est pas termine
		if (Input.GetKeyDown(KeyCode.Return) && !refHelico.GetComponent<DeplacementHelico>().finJeu) 
		{
			moteurEnMarche = !moteurEnMarche;
		}

		if(refHelico.GetComponent<DeplacementHelico>().finJeu) 
		{
			// Le moteur s'arrete
			moteurEnMarche = false;
			// Grande acceleration pour que les helices sarrentent rapidement
			acceleration = 100;
		}

		// Acceleration progressive des helices si le moteur est en marche jusqu'a vitesse max
		if (moteurEnMarche)
		{
			// Acceleration lineraire des helices
			// Augmenetation de la vitesse a chaque seconde jusqu'a atteindre la vitesse max
			vitesseHelice.y = Mathf.Clamp(vitesseHelice.y += acceleration, 0f, vitesseRotationMax);
		}
		else
		{
			// Helice ralenti jusqua a un minium de 0
			vitesseHelice.y = Mathf.Clamp(vitesseHelice.y -= acceleration, 0f, vitesseRotationMax);
		}

		// Le moteur est desactivite si la variable niveauEssenceCourent du scripte de deplacement de l'helico <= 0
		if (refHelico.GetComponent<DeplacementHelico>().niveauEssenceCourent <= 0)
		{
			moteurEnMarche = false;
		}
	}
	
	void FixedUpdate()
	{
		// Application des vitesses de roations aux helices
		transform.Rotate(vitesseHelice * Time.deltaTime);
	}
}

