using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager current;
    public GameObject EnemyPrefab;
    private List<Enemy> _enemies;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(this);
            return;
        }

        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (EnemyPrefab == null)
        {
            throw new System.NullReferenceException("EnemyPrefeb cannot be null");
        }
        _enemies = new List<Enemy>();
        SpawnDebugEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnDebugEnemies()
    {
        //5, 7
        var go = GameObject.Instantiate(EnemyPrefab, transform);
        go.transform.position = new Vector3(5f, 0f, 7f);
        _enemies.Add(go.GetComponent<Enemy>());
    }

    public void HandlePlayerDash(Vector3 position, Vector3 desiredPosition)
    {
        // todo this wont work if you move diagonally.
        var enemies = GetEnemiesBetweenPoints(position.x, position.z, desiredPosition.z);
        if (enemies.Count == 0) return;

        foreach (Enemy e in enemies)
        {
            Debug.Log("Hit Enemy");
            e.HandleHit();
        }
    }

    public List<Enemy> GetEnemiesBetweenPoints(float x, float z1, float z2)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (Enemy e in _enemies)
        {
            Vector3 pos = e.transform.position;
            if ((int)pos.x != (int)x) continue;
            if (!LiesBetweenPoints(pos.z, z1, z2)) continue;
            enemies.Add(e);
        }

        return enemies;
    }

    private bool LiesBetweenPoints(float p, float bound1, float bound2)
    {
        return (p > bound1 && p < bound2) || (p > bound2 && p < bound1);
    }
}
