using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiChasseur : MonoBehaviour
{
    private NavMeshAgent agent;
    private MachineAEtat cerveau;
    private float tempsDeVie;
    private float distanceMax;

    //private bool aveugle;
    private bool attrapeJoueur;
    private bool contactVisuel;

    private bool lumiereAllumee;

    public GameObject joueur;
    public Camera raycastCamera;
    private void Start()
    {
        distanceMax = 1.9f;

        tempsDeVie = Random.Range(10, 20);
        agent = GetComponent<NavMeshAgent>();
        cerveau = GetComponent<MachineAEtat>();
        cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
    }

    private void Update()
    {
        tempsDeVie -= Time.deltaTime;

        //Code pour v�rifier si le joueur pointe le m�chant
        Ray camRay = raycastCamera.ScreenPointToRay(Input.mousePosition, raycastCamera.stereoActiveEye);
        RaycastHit infoCollision;

        if (Physics.Raycast(camRay.origin, camRay.direction, out infoCollision, distanceMax) && infoCollision.collider.gameObject.tag == "Joueur")
        {
            contactVisuel = true;
        }
        else
        {
            contactVisuel = false;
        }

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

        //Si le m�chant re�oit lumi�re : trigger state de fuite
        if (contactVisuel && lumiereAllumee)
        {
            cerveau.ActiverEtat(FuirJoueur, EntrerFuirJoueur, SortirFuirJoueur);
        }
    }
    void SortirChasserJoueur()
    {
        GetComponent<Animator>().SetBool("chasse", false);
    }

    void EntrerFuirJoueur()
    {
        GetComponent<Animator>().SetBool("fuite", true);
    }
    void FuirJoueur()
    {
        //M�chant s'�loigne du joueur

        //Si le m�chant re�oit la lumi�re et si le temps de vie est pass�
        if (tempsDeVie <= 0 && true)
        {
            cerveau.ActiverEtat(Mourir, EntrerMourir, SortirMourir);
        }
    }
    void SortirFuirJoueur()
    {
        GetComponent<Animator>().SetBool("fuite", false);
    }

    void EntrerMourir()
    {
        //Produire son de mort

        GetComponent<Animator>().SetBool("mort", true);
    }
    void Mourir()
    {
        //despawn le m�chant
        Destroy(gameObject);
    }
    void SortirMourir()
    {

    }
}
