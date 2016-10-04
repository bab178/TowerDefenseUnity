using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class Stats
    {
        public int Health = 1;
        public int Money = 10;
        public float Speed = 5f;

        public Stats(Stats stats)
        {
            Health = stats.Health;
            Money = stats.Money;
            Speed = stats.Speed;
        }

    }

    private int PathIndex = 0;
    private List<Transform> PathNodes;

    public Stats stats;
    public Rigidbody rb;
    public LevelController Path;
    public GameController gc;

    public void TakeDamage(int damage)
    {
        stats.Health -= damage;

        // Enemy dies
        if (stats.Health <= 0)
        {
            gc.EarnMoney(stats.Money);
            Destroy(gameObject);
        }
    }

    void Start() {
        stats = new Stats(stats);

        bool IsFirstChild = true;
        PathNodes = new List<Transform>(20);

        foreach (Transform child in Path.transform) {
            if (!IsFirstChild) {
                // Child is your child transform
                PathNodes.Add(child);
            }
            else {
                IsFirstChild = false;
            }
        }
    }

    void Update() {
        MoveEnemy();
    }

    void MoveEnemy() {
        if(PathNodes != null && PathNodes.Count > 0 && PathIndex <= PathNodes.Count-1) {
            var targetNodeTransform = PathNodes.ToArray()[PathIndex];

            Vector3 direction = targetNodeTransform.position - transform.localPosition;
            float distance = stats.Speed * Time.deltaTime;

            if( direction.magnitude > distance ) {
                transform.Translate(direction.normalized * distance, Space.World);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5);
            }
            else {
                PathIndex++;
            }
        }
        else {
            ReachedEnd();
        }
    }

    void ReachedEnd() {
        gc.TakeDamage();
        Destroy(gameObject);
    }
}
