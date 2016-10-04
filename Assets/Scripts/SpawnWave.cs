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

    LevelController Path;
    GameController GameController;
    public Wave[] Waves;

    void Start()
    {
        Path = FindObjectOfType<LevelController>();
        GameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        WaveCooldownRemaining -= Time.deltaTime;
        if(WaveCooldownRemaining <= 0)
        {
            WaveCooldownRemaining = WaveCooldown;
            bool didSpawn = false;
            foreach (var wave in Waves)
            {
                if(wave.Spawned < wave.NumberInGroup)
                {
                    SpawnEnemy(wave.EnemyPrefab, Path);
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

    void SpawnEnemy(GameObject go, LevelController path)
    {
        GameObject e = (GameObject)Instantiate(go, transform.position, transform.rotation);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.rb = e.GetComponent<Rigidbody>();
        enemy.gc = GameController;
        enemy.Path = path;
    }
}
