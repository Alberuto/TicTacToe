using Fusion;
using System;
using UnityEngine;

public class NetworkTicTacToe : NetworkBehaviour {

    [Networked, Capacity(9)]
    public NetworkArray<int> Board => default;

    [Networked]
    public PlayerPrefs ThisTurn {
        get; set;
    }
    [Networked]
    public bool EndGame {
        get; set;
    }
    public override void Spawned() {

        if (HasStateAuthority) {

            //ThisTurn = Runner.LocalPlayer;

            EndGame = false;

            for (int i = 0; i < 9; i++) {

                Board.Set(i, 0);

            }
        }
    }
    public void TryTurn(int index) {

        if (EndGame) {
            return;
        }
        else {
            RPC_Play(index);
        }
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_Play(int index) {

        if (EndGame) return;

        if (Board.Get(index) != 0) return;

        //if(Runner.LocalPlayer != ThisTurn) return;

       // int player = (ThisTurn == Runner.ActivePlayers[0]) ? 1 : 2;

       // Board.Set(index, player);

      /*  if (CheckWin(player))
        {
            EndGame = true;
        }
        else {
            ChangeTurn();
        }*/
    }

    private void ChangeTurn() {


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


            
        }

        return false;
    }
}