using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class ProfileController : IBonusObserver
    {
        public PlayerProfile playerProfile;
        public Player player { get; set; }

        public ProfileController(PlayerProfile playerProfile, Player player)
        {
            this.playerProfile = playerProfile;
            this.player = player;
        }

        public ProfileController(PlayerProfile playerProfile)
        {
            this.playerProfile = playerProfile;
            this.player = new Player();
        }

        public void Load()
        {
            if (!File.Exists(playerProfile.dataPath))
            {
                player.score = 0;
                player.speed = 1f;
                return;
            }

            string str = File.ReadAllText(playerProfile.dataPath);
            PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(str);
            player.speed = playerSaveData.speed;
            player.score = playerSaveData.score;
        }

        public void Save()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData(player);
            string str = JsonUtility.ToJson(playerSaveData);
            File.WriteAllText(playerProfile.dataPath, str);
        }

        public void PlayerTakeBonus(IBonus bonus)
        {
            if (bonus.bonusType == BonusType.Score) Save();
        }
    }
}
