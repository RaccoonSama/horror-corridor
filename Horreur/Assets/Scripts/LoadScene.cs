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
        isObjectiveDone = false;
        Debug.Log(SceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void OnCollisionEnter(Collision other)
    {
        
        if(other.gameObject.name == "perso" && isObjectiveDone)
        {

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
