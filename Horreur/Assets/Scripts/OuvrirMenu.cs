using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
 * Description g�n�rale
 * Script qui permet l'ouverture et la Fermeture du menu. Il contient �galement
 * tout ce qui est en lien avec les menus comme le volume et quitter le jeu
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 15 d�cembre 2021
 */
public class OuvrirMenu : MonoBehaviour
{
    public GameObject MenuPause;                                // Le gameObject du menu
    [SerializeField] InputActionReference triggerInputAction;   // Le input de la main droite
    public GameObject ligneVisuelle;                            // La ligne qui part de la main gauche
    public Slider sliderVolume;                                 // Le slider du volume

    private void Start()
    {
        /* R�cup�re la variable du volume de la musique 
        / pr�alablement d�finie ou la d�finie � 1*/
        if (!PlayerPrefs.HasKey("volumeMusique"))
        {
            PlayerPrefs.SetFloat("volumeMusique", 1);
            Save();
        }
        else {Load();}
    }
    private void OnEnable()
    {
        triggerInputAction.action.performed += XPressed;
    }

    private void XPressed(InputAction.CallbackContext obj)
    {
        // Active le menu si le bouton X est activ� (main gauche)
        if (!MenuPause.gameObject.activeInHierarchy)
        {
            MenuPause.SetActive(true);
            ligneVisuelle.SetActive(true);
        }
        // D�sactive le menu si il est d�j� activ�
        else
        {
            MenuPause.SetActive(false);
            ligneVisuelle.SetActive(false);
        }
        
    }

    private void OnDisable()
    {
        triggerInputAction.action.performed -= XPressed;
    }

    /// <summary>
    /// Fonction FermerMenu()
    /// Ferme le menu si le joueur clique sur le bouton de fermeture
    /// dans le menu de pause
    /// </summary>
    public void FermerMenu()
    {
        MenuPause.SetActive(false);
        ligneVisuelle.SetActive(false);
    }

    /// <summary>
    /// Fonction QuitterJeu()
    /// Ferme l'application lorsque la fonction est appel�e
    /// </summary>
    public void QuitterJeu()
    {
        Application.Quit();
    }

    /// <summary>
    /// Fonction ChangerVolume()
    /// Change le volume global en fonction de la valeur �crite sur le slider
    /// </summary>
    public void ChangerVolume()
    {
        AudioListener.volume = sliderVolume.value;
        Save();
    }

    /// <summary>
    /// Fonction Load()
    /// D�finie la valeur du slider � partir d'une valeur sauvegard�e avec Save()
    /// </summary>
    private void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volumeMusique");
        
    }

    /// <summary>
    /// Fonction Load()
    /// R�cup�re la valeur du slider et l'enregistre pour les autres sc�nes
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("volumeMusique", sliderVolume.value);
    }
}
