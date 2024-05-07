using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;

namespace MyFirstPlugin {
    [BepInPlugin("com.example.MyFirstPlugin", "MyFirstPlugin", "0.1")]
    public class MyFirstPlugin : BaseUnityPlugin {
        private static GameWorld gameWorld;
        private static LocalGame game;
        
        private void Awake() {

            Logger.LogWarning("Plugin initiated");
        }

        private void Update() {
            gameWorld = Singleton<GameWorld>.Instance;

            if (!Ready()) return;

            game = GameObject.Find("GAME").GetComponent<LocalGame>();
            var gameTimer = game.GameTimer;

            var allPlayersList = gameWorld.AllPlayersEverExisted;

            if (!(gameTimer.method_0().TotalSeconds < 30)) {
                return;
            }

            var rand = new Random();
            int number = rand.Next(2);
            foreach (Player player in allPlayersList) {
                if (player.IsAI) {
                    player.KillMe(EBodyPartColliderType.HeadCommon, 999);
                }
            }

            // Logger.LogWarning(gameWorld.AllAlivePlayersList);

            
        }

        private bool Ready() {
            if (!Singleton<GameWorld>.Instantiated || gameWorld.AllAlivePlayersList == null || gameWorld.AllAlivePlayersList.Count <= 0) return false;
            return true;
        }
    }
}