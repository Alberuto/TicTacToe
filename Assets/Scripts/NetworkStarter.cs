using UnityEngine;
using Fusion;

public class NetworkStarter : MonoBehaviour {

    public NetworkRunner runnerPrefab;
    public NetworkObject tictactoePrefab;

    private async void Start() {
        
        var runner = Instantiate(runnerPrefab); 

        runner.ProvideInput = false;

        await runner.StartGame(new StartGameArgs() {
            GameMode = GameMode.Shared,
            SessionName = "Test",
            SceneManager = runner.GetComponent<NetworkSceneManagerDefault>()
        });

        if (runner.IsSharedModeMasterClient) { 
            runner.Spawn(tictactoePrefab);
        }
    }
}