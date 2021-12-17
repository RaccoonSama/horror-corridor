using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description g�n�rale
 * Script qui cr�e des boules de lumi�res invisibles pour attaquer les chasseurs
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 15 d�cembre 2021
 */
public class Lumiere : MonoBehaviour
{
    public int degats;          // Le nombre de d�gat par tir
    public float tempsVie;      // Le temps de vie de chaque balle
    private float tempsTir;     // Le temps entre chaque tir
    public GameObject impactParticules; // Des particules lorsque les balles touches

    void OnEnable()
    {
        tempsTir = Time.time;
    }

    void Update()
    {
        // Si le temps de vie est pass�, d�truit la balle
        if (Time.time - tempsTir >= tempsVie)
        {
            Destroy(gameObject);
        }     
    }
    void OnTriggerEnter(Collider infoCollision)
    {
        /* Si la balle de lumi�re touche un chasseur : il prend du d�g�t, 
        / un impact de particule appara�t pour .5s et la balle est d�truite */
        if (infoCollision.CompareTag("Chasseur"))
        {
            infoCollision.GetComponent<EnnemiChasseur>().PrendreDegats(degats);
            GameObject obj = Instantiate(impactParticules, transform.position, Quaternion.identity);
            Destroy(obj, 0.5f);
        }
        Destroy(gameObject);
    }
}
