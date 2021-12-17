using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Description g�n�rale
 * Script qui fait appara�tre le chasseur dans la sc�ne finale
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 16 d�cembre 2021
 */
public class ApparitionFinale : MonoBehaviour
{
    public GameObject Chasseur;

    void Start()
    {
        Invoke("ApparitionChasseur", 15);
    }

    /// <summary>
    /// Fait appara�tre le chasseur
    /// </summary>
    void ApparitionChasseur()
    {
        Chasseur.SetActive(true);
    }
}
