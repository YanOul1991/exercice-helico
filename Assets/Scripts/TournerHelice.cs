using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournerHelice : MonoBehaviour
{

    // [SerializeField] Vector3 vitesseHelice;

    // Variable vitesse de l'helice
    public Vector3 vitesseHelice;

    // public float compteur;
    public bool moteurEnMarche;

    // Variable public float vitesse max de l'helice
    public float vitesseRotationMax;

    public float acceleration;

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
            moteurEnMarche = !moteurEnMarche;

            acceleration = 0;
        }

        if (moteurEnMarche)
        {
            if (vitesseHelice.y < vitesseRotationMax)
            {
                acceleration += 1 * Time.deltaTime;
                vitesseHelice.y += acceleration;
            }
            else
            {
                acceleration = 0;
                vitesseHelice.y = vitesseRotationMax;
            }

            // vitesseHelice.y < vitesseRotationMax ? vitesseHelice.y += acceleration : vitesseHelice.y = vitesseRotationMax;
        }

        transform.Rotate(vitesseHelice * Time.deltaTime);

    }
}
