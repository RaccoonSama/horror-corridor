using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
 * Description générale
 * Ce script définie ce qu'est la classe Etat
 * qui est utilisée par les ennemis. Les ennemis utilisent des
 * états d'entrée, d'activation et de sortie définies ici.
 * 
 * Script Adapté par Aryane Duperron
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
