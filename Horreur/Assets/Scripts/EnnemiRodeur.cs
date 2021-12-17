using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 * Description g�n�rale
 * Script qui d�fini le comportement des ennemis r�deurs
 * 
 * Cr�� par : Aryane Duperron
 * Derni�re modifications : 16 d�cembre 2021
 */
public class EnnemiRodeur : MonoBehaviour
{
    /* variables de la machine � �tat */
    private NavMeshAgent agent;
    private MachineAEtat cerveau;

    private bool lumiereAllumee;    // D�termine si la lampe de poche est allum�e
    public GameObject joueur;       // Le gameObject du joueur

    /* Variables en lien avec l'audio */
    public AudioClip sonAttaque;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        cerveau = GetComponent<MachineAEtat>();
        cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
    }

    private void Update()
    {
        //Code pour v�rifier si la lumi�re est allum�
        if (AllumerLampe.lumiereAllumee){lumiereAllumee = true;}
        else{lumiereAllumee = false;}
    }

    void EntrerMarcher()
    {
        // Fait marcher al�atoirement l'ennemi dans la sc�ne
        audioSource.pitch = Random.Range(1f, 1.2f);
        Vector3 directionMarche = (Random.insideUnitSphere * 6f) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(directionMarche, out navMeshHit, 6f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }
    void Marcher()
    {
        // Si l'ennemi est pr�s de son point d'arriv�e, il marche � un autre point
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
        // Si la lumi�re est allum�e, le r�deur chasse le joueur
        if (lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirMarcher()
    {

    }

    void EntrerChasserJoueur()
    {
        agent.ResetPath();
        // Animation de chasse du m�chant
        GetComponent<Animator>().SetBool("Chasse", true);
        GetComponent<NavMeshAgent>().speed = 1f;

        // Produire son de chasse
        audioSource.PlayOneShot(sonAttaque);
        audioSource.pitch = 1.3f;
    }
    void ChasserJoueur()
    {
        // Le m�chant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //Si le joueur ferme sa lumi�re
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
    }
    void SortirChasserJoueur()
    {
        // Change la vitesse de l'ennemi et il arr�te de chasser
        GetComponent<NavMeshAgent>().speed = 0.4f;
        audioSource.pitch = 1f;
        GetComponent<Animator>().SetBool("Chasse", false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Si l'ennemi touche au joueur, reload la sc�ne
        if (collision.gameObject.tag == "perso")
        {
            lumiereAllumee = false;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
