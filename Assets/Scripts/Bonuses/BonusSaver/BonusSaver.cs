using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Oliasca.Maze
{
    public sealed class BonusSaver
    {
        private const string fileName = "BonusesData.dat";
        private readonly string path;

        public BonusSaver()
        {
            path = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Save(BonusSaveList bonusSaveList)
        {
            var str = JsonUtility.ToJson(bonusSaveList);
            File.WriteAllText(path, Crypto.CryptoXOR(str));
        }

        public BonusSaveList Load()
        {
            if (!File.Exists(path)) return new BonusSaveList();

            string str = File.ReadAllText(path);
            return JsonUtility.FromJson<BonusSaveList>(Crypto.CryptoXOR(str));
        }
    }
}
