using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continuer : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
