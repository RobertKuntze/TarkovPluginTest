using EFT;
using UnityEngine;
using Comfort.Common;
using BepInEx;
using System.Collections.Generic;

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

            var allPlayersList = gameWorld.AllAlivePlayersList;

            if (!(gameTimer.method_0().TotalSeconds < 30)) {
                return;
            }

            var survivingPlayers = new List<Player>();
            foreach (Player player in allPlayersList) {
                if (player.IsAI && UnityEngine.Random.Range(0,2) == 1 && !allPlayersList.Contains(player)) {
                    player.KillMe(EBodyPartColliderType.HeadCommon, 1);
                    Logger.LogWarning("Kill Confirmed");
                }
                // Logger.LogWarning("Not Killed");
                survivingPlayers.Add(player);
            }


            
        }

        private bool Ready() {
            if (!Singleton<GameWorld>.Instantiated || gameWorld.AllAlivePlayersList == null || gameWorld.AllAlivePlayersList.Count <= 0) return false;
            return true;
        }
    }
}