using UnityEngine;

public class SpawnWave : MonoBehaviour {

    float WaveCooldown = 0.5f;
    float WaveCooldownRemaining = 0f;

    [System.Serializable]
    public class Wave
    {
        public GameObject EnemyPrefab;
        public int NumberInGroup = 1;

        [System.NonSerialized]
        public int Spawned = 0;
    }

    public GameObject Path;
    public GameController gameController;
    public Wave[] waves;

    void Update()
    {
        WaveCooldownRemaining -= Time.deltaTime;
        if(WaveCooldownRemaining <= 0)
        {
            WaveCooldownRemaining = WaveCooldown;
            bool didSpawn = false;
            foreach (var wave in waves)
            {
                if(wave.Spawned < wave.NumberInGroup)
                {
                    Spawn(wave.EnemyPrefab, Path);
                    wave.Spawned++;
                    didSpawn = true;
                    break;
                }
            }

            if(!didSpawn)
            {
                Destroy(gameObject);
            }
        }
    }

    void Spawn(GameObject go, GameObject path)
    {
        GameObject e = (GameObject)Instantiate(go, transform.position, transform.rotation);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.gc = gameController;
        enemy.Path = path;
    }
}
