using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField]Dialog dialog;

    public void Interact()
    {
        
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
