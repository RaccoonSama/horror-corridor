using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 * Description générale
 * Script qui défini le comportement des ennemis rôdeurs
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 16 décembre 2021
 */
public class EnnemiRodeur : MonoBehaviour
{
    /* variables de la machine à état */
    private NavMeshAgent agent;
    private MachineAEtat cerveau;

    private bool lumiereAllumee;    // Détermine si la lampe de poche est allumée
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
        //Code pour vérifier si la lumière est allumé
        if (AllumerLampe.lumiereAllumee){lumiereAllumee = true;}
        else{lumiereAllumee = false;}
    }

    void EntrerMarcher()
    {
        // Fait marcher aléatoirement l'ennemi dans la scène
        audioSource.pitch = Random.Range(1f, 1.2f);
        Vector3 directionMarche = (Random.insideUnitSphere * 6f) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(directionMarche, out navMeshHit, 6f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }
    void Marcher()
    {
        // Si l'ennemi est près de son point d'arrivée, il marche à un autre point
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
        // Si la lumière est allumée, le rôdeur chasse le joueur
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
        // Animation de chasse du méchant
        GetComponent<Animator>().SetBool("Chasse", true);
        GetComponent<NavMeshAgent>().speed = 1f;

        // Produire son de chasse
        audioSource.PlayOneShot(sonAttaque);
        audioSource.pitch = 1.3f;
    }
    void ChasserJoueur()
    {
        // Le méchant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        //Si le joueur ferme sa lumière
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(Marcher, EntrerMarcher, SortirMarcher);
        }
    }
    void SortirChasserJoueur()
    {
        // Change la vitesse de l'ennemi et il arrête de chasser
        GetComponent<NavMeshAgent>().speed = 0.4f;
        audioSource.pitch = 1f;
        GetComponent<Animator>().SetBool("Chasse", false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Si l'ennemi touche au joueur, reload la scène
        if (collision.gameObject.tag == "perso")
        {
            lumiereAllumee = false;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
