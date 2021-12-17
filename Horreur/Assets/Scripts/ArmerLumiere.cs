using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description générale
 * Script qui permet de tirer des boules de lumière sur le chasseur
 * pour le blesser
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 16 décembre 2021
 */
public class ArmerLumiere : MonoBehaviour
{
    public GameObject lumiereBlessante;     // La "balle" invisible
    public Transform boutLampe;
    public float vitesseLumiere;
    public float cadenceTir;
    private float dernierTir;               // Le temps avant de pouvoir tirer à nouveau

    /// <summary>
    /// Vérifie si le joueur peut tirer
    /// </summary>
    /// <returns> Retourne true ou false </returns>
    public bool PeutTirer()
    {
        // Si le temps de tir à dépassé la cadence de tir
        if (Time.time - dernierTir >= cadenceTir)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Appelée lorsque le joueur veut tirer une balle de lumière
    /// Envoit la 
    /// </summary>
    public void TirerLumiere()
    {
        // Tirer une balle qui va tout droit à partir de la lampe de poche du joueur
        dernierTir = Time.time;
        GameObject obj = Instantiate(lumiereBlessante);
        obj.transform.position = boutLampe.position;
        obj.transform.rotation = boutLampe.rotation;
        obj.GetComponent<Rigidbody>().velocity = boutLampe.forward * vitesseLumiere;
    }
}
