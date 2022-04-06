using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> enemyPool;
    public GameObject enemiesToPool;
    public int amountOfEnemies;
    [SerializeField]
    Transform enemyParent;
    [SerializeField]
    bool createNewWhenPoolEmpty = false;

    public List<GameObject> projectilesPool;
    public GameObject projectilesToPool;
    public int amountOfProjectiles;
    [SerializeField]
    Transform projectileParent;

    void Awake()
    {
        SharedInstance = this;
    }

     void Start()
    {
        enemyPool = new List<GameObject>();
        GameObject tmp;
        for(int i=0; i< amountOfEnemies; i++)
        {
            tmp = Instantiate(enemiesToPool);
            tmp.transform.parent = enemyParent;
            tmp.SetActive(false);
            enemyPool.Add(tmp);
        }

        projectilesPool = new List<GameObject>();
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            tmp = Instantiate(projectilesToPool);
            tmp.transform.parent = projectileParent;
            tmp.SetActive(false);
            projectilesPool.Add(tmp);
        }

    }

    public GameObject GetEnemyFromPool()
    {
        for (int i = 0;  i < amountOfEnemies; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].SetActive(true);
                return enemyPool[i];
            }
        }
        if (!createNewWhenPoolEmpty) return null;
        else
        {
            GameObject tmp;
            tmp = Instantiate(enemiesToPool);
            tmp.transform.parent = enemyParent;
            tmp.SetActive(false);
            enemyPool.Add(tmp);
            tmp.SetActive(true);
            return tmp;
        }

    }

    public GameObject GetProjectileFromPool()
    {
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            if (!projectilesPool[i].activeInHierarchy)
            {
                projectilesPool[i].SetActive(true);
                return projectilesPool[i];
            }
        }
        return null;
    }

}
