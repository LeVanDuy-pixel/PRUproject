using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class BulletSpawnEvent: MonoBehaviour
    {
        public List<float> spawnTime;
        public List<Transform[]> spawnPoints;
    }
    public class LoadData : MonoBehaviour
    {
        public static BulletSpawnEvent spawnData;
        public static string filePath;

        public static void Load()
        {
            string jsonData = File.ReadAllText(Application.dataPath + "/" + filePath);
            spawnData = JsonUtility.FromJson<BulletSpawnEvent>(jsonData);
        }
    }
}
