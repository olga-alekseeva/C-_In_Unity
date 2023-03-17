using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class ProfileListController
    {
        private readonly string listDataPath;

        public ProfileList profileList;

        public ProfileListController()
        {
            listDataPath = Path.Combine(Application.persistentDataPath, "profilelist.dat");
            Load();
        }

        public void SetLastProfile(PlayerProfile playerProfile)
        {
            profileList.lastProfile = playerProfile;
            Save();
        }

        private string GetDataPath(int index)
        {
            return Path.Combine(Application.persistentDataPath, $"profile_{index}.dat");
        }

        public void AddNewProfile(string name)
        {
            PlayerProfile playerProfile = new PlayerProfile();
            playerProfile.name = name;
            playerProfile.dataPath = GetDataPath(profileList.GetNextCounter());

            profileList.list.Add(playerProfile);
            profileList.lastProfile = playerProfile;

            Save();
        }

        private void Load()
        {
            profileList = new ProfileList();

            if (File.Exists(listDataPath))
            {
                string str = File.ReadAllText(listDataPath);
                profileList = JsonUtility.FromJson<ProfileList>(str);
            }
        }

        private void Save()
        {
            string str = JsonUtility.ToJson(profileList);
            File.WriteAllText(listDataPath, str);
        }
    }
}
