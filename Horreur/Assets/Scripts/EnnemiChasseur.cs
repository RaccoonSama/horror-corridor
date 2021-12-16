using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnnemiChasseur : MonoBehaviour
{
    private NavMeshAgent agent;
    private MachineAEtat cerveau;
    public int vieEnnemi;
    private bool attrapeJoueur;
    private bool contactlumiere;
    private bool lumiereAllumee;
    public GameObject boutLampeDePoche;
    public GameObject joueur;
    public AudioClip sonChasse;
    public AudioClip sonFuite;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cerveau = GetComponent<MachineAEtat>();
        cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
    }

    private void Update()
    {
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
        //GetComponent<Animator>().SetBool("chasse", true);

        //Produire son de chasse
        GetComponent<AudioSource>().PlayOneShot(sonChasse);
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
        if (contactlumiere && lumiereAllumee)
        {
            cerveau.ActiverEtat(FuirJoueur, EntrerFuirJoueur, SortirFuirJoueur);
        }
    }
    void SortirChasserJoueur()
    {

    }

    void EntrerFuirJoueur()
    {
        GetComponent<NavMeshAgent>().speed = 2.5f;
        GetComponent<AudioSource>().PlayOneShot(sonFuite);
        GetComponent<Animator>().SetBool("Fuite", true);
        agent.ResetPath();
        Vector3 directionMarche = (Random.insideUnitSphere * 6f) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(directionMarche, out navMeshHit, 6f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }
    void FuirJoueur()
    {
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(FuirJoueur, EntrerFuirJoueur, SortirFuirJoueur);
        }

        //Si le méchant reçoit la lumière et si le temps de vie est passé
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirFuirJoueur()
    {
        GetComponent<NavMeshAgent>().speed = 2f;
        GetComponent<Animator>().SetBool("Fuite", false);
    }

    void EntrerMourir()
    {
        //Produire son de mort

        //GetComponent<Animator>().SetBool("mort", true);
    }
    void Mourir()
    {
        //despawn le méchant
        Destroy(gameObject);
    }
    
    void SortirMourir()
    {

    }
    public void PrendreDegats(int degats)
    {
        vieEnnemi -= degats;

        if (vieEnnemi <= 0)
            cerveau.ActiverEtat(Mourir, EntrerMourir, SortirMourir);

        contactlumiere = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "perso")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
