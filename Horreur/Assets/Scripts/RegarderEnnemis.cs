using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegarderEnnemis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        int layerMask = 8;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
            Debug.Log(hit);
        }
            
    }
}
