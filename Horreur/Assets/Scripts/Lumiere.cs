using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumiere : MonoBehaviour
{
    public int degats;
    public float tempsVie;
    private float tempsTir;

    public GameObject impactParticules;

    void OnEnable()
    {
        tempsTir = Time.time;
    }

    void Update()
    {
        if (Time.time - tempsTir >= tempsVie)
        {
            Destroy(gameObject);
        }     
    }
    void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.CompareTag("Chasseur"))
        {
            infoCollision.GetComponent<EnnemiChasseur>().PrendreDegats(degats);
            GameObject obj = Instantiate(impactParticules, transform.position, Quaternion.identity);
            Destroy(obj, 0.5f);
        }
        Destroy(gameObject);
    }
}
