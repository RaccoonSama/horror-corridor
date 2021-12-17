using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 * Description g�n�rale
 * Script qui d�fini le comportement des ennemis chasseurs
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 16 d�cembre 2021
 */
public class EnnemiChasseur : MonoBehaviour
{
    /* variables de la machine � �tat */
    private NavMeshAgent agent;
    private MachineAEtat cerveau;
    public int vieEnnemi;           // Le nombre de points de vie de l'ennemi
    private bool contactlumiere;    // D�termine si l'ennemi est en contact avec la lumi�re
    private bool lumiereAllumee;    // D�termine si la lumi�re est allum�e
    public GameObject boutLampeDePoche; 
    public GameObject joueur;
    /* Variables en lien avec l'audio */
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
        // Produire son de chasse
        GetComponent<AudioSource>().PlayOneShot(sonChasse);
    }
    void ChasserJoueur()
    {
        // M�chant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        // Si le m�chant re�oit lumi�re : trigger state de fuite
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
        // Quand le m�chant est en fuite, il court n'importe o� et un son de fuite est jou�
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
        // Si l'ennemi en fuite est pr�s de son point d'arriv�e, il fuit vers un autre point
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(FuirJoueur, EntrerFuirJoueur, SortirFuirJoueur);
        }

        //Si le joueur ferme sa lumi�re, l'ennemi se remet � le chasser
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirFuirJoueur()
    {
        // Remet la vitesse du m�chant normale et active son animation d'attaque
        GetComponent<NavMeshAgent>().speed = 2f;
        GetComponent<Animator>().SetBool("Fuite", false);
    }

    /// <summary>
    /// Fonction qui est appel�e lorsque l'ennemi doit perdre de la vie
    /// </summary>
    /// <param name="degats"> Les d�gats de l'attaque de lumi�re</param>
    public void PrendreDegats(int degats)
    {
        vieEnnemi -= degats;
        // Si l'ennemi n'a plus de vie, son gameObject est d�truit
        if (vieEnnemi <= 0) { Destroy(gameObject); }
            
        contactlumiere = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Si l'ennemi touche au joueur, reload la sc�ne
        if (collision.gameObject.tag == "perso")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
