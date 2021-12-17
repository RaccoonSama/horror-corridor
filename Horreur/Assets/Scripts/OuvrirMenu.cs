using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
 * Description générale
 * Script qui permet l'ouverture et la Fermeture du menu. Il contient également
 * tout ce qui est en lien avec les menus comme le volume et quitter le jeu
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 15 décembre 2021
 */
public class OuvrirMenu : MonoBehaviour
{
    public GameObject MenuPause;                                // Le gameObject du menu
    [SerializeField] InputActionReference triggerInputAction;   // Le input de la main droite
    public GameObject ligneVisuelle;                            // La ligne qui part de la main gauche
    public Slider sliderVolume;                                 // Le slider du volume

    private void Start()
    {
        /* Récupère la variable du volume de la musique 
        / préalablement définie ou la définie à 1*/
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
        // Active le menu si le bouton X est activé (main gauche)
        if (!MenuPause.gameObject.activeInHierarchy)
        {
            MenuPause.SetActive(true);
            ligneVisuelle.SetActive(true);
        }
        // Désactive le menu si il est déjà activé
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
    /// Ferme l'application lorsque la fonction est appelée
    /// </summary>
    public void QuitterJeu()
    {
        Application.Quit();
    }

    /// <summary>
    /// Fonction ChangerVolume()
    /// Change le volume global en fonction de la valeur écrite sur le slider
    /// </summary>
    public void ChangerVolume()
    {
        AudioListener.volume = sliderVolume.value;
        Save();
    }

    /// <summary>
    /// Fonction Load()
    /// Définie la valeur du slider à partir d'une valeur sauvegardée avec Save()
    /// </summary>
    private void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volumeMusique");
        
    }

    /// <summary>
    /// Fonction Load()
    /// Récupère la valeur du slider et l'enregistre pour les autres scènes
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("volumeMusique", sliderVolume.value);
    }
}
