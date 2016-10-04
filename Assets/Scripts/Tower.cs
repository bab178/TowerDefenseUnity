using UnityEngine;

public class Tower : MonoBehaviour {

    public Transform turretTransform;
    public GameObject BulletPrefab;
    public int Price = 5;

    public float range;
    float fireCooldown = 0.5f;
    float fireCooldownRemaining = 0f;

	void Update () {
        // FIXME: Make enemy manager to give enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        Enemy nearest = null;
        float dist = Mathf.Infinity;

        foreach(var e in enemies)
        {
            var d = Vector3.Distance(transform.position, e.transform.position);
            if(nearest == null || d < dist)
            {
                nearest = e;
                dist = d;
            }
        }
        
        if(nearest == null)
        {
            return;
        }

        Vector3 direction = nearest.transform.position - transform.position;
        turretTransform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(direction).eulerAngles.y, 0);

        fireCooldownRemaining -= Time.deltaTime;
        if(fireCooldownRemaining <= 0 && direction.magnitude <= range)
        {
            fireCooldownRemaining = fireCooldown;
            GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, transform.position, transform.rotation);
            Bullet b = bulletGO.GetComponent<Bullet>();
            b.Target = nearest.transform;
        }
    }
}
