using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmerLumiere : MonoBehaviour
{
    public GameObject lumiereBlessante;
    public Transform boutLampe;
    public float vitesseLumiere;
    public float cadenceTir;
    private float dernierTir;

    public bool PeutTirer()
    {
        if (Time.time - dernierTir >= cadenceTir)
        {
            return true;
        }

        return false;
    }

    // called when we want to shoot a bullet
    public void TirerLumiere()
    {
        dernierTir = Time.time;

        GameObject obj = Instantiate(lumiereBlessante);
        // GameObject bullet = bulletPool.GetObject();

        obj.transform.position = boutLampe.position;
        obj.transform.rotation = boutLampe.rotation;

        // set the velocity
        obj.GetComponent<Rigidbody>().velocity = boutLampe.forward * vitesseLumiere;
    }
}
