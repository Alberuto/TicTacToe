using Fusion;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;

public class NetworkTicTacToe : NetworkBehaviour {

    [Networked, Capacity(9)]
    public NetworkArray<int> Board => default;

    [Networked]
    public PlayerRef ThisTurn {
        get; set;
    }
    [Networked]
    public bool EndGame {
        get; set;
    }
    public override void Spawned() {

        if (HasStateAuthority) {

            ThisTurn = Runner.LocalPlayer;
            EndGame = false;

            for (int i = 0; i < 9; i++) {

                Board.Set(i, 0);
            }
        }
    }
    public void TryTurn(int index) {
        
        Debug.Log($"Player {Runner.LocalPlayer} is trying to play at index {index}");
        Debug.Log("intentamos" + index);

        if (EndGame) 
            return;
        
        RPC_Play(index);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_Play(int index, RpcInfo info = default) {

        Debug.Log($"RPC_Play ejecutado en "+Runner.LocalPlayer);

        if (EndGame) return;

        if (Board.Get(index) != 0) return;

        PlayerRef playerCaller = info.Source; //player that called the RPC

        Debug.Log($"Player {playerCaller} called RPC_Play with index {index}");

        if (playerCaller != ThisTurn) return;

          int player = (ThisTurn == Runner.ActivePlayers.ToList()[0]) ? 1 : 2;

          Board.Set(index, player);

          if (CheckWin(player)) 
              EndGame = true;
          else 
              ChangeTurn();
    }
    private void ChangeTurn() {

        foreach (var player in Runner.ActivePlayers) {

            if (player != ThisTurn) {
                ThisTurn = player;
                break;
            }
        }   
    }
    private bool CheckWin(object player) {

        int [,] combination = new int[,] {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };
        for (int  i  = 0;  i  < combination.GetLength(0);  i ++) {

                if (Board.Get(combination[i,0]) == (int)player &&
                    Board.Get(combination[i,1]) == (int)player &&
                    Board.Get(combination[i,2]) == (int)player) 
                return true;
        }
        return false;
    }
}