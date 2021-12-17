using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description générale
 * Script qui crée des boules de lumières invisibles pour attaquer les chasseurs
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 15 décembre 2021
 */
public class Lumiere : MonoBehaviour
{
    public int degats;          // Le nombre de dégat par tir
    public float tempsVie;      // Le temps de vie de chaque balle
    private float tempsTir;     // Le temps entre chaque tir
    public GameObject impactParticules; // Des particules lorsque les balles touches

    void OnEnable()
    {
        tempsTir = Time.time;
    }

    void Update()
    {
        // Si le temps de vie est passé, détruit la balle
        if (Time.time - tempsTir >= tempsVie)
        {
            Destroy(gameObject);
        }     
    }
    void OnTriggerEnter(Collider infoCollision)
    {
        /* Si la balle de lumière touche un chasseur : il prend du dégât, 
        / un impact de particule apparaît pour .5s et la balle est détruite */
        if (infoCollision.CompareTag("Chasseur"))
        {
            infoCollision.GetComponent<EnnemiChasseur>().PrendreDegats(degats);
            GameObject obj = Instantiate(impactParticules, transform.position, Quaternion.identity);
            Destroy(obj, 0.5f);
        }
        Destroy(gameObject);
    }
}
