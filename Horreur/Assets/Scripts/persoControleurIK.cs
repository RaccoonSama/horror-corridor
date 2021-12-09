using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persoControleurIK : MonoBehaviour {

    private Animator persoAnimateur;
    public Transform cible;

    void Start() {

        persoAnimateur = GetComponent<Animator>();
        
    }

    private void OnAnimatorIK()
    {
        persoAnimateur.SetLookAtWeight(1);
        persoAnimateur.SetLookAtPosition(cible.position);

        persoAnimateur.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        persoAnimateur.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        persoAnimateur.SetIKPosition(AvatarIKGoal.RightHand, cible.position);
        persoAnimateur.SetIKRotation(AvatarIKGoal.RightHand, cible.rotation);
    }

   
}
