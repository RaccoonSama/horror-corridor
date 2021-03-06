using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public int SceneIndex;
    public float transitionTime;
    public GameObject canvas;
    public static bool isObjectiveDone;
    public static int indexCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
       /* if (SceneIndex == 1 | SceneIndex == 2)
        {
            isObjectiveDone = false;
        }
        else { isObjectiveDone = true; }
        
        Debug.Log("Scene" +SceneIndex + isObjectiveDone);*/
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "perso")
        {
            // Ajouter && isObjectiveDone quand les objectifs vont ?tre prenable
            Debug.Log("fdaswefsdf");
            StartCoroutine(gestionChangementScene());
        }
    }


    IEnumerator gestionChangementScene()
    {

        canvas.GetComponent<Animator>().SetTrigger("start");
      
        yield return new WaitForSeconds(transitionTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex + 1);

    }
}
