using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 * Description générale
 * Script qui défini le comportement des ennemis chasseurs
 * 
 * Créé par : Aryane Duperron
 * Dernière modifications : 16 décembre 2021
 */
public class EnnemiChasseur : MonoBehaviour
{
    /* variables de la machine à état */
    private NavMeshAgent agent;
    private MachineAEtat cerveau;
    public int vieEnnemi;           // Le nombre de points de vie de l'ennemi
    private bool contactlumiere;    // Détermine si l'ennemi est en contact avec la lumière
    private bool lumiereAllumee;    // Détermine si la lumière est allumée
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
        // Produire son de chasse
        GetComponent<AudioSource>().PlayOneShot(sonChasse);
    }
    void ChasserJoueur()
    {
        // Méchant va vers le joueur
        GetComponent<NavMeshAgent>().SetDestination(joueur.transform.position);

        // Si le méchant reçoit lumière : trigger state de fuite
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
        // Quand le méchant est en fuite, il court n'importe où et un son de fuite est joué
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
        // Si l'ennemi en fuite est près de son point d'arrivée, il fuit vers un autre point
        if (agent.remainingDistance <= 1f)
        {
            agent.ResetPath();
            cerveau.ActiverEtat(FuirJoueur, EntrerFuirJoueur, SortirFuirJoueur);
        }

        //Si le joueur ferme sa lumière, l'ennemi se remet à le chasser
        if (!lumiereAllumee)
        {
            cerveau.ActiverEtat(ChasserJoueur, EntrerChasserJoueur, SortirChasserJoueur);
        }
    }
    void SortirFuirJoueur()
    {
        // Remet la vitesse du méchant normale et active son animation d'attaque
        GetComponent<NavMeshAgent>().speed = 2f;
        GetComponent<Animator>().SetBool("Fuite", false);
    }

    /// <summary>
    /// Fonction qui est appelée lorsque l'ennemi doit perdre de la vie
    /// </summary>
    /// <param name="degats"> Les dégats de l'attaque de lumière</param>
    public void PrendreDegats(int degats)
    {
        vieEnnemi -= degats;
        // Si l'ennemi n'a plus de vie, son gameObject est détruit
        if (vieEnnemi <= 0) { Destroy(gameObject); }
            
        contactlumiere = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Si l'ennemi touche au joueur, reload la scène
        if (collision.gameObject.tag == "perso")
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
