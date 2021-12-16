using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OuvrirMenu : MonoBehaviour
{
    public GameObject MenuPause;
    [SerializeField] InputActionReference triggerInputAction;
    public GameObject ligneVisuelle;
    public Slider sliderVolume;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("volumeMusique"))
        {
            PlayerPrefs.SetFloat("volumeMusique", 1);
            Save();
        }
        else
        {
            Load();
        }
    }
    private void OnEnable()
    {
        triggerInputAction.action.performed += XPressed;
    }

    private void XPressed(InputAction.CallbackContext obj)
    {
        if (!MenuPause.gameObject.activeInHierarchy)
        {
            MenuPause.SetActive(true);
            ligneVisuelle.SetActive(true);
        }
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

    public void FermerMenu()
    {
        MenuPause.SetActive(false);
        ligneVisuelle.SetActive(false);
    }

    public void QuitterJeu()
    {
        Application.Quit();
    }

    public void ChangerVolume()
    {
        AudioListener.volume = sliderVolume.value;
        Save();
    }

    private void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volumeMusique");
        
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("volumeMusique", sliderVolume.value);
    }
}
