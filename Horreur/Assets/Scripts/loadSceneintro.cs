using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadSceneintro : MonoBehaviour
{
    public float transitionTime;
    int SceneIndex;
    public GameObject canvas;

    private void Start()
    {
        
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke("ChangerScene", 43.5f);
    }
    /*
    void OnTriggerEnter(Collider infosCollider)
    {
        if(infosCollider.gameObject.name == "Ascenceur")
        {
            Debug.Log("ici");
            SceneManager.LoadScene("Scene1");
        }
    }*/
    void ChangerScene()
    {
        //SceneManager.LoadScene("Scene1");
        StartCoroutine(gestionChangementScene());
    }

    IEnumerator gestionChangementScene()
    {

        canvas.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex + 1);

    }
}
