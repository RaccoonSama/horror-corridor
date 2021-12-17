using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*****************************************************************
 * Script AllumerLampe()
 * Pris dans l'asset store de Unity et modifié par Aryane Duperron
 * https://assetstore.unity.com/packages/3d/props/tools/flashlight-pro-53053
 ****************************************************************/
public class AllumerLampe : MonoBehaviour
{
	[Space(10)]
	[SerializeField()] GameObject Lights; // Les lumières de la lampe de poche
	[SerializeField()] AudioSource switch_sound; // Le son de clic lorsque la lampe de poche allume
	[SerializeField()] ParticleSystem dust_particles; // Des particules de lumière
	[SerializeField] InputActionReference triggerInputAction;	// L'input de trigger sur la manette de VR
	private Light spotlight;
	private Material ambient_light_material;
	private Color ambient_mat_color;

	public static bool lumiereAllumee = false;
	public GameObject batterie;			// Le HUD de la batterie
	private ArmerLumiere armeLumiere;	// Les balles de lumières à tirer

	void Start()
	{
		armeLumiere = GetComponent<ArmerLumiere>();
		spotlight = Lights.transform.Find("Spotlight").GetComponent<Light>();
		ambient_light_material = Lights.transform.Find("ambient").GetComponent<Renderer>().material;
		ambient_mat_color = ambient_light_material.GetColor("_TintColor");
	}

    private void Update()
    {
		// Si la lumière est allumée, baisse la batterie dans la lampe de poche
        if (lumiereAllumee)
        {
			// Si la flashlight peut tirer, génère des balles de lumière
            if (armeLumiere.PeutTirer())
            {
				armeLumiere.TirerLumiere();
            }
			batterie.GetComponent<Slider>().value -= Time.deltaTime/8;
		}
        else
        {
			// Augmente la batterie
			batterie.GetComponent<Slider>().value += Time.deltaTime/16;
		}
		// Si la batterie est à plat, ferme la lampe de poche
        if (batterie.GetComponent<Slider>().value <= 0)
        {
			Switch();
        }
	}

    /// <summary>
	/// change l'intesité de la lumière de 0 à 100
    /// </summary>
    public void Change_Intensivity(float percentage)
	{
		percentage = Mathf.Clamp(percentage, 0, 100);
		spotlight.intensity = (8 * percentage) / 100;
		ambient_light_material.SetColor("_TintColor", new Color(ambient_mat_color.r, ambient_mat_color.g, ambient_mat_color.b, percentage / 2000));
	}


	/// <summary>
	/// Change la valeur de la lumière allumée
	/// </summary>
	public void Switch()
	{
		lumiereAllumee = !lumiereAllumee;
		Lights.SetActive(lumiereAllumee);
		if (switch_sound != null)
			switch_sound.Play();
	}


	/// <summary>
	/// Active les particules
	/// </summary>
	public void Enable_Particles(bool value)
	{
		if (dust_particles != null)
		{
			if (value)
			{
				dust_particles.gameObject.SetActive(true);
				dust_particles.Play();
			}
			else
			{
				dust_particles.Stop();
				dust_particles.gameObject.SetActive(false);
			}
		}
	}

    private void OnEnable()
    {
		triggerInputAction.action.performed += TriggerPressed;
	}

	// Si le joueur appuie sur le trigger, active ou désactive la lampe de poche
	private void TriggerPressed(InputAction.CallbackContext obj)
	{
		Switch();
	}

	private void OnDisable()
	{
		triggerInputAction.action.performed -= TriggerPressed;
	}
}
