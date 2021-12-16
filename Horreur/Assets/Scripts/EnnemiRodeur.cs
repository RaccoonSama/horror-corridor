using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnnemiRodeur : MonoBehaviour
{
    private NavMeshAgent agent;
    private MachineAEtat cerveau;

    private bool alerte;
    private bool attrapeJoueur;
    private bool lumiereAllumee;
    public GameObject joueur;
    /* Variables en lien avec l'audio */
    //public AudioClip fredonnement;
    public AudioClip sonAttaque;
    AudioSource audioSource;

    float changerEsprit;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        //audioSource.volume = Random.Range(0.25f, 0.3f);
        audioSource.pitch = Random.Range(1f, 1.2f);
        //audioSource.PlayOneShot(fredonnement);
        //animation de marche
        //GetComponent<Animator>().SetBool("marche", true);
        Vector3 directionMarche = (Random.insideUnitSphere * 6f) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(directionMarche, out navMeshHit, 6f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }
    void Marcher()
    {
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
        //produire son en loop
        //GetComponent<AudioSource>().Play(fredonnement);
        //Chasse le joueur si lumi�re allum�e
        if (lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirMarcher()
    {
        //GetComponent<Animator>().SetBool("marche", false);
    }

    void EntrerChasserJoueur()
    {
        agent.ResetPath();
        //Animation de chasse du m�chant
        GetComponent<Animator>().SetBool("Chasse", true);
        GetComponent<NavMeshAgent>().speed = 1f;

        //Produire son de chasse
        audioSource.PlayOneShot(sonAttaque);
        audioSource.pitch = 1.3f;
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
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
    }
    void SortirChasserJoueur()
    {
        GetComponent<NavMeshAgent>().speed = 0.4f;
        audioSource.pitch = 1f;
        GetComponent<Animator>().SetBool("Chasse", false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "perso")
        {
            lumiereAllumee = false;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
