using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {FreeRoam, Dialog}
public class GameController : MonoBehaviour
{
    [SerializeField] NewMainController newMainController;
    [SerializeField] Camera worldCamera;
    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        state = GameState.FreeRoam;
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }
    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            newMainController.Update();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        
    }
}
