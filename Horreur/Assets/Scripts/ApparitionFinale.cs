using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionFinale : MonoBehaviour
{
    public GameObject Chasseur;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ApparitionChasseur", 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApparitionChasseur()
    {
        Chasseur.SetActive(true);
    }
}
