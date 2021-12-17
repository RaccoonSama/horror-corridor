using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description générale
 * Script qui fait apparaître le chasseur dans la scène finale
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 16 décembre 2021
 */
public class ApparitionFinale : MonoBehaviour
{
    public GameObject Chasseur;

    void Start()
    {
        Invoke("ApparitionChasseur", 15);
    }

    /// <summary>
    /// Fait apparaître le chasseur
    /// </summary>
    void ApparitionChasseur()
    {
        Chasseur.SetActive(true);
    }
}
