using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneApprove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "perso")
        {
            Debug.Log("woooooooooooooooooo");
            LoadScene.isObjectiveDone = true;
        }
    }
    }
