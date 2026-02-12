using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour {

    public NetworkTicTacToe game;
    public Button[] buttons;
    public Text turnText;
    public Text endGameText;

    private void Update() {

        if (game == null) {
            return;
        }
        for (int i = 0; i < 9; i++) {

            int index = game.Board.Get(i);
            buttons[i].GetComponentInChildren<Text>().text = game.board[i];

        }
    }
}