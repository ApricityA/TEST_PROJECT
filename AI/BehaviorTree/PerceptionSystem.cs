using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 感知系统，检测敌人是否在指定范围内
/// </summary>
public class PerceptionSystem
{
    private Soldier soldier; // 关联的士兵
    private float detectionRadius = 10f; // 检测半径

    public PerceptionSystem(Soldier soldier)
    {
        this.soldier = soldier;
    }

    // 检测范围内的敌人（具体实现可能用到Physics.OverlapSphere）
    // 优化：使用非分配内存的物理检测避免GC
    public List<Enemy> DetectEnemies(Vector3 center) {
        Collider[] hits = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(
            center, 
            detectionRadius, 
            hits, 
            LayerMask.GetMask("Enemy")
        );
        
        var enemies = new List<Enemy>();
        for (int i = 0; i < count; i++) {
            if (hits[i].TryGetComponent<Enemy>(out var enemy)) {
                enemies.Add(enemy);
            }
        }
        return enemies;
    }
}

// 敌人类
public class Enemy : MonoBehaviour
{
    public bool IsAlive => gameObject.activeSelf;
    public Vector3 Position => transform.position;
}

// 2. 目标锁定系统（核心逻辑）
public class TargetingSystem {
    private Enemy _currentTarget; // 当前锁定目标
    private float _maxLockDistance = 15f; // 最大锁定距离（防目标跑远后仍追击）
    private Soldier soldier;
    
    // 选择最近目标（无目标或目标死亡时调用）
    public void AcquireNewTarget(Vector3 soldierPos, List<Enemy> enemies) {
        if (enemies.Count == 0) {
            _currentTarget = null;
            return;
        }
        
        // 找到最近存活敌人
        _currentTarget = enemies
            .Where(e => e.IsAlive)
            .OrderBy(e => Vector3.Distance(soldierPos, e.Position))
            .FirstOrDefault();
    }
    
    // 外部调用：判断是否需要切换目标（目标死亡或超出锁定距离）
    public bool ShouldRefreshTarget() {
        return _currentTarget == null || 
               !_currentTarget.IsAlive || 
               Vector3.Distance(_currentTarget.Position, soldier.transform.position) > _maxLockDistance;
    }
    
    public Enemy CurrentTarget => _currentTarget;
}