using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* 
    Script de gestion des deplacments de l'helicoptere de deplacment, montee et descente en altitude et rotation sur lui-meme.
    Par : Yanis Oulmane
    Derniere Modification 04-09-2024
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // Update() pour detecter les touches, pour appliquer les forces c'est dans le FixedUpdate()
    void Update()
    {
        // Le multiplicatuer de force dependra de la vitesse des helices
        multiplicateurForce = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y;

        // DEPLACEMENT DE MONTEE ET DESCENTE
        // Input.GetAxis("Horizontal");
        float valeurAxeV = Input.GetAxis("Vertical"); // Retourne valeur entre -1 et 1
        forceDeplacement = valeurAxeV * multiplicateurForce;

        // DEPLACEMENT DE ROTATION  
        // Input.GetAxis("Horizontal");
        float valeurAxeH = Input.GetAxis("Horizontal"); // Retourne valeur entre -1 et 1
        forceTorsion = valeurAxeH * multiplicateurForce;

        // DEPLACEMENT AVANT
        // Lorsque la touche q est appuye la vitesse augmente a chaque fois
        // Jusqu'a atteindre la vitesse de deplacement avant max
        if (Input.GetKeyDown(KeyCode.Q))
        {
            vitesseAvant = Mathf.Clamp(vitesseAvant += vitesseAvantMax/4, 0, vitesseAvantMax);
        }
        // Lorsque a chaque fois que la touche E est appuye la vitesse diminue jusqua un minimum de 0
        if (Input.GetKeyDown(KeyCode.E))
        {
            vitesseAvant = Mathf.Clamp(vitesseAvant -= vitesseAvantMax/4, 0, vitesseAvantMax);
        }
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
