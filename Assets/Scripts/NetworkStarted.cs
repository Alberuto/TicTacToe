using UnityEngine;
using Fusion;

public class NetworkStarted : MonoBehaviour {

    public NetworkRunner runnerPrefab;

    private async void Start() {
        
        var runner = Instantiate(runnerPrefab); 

        runner.ProvideInput = false;

        await runner.StartGame(new StartGameArgs() {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "Test"
        });
    }
    void Update() {
        
    }
}