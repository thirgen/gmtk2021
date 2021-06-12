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

    void Start()
    {
        if (EnemyPrefab == null)
        {
            throw new System.NullReferenceException("EnemyPrefeb cannot be null");
        }
        _enemies = new List<Enemy>();
        SpawnDebugEnemies();
    }

    private void SpawnDebugEnemies()
    {
        var go = GameObject.Instantiate(EnemyPrefab, transform);
        go.transform.position = new Vector3(5f, 0f, 7f);
        _enemies.Add(go.GetComponent<Enemy>());
    }

    /// <summary>
    /// Checks to see if the player hits any enemies along a dash from <paramref name="position"/>
    /// to <paramref name="desiredPosition"/>, and tells the enemies they've been hit.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="desiredPosition"></param>
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


    /// <summary>
    /// Returns a list of all enemies between points <paramref name="z1"/> and <paramref name="z2"/>
    /// on x axis <paramref name="x"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z1"></param>
    /// <param name="z2"></param>
    /// <returns></returns>
    private List<Enemy> GetEnemiesBetweenPoints(float x, float z1, float z2)
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

    /// <summary>
    /// Returns true if <paramref name="p"/> lies between <paramref name="bound1"/> and <paramref name="bound2"/>.
    /// </summary>
    /// <returns></returns>
    private bool LiesBetweenPoints(float p, float bound1, float bound2)
    {
        return (p > bound1 && p < bound2) || (p > bound2 && p < bound1);
    }
}
