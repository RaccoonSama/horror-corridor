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

        //Code pour vérifier si le joueur pointe le méchant
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

        //Code pour vérifier si la lumière est allumé
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
        //Animation de chasse du méchant
        GetComponent<Animator>().SetBool("chasse", true);

        //Produire son de chasse
    }
    void ChasserJoueur()
    {
        //Méchant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //Si le méchant touche au joueur : défaite
        if (attrapeJoueur)
        {
            //Amène au script de défaite
        }

        //Si le méchant reçoit lumière : trigger state de fuite
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
        //Méchant s'éloigne du joueur

        //Si le méchant reçoit la lumière et si le temps de vie est passé
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
        //despawn le méchant
        Destroy(gameObject);
    }
    void SortirMourir()
    {

    }
}
