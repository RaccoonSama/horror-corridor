using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiRodeur : MonoBehaviour
{
    private NavMeshAgent agent;
    private MachineAEtat cerveau;

    private bool alerte;
    private bool attrapeJoueur;
    private bool lumiereAllumee;

    public GameObject joueur;
    public Camera raycastCamera;
    private void Start()
    {
        alerte = false;
        attrapeJoueur = false;
        agent = GetComponent<NavMeshAgent>();
        cerveau = GetComponent<MachineAEtat>();
        cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
    }

    private void Update()
    {
        //Code pour v�rifier si la lumi�re est allum�
        if (AllumerLampe.lumiereAllumee)
        {
            lumiereAllumee = true;
        }
        else
        {
            lumiereAllumee = false;
        }
    }

    void EntrerMarcher()
    {
        //animation de marche
        GetComponent<Animator>().SetBool("marche", true);
        Vector3 directionMarche = (Random.insideUnitSphere * 4f) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(directionMarche, out navMeshHit, 4f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }
    void Marcher()
    {
        //produire son en loop

        //Chasse le joueur si lumi�re allum�e
        if (lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirMarcher()
    {
        GetComponent<Animator>().SetBool("marche", false);
    }

    void EntrerChasserJoueur()
    {
        agent.ResetPath();
        //Animation de chasse du m�chant
        GetComponent<Animator>().SetBool("chasse", true);

        //Produire son de chasse
    }
    void ChasserJoueur()
    {
        //M�chant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //Si le m�chant touche au joueur : d�faite
        if (attrapeJoueur)
        {
            //Am�ne au script de d�faite
        }

        //Si le joueur ferme sa lumi�re
        if (lumiereAllumee == false)
        {
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
    }
    void SortirChasserJoueur()
    {
        GetComponent<Animator>().SetBool("chasse", false);
    }

}
