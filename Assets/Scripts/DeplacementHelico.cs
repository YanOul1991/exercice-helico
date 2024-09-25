using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* 
    Script de l'helicoptere qui gere les fonctionnalites suivates :
    
        - Deplacments de l'helicoptere vers l'avant avec le touches 'Q' et 'E';
        - Montee et descente en altitude et rotation sur lui-meme avec Input.GetAxis();
        - Gestion des sons de helices de l'helico et du son globale;
        - Gestion collisions de l'helico et de l'action qui suit selon l'objet qui a ete touche;
        - Gestion de l'essence et des actions qui sont liees a la variation de celui-ci;

    Par : Yanis Oulmane
    Derniere Modification 24-09-2024
 */
public class DeplacementHelico : MonoBehaviour
{
    /* ================================================================================================= */
    /* =================================== DECLARATION DES VARIABLES =================================== */
    /* ================================================================================================= */

    /* **************************** Variables pour gestion des deplacements **************************** */
    float multiplicateurForce; // variable qui memorize un multiplicateur de force qui sera applique aux forces qui font deplacer l'helico
    float forceDeplacement; // Variable qui memorise les forces de deplacement sur les axes
    float forceRotation; // Variable qui memorise la force de torsion/rotation sur l'axe Y
    float vitesseAvant; // Variable qui memorize la vitesse de deplacment vers l'avant de l'helico
    public float vitesseAvantMax; // Variable qui memorize la vitesse de deplacement avant maximal de l'helico
    public GameObject heliceRef; // Variables Public GameObject qui sera reference aux helices de l'helico pour acceder a leur propriete de vitesse


    /* ***************************** Variables pour gestion de l'essence ***************************** */
    public float niveauEssenceMax; // niveau max de l'essence
    public float niveauEssenceCourent; // Niveau en temp reel de l'essence
    public Image imgNiveauEssence; // Variable qui memorise l'image qui represente le niveau d'essence


    /* ************************* Variable pour fonctionalites suplementaires ************************* */
    public bool finJeu = false; // Variable bool memorisant si l'helico est detruit, donc la fin du jeu
    public AudioClip sonCollecte; // Son qui joue lors de la collecte d'un bidon d'essence
    public GameObject animExplosion; // Effet particules d'explosion lors de la destruction de l'helico
    public GameObject cameraFinJeu; // Camera active lors de la fin de la partie

    /* ================================================================================================= */
    /* ================================================================================================= */

    // Fonction appele des le debut
    void Start()
    {
        // Fait le plein d'essence
        niveauEssenceCourent = niveauEssenceMax;
    }

    // Fonction update appele a chaque frame qui sera principalement pour les detections de touche et variations des variables de force pour les deplacements
    void Update()
    {
        /* ======================================================================================= */
        /* ================================ FORCES DE DEPLACEMENT ================================ */
        /* ======================================================================================= */

        // Le multiplicateur de force varirera selon la vitesse des helices
        multiplicateurForce = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y * 2;

        // Accepte Inputs si la partie n'est pas termine et qu'il reste de l'essence
        if (!finJeu && heliceRef.GetComponent<TournerHelice>().moteurEnMarche && niveauEssenceCourent > 0)
        {
            /* ==================== DEPLACEMENT SUR LES AXES ==================== */

            // La force des deplacements seront affectees par le multiplicateur de force
            // Deplacement vertical (monter / descendre)
            forceDeplacement = Input.GetAxis("Vertical") * multiplicateurForce;

            // Deplacement  horizontal (rotation sur place)
            forceRotation = Input.GetAxis("Horizontal") * multiplicateurForce;

            /* ======================= DEPLACEMENT AVANT ======================= */

            // La touche Q accelere le deplacement avant jusqua atteindre la vitesse max
            // La touche E ralenti le deplacement avant jusqua atteindre la vitesse de 0
            if (Input.GetKey(KeyCode.Q))
            {
                // Augmentation de la vitesse avant
                vitesseAvant = Mathf.Clamp(vitesseAvant += vitesseAvantMax / 50, 0, vitesseAvantMax);
            }
            if (Input.GetKey(KeyCode.E))
            {
                // Diminution de la vitesse avant 
                vitesseAvant = Mathf.Clamp(vitesseAvant -= vitesseAvantMax / 50, 0, vitesseAvantMax);
            }
        }

        /* ====================================================================================== */
        /* ======================================== SONS ======================================== */
        /* ====================================================================================== */

        /* ============== SON DES HELICES ============== */

        // Regarde si le moteur de l'helico est en marche et que le son des helices ne joue pas
        if (heliceRef.GetComponent<TournerHelice>().moteurEnMarche && !GetComponent<AudioSource>().isPlaying)
        {
            // Joue le son des helices
            GetComponent<AudioSource>().Play();
        }

        /* ======= VOLUME ET PITCH DES HELICES ======= */

        // Vitesse reel / vitesse max  = une valeur entre 0 et 1 ce qui sera la valeur du volume de l'AudioSource
        GetComponent<AudioSource>().volume = heliceRef.GetComponent<TournerHelice>().vitesseHelice.y / heliceRef.GetComponent<TournerHelice>().vitesseRotationMax;
        // La vitesse de pitch = 0.5 * la moitier du volume (entre 0 et 0.5). Pitch varie donc entre 0.5 en 1
        GetComponent<AudioSource>().pitch = 0.5f + (heliceRef.GetComponent<TournerHelice>().vitesseHelice.y / heliceRef.GetComponent<TournerHelice>().vitesseRotationMax / 2);

    }

    // Fonction stable a 50FPS, reservee aux objets physiques
    void FixedUpdate()
    {
        // Si les helice tournent(vitesse > 100)
        // *** Remarque : Helico utilisera automatiquement la gravite lors de sa destruction ***
        if (heliceRef.GetComponent<TournerHelice>().vitesseHelice.y > 100)
        {
            // Desactivation de la gravite
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            // Active la gravite dans le cas contraire
            GetComponent<Rigidbody>().useGravity = true;
        }

        // Applique la force au Rigidbody
        // On utilise seulment une fois AddRelativeForce() / AddRelativeTorque();
        GetComponent<Rigidbody>().AddRelativeForce(0f, forceDeplacement, vitesseAvant);
        GetComponent<Rigidbody>().AddRelativeTorque(0, forceRotation, 0);

        // Rearrange les rotations de l'helico si la partie est encore en cours pour eviter qu'il penche
        // Sinon il peut aller n'importe comment
        if (!finJeu)
        {
            // Arrange les rotations de l'helico pour evite qu'il penche sur les axes X et Z
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0);
        }

        // Appel a chaque 50fps la fonction de gestion de l'essence
        GestionEssence();
    }

    // Detection des collision
    private void OnCollisionEnter(Collision collision)
    {
        /* ========================== COLLISIONS AVEC LE TERRAIN ========================== */

        // Detection d'une collision avec un objet de tag "Terrain" pour inclure le terrain et les rochers
        if (collision.gameObject.tag == "Terrain")
        {
            // Boucle pour verifier chaque vector de la velocite (Vector3)
            for (int i = 0; i < 3; i++)
            {
                // Regarde si la valeur absolue est superieur a 7.5
                if (MathF.Abs(GetComponent<Rigidbody>().velocity[i]) > 7.5)
                {
                    // Demmarage de coroutine PartieTermine()
                    StartCoroutine(PartieTermine());

                    // Active l'explosion
                    animExplosion.SetActive(true);

                    // Arret de la boucle
                    break;
                }
            }
        }

    }

    // Detection des collisions de type trigger
    private void OnTriggerEnter(Collider collision)
    {
        /* =========================== TRIGGER AVEC LES BIDONS =========================== */

        // Lorsque l'helico detecte un trigger collision avec un objet qui a la le tag bidon
        if (collision.gameObject.tag == "bidon")
        {
            // Joue le son de collecte
            GetComponent<AudioSource>().PlayOneShot(sonCollecte);

            // Destruit cet objet
            Destroy(collision.gameObject);

            // Augmente le niveau d'essence mais de sorte a ce qu'il ne depasse jammais la quantite max
            niveauEssenceCourent = Mathf.Clamp(niveauEssenceCourent += 50, 0, niveauEssenceMax);
        }
    }

    // Fonction coroutine qui gere les actions lorsque l'helico sera detruit
    public IEnumerator PartieTermine()
    {
        print("PARTIE TERMINE RIP");
        // Partie est termine
        finJeu = true;

        // Change la couleur du mesh l'helico
        GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.2f, 0, 1);
        heliceRef.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.2f, 0, 1);

        // Modification des proprietes du rigide body de l'helico : frag, angularDrag et freezeRotation
        GetComponent<Rigidbody>().drag = GetComponent<Rigidbody>().drag / 10;
        GetComponent<Rigidbody>().angularDrag = GetComponent<Rigidbody>().angularDrag / 10;
        GetComponent<Rigidbody>().freezeRotation = false;

        // Vitesse avant de l'helico est de 0 (peux plus avancer)
        vitesseAvant = 0;

        // Desactivation de la camera actif et activation de la camera de fin de jeu
        Camera.main.gameObject.SetActive(false);
        cameraFinJeu.SetActive(true);

        // Attendre 8 secondes
        yield return new WaitForSeconds(8);
        // Relance la scene
        SceneManager.LoadScene(0);
    }

    // Fonction qui gere le niveau d'essence
    void GestionEssence()
    {
        niveauEssenceCourent -= 5 * Time.deltaTime;

        // Ajustement de la barre blache representant le niveau d'essence (proprete fill amount)
        imgNiveauEssence.fillAmount = niveauEssenceCourent / niveauEssenceMax;
    }

}
