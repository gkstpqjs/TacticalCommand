using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Ready,
    Playing,
    End
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public BattleState State { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        State = BattleState.Playing;
        Debug.Log("Battle Start");
    }

    public void EndBattle(bool isPlayerWin)
    {
        State = BattleState.End;
        Debug.Log(isPlayerWin ? "Player Win" : "Player Lose");
    }
}