using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
	Controle des rotation des deux helices de l'helicoptere avec la methode transform.Rotate().
	Alternace marche/arret avec la touche retour du clavier et application d'un effet d'acceleration lors du demmarage des helices.
	Par : Yanis Oulmane
	Derniere modification : 31-08-2024
 */
public class TournerHelice : MonoBehaviour
{	
	// DECLARATION DES VARIABLES	
	public Vector3 vitesseHelice; // Variable Public Vector3 qui memorisera les vitesses des helices
	private bool moteurEnMarche; // Variable Priavte bool qui memorise l'etat des helice
	public float vitesseRotationMax; // Variable public float pour la vitesse de rotation max des helices
	public float acceleration; // Variable public float pour memoriser les accelerations des helices

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Detection touche entree
		if (Input.GetKeyDown(KeyCode.Return))
		{
			// Change l'etat du moteur
			moteurEnMarche = !moteurEnMarche;
			// Variable acceleration retourne a 0 a chaque switch
			acceleration = 0;
		}
		
		// Acceleration progressive des helices si le moteur est en marche jusqu'a vitesse max
		if (moteurEnMarche)
		{
			// Acceleration lineraire des helices
			// Augmenetation de la vitesse a chaque seconde jusqu'a atteindre la vitesse max
			if (vitesseHelice.y < vitesseRotationMax)
			{
				acceleration += 0.5f * Time.deltaTime;
				vitesseHelice.y += acceleration;
			}
			else
			{
				// Variable acceleration retourne a 0 une fois la vitesse max atteinte
				acceleration = 0;
				vitesseHelice.y = vitesseRotationMax;
			}
		}

		// Application des vitesses de roations aux helices
		transform.Rotate(vitesseHelice * Time.deltaTime);

	}
}
