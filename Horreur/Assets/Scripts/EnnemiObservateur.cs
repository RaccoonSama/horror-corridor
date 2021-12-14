using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiObservateur : MonoBehaviour
{
    private NavMeshAgent agent;
    private MachineAEtat cerveau;

    public float distanceMax;
    private bool contactVisuel;
    private bool attrapeJoueur;

    public GameObject joueur;
    public Camera raycastCamera;               // La caméra qui s'occupe des raycast. Elle suit la Main Camera, mais ses clipping planes sont normaux

    private void Start()
    {
        //distanceMax = 1.9f;
        contactVisuel = false;
        attrapeJoueur = false;

        agent = GetComponent<NavMeshAgent>();
        cerveau = GetComponent<MachineAEtat>();
        cerveau.ActiverEtat(Immobile, EntrerImmobile, SortirImmobile);
    }

    private void Update()
    {
        //Code pour vérifier si le joueur regarde le méchant
        Ray camRay = raycastCamera.ScreenPointToRay(Input.mousePosition, raycastCamera.stereoActiveEye);
        RaycastHit infoCollision;
        int layerMask = 6;
        layerMask = ~layerMask;
        //&& infoCollision.collider.gameObject.tag == "Observateur"
        if (Physics.Raycast(camRay.origin, camRay.direction, out infoCollision, distanceMax, layerMask) )
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * infoCollision.distance, Color.yellow);
            contactVisuel = true;
        }
        else
        {
            contactVisuel = false;
        }

        //Code pour vérifier si le méchant attrape le joueur
        attrapeJoueur = Vector3.Distance(transform.position, joueur.transform.position) < 1;
    }

    void EntrerImmobile()
    {
        GetComponent<Animator>().SetBool("marche", false);
    }
    void Immobile()
    {
        //produire le son de humming

        //le méchant fixe le joueur
        agent.transform.LookAt(joueur.transform);

        //si le joueur fixe le méchant, trigger la chasse
        if (contactVisuel)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirImmobile()
    {
        //produire un son de rire creepy

    }

    void EntrerChasserJoueur()
    {
        //animation de déplacement vers le joueur
        agent.ResetPath();
        GetComponent<Animator>().SetBool("marche", true);
    }
    void ChasserJoueur()
    {
        //se déplace vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //son de chasse

        //Si le méchant atteint le joueur
        if (attrapeJoueur)
        {
            //Amène au script de défaite          
        }

        //Si le joueur détourne le regard, le méchant retourne à son état immobile
        if (contactVisuel == false)
        {
            cerveau.ActiverEtat(Immobile, EntrerImmobile, SortirImmobile);
        }
        else
        {

        }
    }
    void SortirChasserJoueur()
    {
        //Ajoute un délai avant que le méchant arrête d'attaquer
        StartCoroutine(FinChasse());
    }

    //Code pour ajouter le délai
    IEnumerator FinChasse()
    {
        print(Time.time);
        yield return new WaitForSeconds(.5f);
        print(Time.time);
    }
}
