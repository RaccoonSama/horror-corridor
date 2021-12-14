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
    public Camera raycastCamera;               // La cam�ra qui s'occupe des raycast. Elle suit la Main Camera, mais ses clipping planes sont normaux

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
        //Code pour v�rifier si le joueur regarde le m�chant
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

        //Code pour v�rifier si le m�chant attrape le joueur
        attrapeJoueur = Vector3.Distance(transform.position, joueur.transform.position) < 1;
    }

    void EntrerImmobile()
    {
        GetComponent<Animator>().SetBool("marche", false);
    }
    void Immobile()
    {
        //produire le son de humming

        //le m�chant fixe le joueur
        agent.transform.LookAt(joueur.transform);

        //si le joueur fixe le m�chant, trigger la chasse
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
        //animation de d�placement vers le joueur
        agent.ResetPath();
        GetComponent<Animator>().SetBool("marche", true);
    }
    void ChasserJoueur()
    {
        //se d�place vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //son de chasse

        //Si le m�chant atteint le joueur
        if (attrapeJoueur)
        {
            //Am�ne au script de d�faite          
        }

        //Si le joueur d�tourne le regard, le m�chant retourne � son �tat immobile
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
        //Ajoute un d�lai avant que le m�chant arr�te d'attaquer
        StartCoroutine(FinChasse());
    }

    //Code pour ajouter le d�lai
    IEnumerator FinChasse()
    {
        print(Time.time);
        yield return new WaitForSeconds(.5f);
        print(Time.time);
    }
}
