using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AllumerLampe : MonoBehaviour
{
	[Space(10)]
	[SerializeField()] GameObject Lights; // all light effects and spotlight
	[SerializeField()] AudioSource switch_sound; // audio of the switcher
	[SerializeField()] ParticleSystem dust_particles; // dust particles
	[SerializeField] InputActionReference triggerInputAction;

	private Light spotlight;
	private Material ambient_light_material;
	private Color ambient_mat_color;
	public static bool lumiereAllumee = false;

	public int batterieTemps = 100;
	public GameObject batterie;


	// Use this for initialization
	void Start()
	{
		// cache components
		spotlight = Lights.transform.Find("Spotlight").GetComponent<Light>();
		ambient_light_material = Lights.transform.Find("ambient").GetComponent<Renderer>().material;
		ambient_mat_color = ambient_light_material.GetColor("_TintColor");
	}

    private void Update()
    {
        if (lumiereAllumee)
        {
			batterie.GetComponent<Slider>().value -= Time.deltaTime/8;
		}
        else
        {
			batterie.GetComponent<Slider>().value += Time.deltaTime/16;
		}

        if (batterie.GetComponent<Slider>().value >= 0)
        {
			Switch();
        }
	}
    /// <summary>
    /// changes the intensivity of lights from 0 to 100.
    /// call this from other scripts.
    /// </summary>
    public void Change_Intensivity(float percentage)
	{
		percentage = Mathf.Clamp(percentage, 0, 100);
		spotlight.intensity = (8 * percentage) / 100;
		ambient_light_material.SetColor("_TintColor", new Color(ambient_mat_color.r, ambient_mat_color.g, ambient_mat_color.b, percentage / 2000));
	}


	/// <summary>
	/// switch current state  ON / OFF.
	/// call this from other scripts.
	/// </summary>
	public void Switch()
	{
		lumiereAllumee = !lumiereAllumee;
		Lights.SetActive(lumiereAllumee);
		if (switch_sound != null)
			switch_sound.Play();
	}


	/// <summary>
	/// enables the particles.
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

	private void TriggerPressed(InputAction.CallbackContext obj)
	{
		Switch();
	}

	private void OnDisable()
	{
		triggerInputAction.action.performed -= TriggerPressed;
	}


}
