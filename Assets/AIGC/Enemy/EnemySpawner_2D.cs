using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_2D : MonoBehaviour
{
    [Header("=== 2D生成核心设置 ===")]
    [Tooltip("敌人预设")]
    public GameObject enemyPrefab;
    public GameObject boss; // Boss预设

    [Tooltip("生成范围的中心点")]
    public Transform spawnCenter;

    [Tooltip("2D圆形生成范围的半径")]
    public float spawnRadius = 8f;

    [Tooltip("单次生成的敌人数量")]
    public int spawnCountPerWave = 3;

    [Tooltip("生成间隔（秒）")]
    public float spawnInterval = 5f;

    [Header("当前生成波次")]
    public int currentWave = 0;

    [Header("=== Boss生成设置 ===")]
    [Tooltip("每多少波生成一次Boss")]
    public int bossWaveInterval = 10; 

    [Header("=== 2D安全生成设置 ===")]
    [Tooltip("生成点与玩家的最小安全距离")]
    public float minDistanceFromPlayer = 2f;

    [Tooltip("生成点Y轴偏移（适配2D场景地面高度，可根据场景调整）")]
    public float yOffset = 0f;

    [Header("=== 对象池设置 ===")]
    [Tooltip("是否启用对象池（推荐开启，优化性能）")]
    public bool useObjectPool = true;

    [Tooltip("对象池最大容量（避免无限生成）")]
    public int maxPoolSize = 20;

    // 内部计时器与对象池
    private float _spawnTimer;
    private Queue<GameObject> _enemyPool = new Queue<GameObject>();


    private void Start()
    {
        // 开局校验参数
        if (enemyPrefab == null)
        {
            Debug.LogError("【EnemySpawner_2D】请绑定敌人预设！");
            enabled = false;
            return;
        }
        if (boss == null)
        {
            Debug.LogWarning("【EnemySpawner_2D】未绑定Boss预设，将不会生成Boss！");
        }
        if (spawnCenter == null)
        {
            // 自动绑定玩家（Tag设为Player）
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) spawnCenter = player.transform;
            else
            {
                Debug.LogError("【EnemySpawner_2D】请绑定生成中心点！");
                enabled = false;
                return;
            }
        }

        // 开局一次性生成
        if (spawnInterval <= 0)
        {
            SpawnEnemyWave(spawnCountPerWave);
        }
    }


    private void Update()
    {
        // 按间隔自动生成
        if (spawnInterval > 0)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnInterval)
            {
                _spawnTimer = 0;
                currentWave++;
                // 判断是否为Boss波
                if (currentWave % bossWaveInterval == 0 && boss != null)
                {
                    SpawnBoss(); // 生成Boss
                }
                else
                {
                    SpawnEnemyWave(spawnCountPerWave); // 生成普通小怪
                }
            }
        }
    }

    /// <summary>
    /// 生成Boss（单独方法）
    /// </summary>
    public void SpawnBoss()
    {
        Debug.Log($"第 {currentWave} 波：Boss 生成！");
        Vector2 spawnPos = GetValid2DSpawnPosition();
        GameObject spawnedBoss = Instantiate(boss, spawnPos + new Vector2(0, yOffset), Quaternion.identity);
        spawnedBoss.SetActive(true);
    }

    /// <summary>
    /// 生成一波敌人（核心方法）
    /// </summary>
    public void SpawnEnemyWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // 1. 获取符合要求的2D随机生成点
            Vector2 spawnPos = GetValid2DSpawnPosition();

            // 2. 从对象池获取/实例化敌人
            GameObject enemy = GetEnemyFromPool();
            if (enemy == null)
            {
                enemy = Instantiate(enemyPrefab);
            }

            // 3. 重置位置与状态
            enemy.transform.position = spawnPos + new Vector2(0, yOffset);
            enemy.SetActive(true);

            // 4. 重置敌人状态（适配你现有的敌人组件）
            ResetEnemyState(enemy);
        }
    }


    /// <summary>
    /// 2D场景专用：获取安全的随机生成点
    /// 保证在圆形范围内，且不刷在玩家脸上
    /// </summary>
    private Vector2 GetValid2DSpawnPosition()
    {
        Vector2 centerPos = spawnCenter.position;
        Vector2 randomPos;
        float distance;
        int retryCount = 0;

        // 最多重试10次，避免死循环
        do
        {
            // 2D圆形随机坐标（极坐标转直角坐标，保证均匀分布）
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);
            float randomRadius = Random.Range(minDistanceFromPlayer, spawnRadius);
            randomPos = centerPos + new Vector2(
                Mathf.Cos(randomAngle) * randomRadius,
                Mathf.Sin(randomAngle) * randomRadius
            );

            distance = Vector2.Distance(randomPos, centerPos);
            retryCount++;
        }
        while (distance < minDistanceFromPlayer && retryCount < 10);

        return randomPos;
    }


    /// <summary>
    /// 从对象池获取敌人（2D优化）
    /// </summary>
    private GameObject GetEnemyFromPool()
    {
        if (!useObjectPool || _enemyPool.Count == 0) return null;

        GameObject enemy = _enemyPool.Dequeue();
        return enemy;
    }


    /// <summary>
    /// 敌人死亡后回收到对象池（在敌人死亡逻辑中调用）
    /// </summary>
    public void ReturnEnemyToPool(GameObject enemy)
    {
        if (!useObjectPool || _enemyPool.Count >= maxPoolSize)
        {
            Destroy(enemy);
            return;
        }

        enemy.SetActive(false);
        _enemyPool.Enqueue(enemy);
    }


    /// <summary>
    /// 重置敌人状态（完美适配你截图中的敌人组件）
    /// </summary>
    private void ResetEnemyState(GameObject enemy)
    {
        // 1. 重置血量（适配Enemy_Hp脚本）
        Enemy_Hp hp = enemy.GetComponent<Enemy_Hp>();
        if (hp != null)
        {
            hp.ResetHp();
        }

        // 2. 重置移动状态（适配Enemy_Movement脚本）
        Enemy_Movement move = enemy.GetComponent<Enemy_Movement>();
        if (move != null)
        {
            move.ResetMovement();
        }

        // 3. 重置战斗状态（适配Enemy_Combat脚本）
        Enemy_Combat combat = enemy.GetComponent<Enemy_Combat>();
        if (combat != null)
        {
            combat.ResetCombatState();
        }

        // 4. 重置击退状态（适配Enemy_Konckback脚本）
        Enemy_Konckback knockback = enemy.GetComponent<Enemy_Konckback>();
        if (knockback != null)
        {
            knockback.ResetKnockback();
        }

        // 5. 重置动画
        Animator anim = enemy.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }
    }


    /// <summary>
    /// Scene窗口可视化生成范围（2D圆形）
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (spawnCenter == null) return;

        // 绘制生成范围（红色大圈）
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnCenter.position, spawnRadius);

        // 绘制安全距离（绿色小圈）
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnCenter.position, minDistanceFromPlayer);
    }
}