using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description g�n�rale
 * Script qui permet de tirer des boules de lumi�re sur le chasseur
 * pour le blesser
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 16 d�cembre 2021
 */
public class ArmerLumiere : MonoBehaviour
{
    public GameObject lumiereBlessante;     // La "balle" invisible
    public Transform boutLampe;
    public float vitesseLumiere;
    public float cadenceTir;
    private float dernierTir;               // Le temps avant de pouvoir tirer � nouveau

    /// <summary>
    /// V�rifie si le joueur peut tirer
    /// </summary>
    /// <returns> Retourne true ou false </returns>
    public bool PeutTirer()
    {
        // Si le temps de tir � d�pass� la cadence de tir
        if (Time.time - dernierTir >= cadenceTir)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Appel�e lorsque le joueur veut tirer une balle de lumi�re
    /// Envoit la 
    /// </summary>
    public void TirerLumiere()
    {
        // Tirer une balle qui va tout droit � partir de la lampe de poche du joueur
        dernierTir = Time.time;
        GameObject obj = Instantiate(lumiereBlessante);
        obj.transform.position = boutLampe.position;
        obj.transform.rotation = boutLampe.rotation;
        obj.GetComponent<Rigidbody>().velocity = boutLampe.forward * vitesseLumiere;
    }
}
