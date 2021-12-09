using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneApprove1 : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        

        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "perso")
        {
            
           
           
            if (LoadScene.indexCount == 3)
            {
                Debug.Log(LoadScene.indexCount);
                Debug.Log(LoadScene.isObjectiveDone);
                LoadScene.isObjectiveDone = true;
            }else
            {
                this.gameObject.SetActive(false);
                Debug.Log(LoadScene.indexCount);
                LoadScene.indexCount++;
            }
        }
    }
}
