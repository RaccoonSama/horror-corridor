using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
 * Description g�n�rale
 * Ce script d�finie ce qu'est la classe Etat
 * qui est utilis�e par les ennemis. Les ennemis utilisent des
 * �tats d'entr�e, d'activation et de sortie d�finies ici.
 * 
 * Script Adapt� par Aryane Duperron
 */
public class Etat
{
    public Action ActiverAction, EntrerAction, SortirAction;

    public Etat(Action activer, Action entrer, Action sortir)
    {
        ActiverAction = activer;
        EntrerAction = entrer;
        SortirAction = sortir;
    }
    public void Executer()
    {
        if (ActiverAction != null)
            ActiverAction.Invoke();
    }
    public void Entrer()
    {
        if (EntrerAction != null)
            EntrerAction.Invoke();
    }
    public void Sortir()
    {
        if (SortirAction != null)
            SortirAction.Invoke();
    }
}
