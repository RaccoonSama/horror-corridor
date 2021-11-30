using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int SceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(SceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("fdaswefsdf");
        if(other.gameObject.name == "perso")
        {
            Debug.Log("machin");
            SceneManager.LoadScene(SceneIndex + 1);
        }
    }
}
