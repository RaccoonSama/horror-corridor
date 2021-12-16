using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadSceneintro : MonoBehaviour
{

    void OnTriggerEnter(Collider infosCollider)
    {
        if(infosCollider.gameObject.name == "Ascenceur")
        {
            Debug.Log("ici");
            SceneManager.LoadScene("Scene1");
        }
    }
}
