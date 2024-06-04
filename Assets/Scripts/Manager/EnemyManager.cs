using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        Dictionary<string, string> LevelData = GameConfigManager.Instance.GetLevelById(id);
        
        string[] enemyIds = LevelData["EnemyIds"].Split('=');
        string[] enemyPos = LevelData["Pos"].Split('=');
        
        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);

            GameObject prefab = Resources.Load<GameObject>(enemyData["Model"]);
            GameObject obj = Object.Instantiate(prefab, new Vector3(x, y, z), prefab.transform.rotation);
            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);
        }
    }

    //敌人死亡后移除对应敌人实体
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        //后续做清场判定
    }
}
