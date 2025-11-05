using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Soldier soldier; // 士兵
    private Enemy enemy;
    private PerceptionSystem perceptionSystem; // 感知系统
    private TargetingSystem targetingSystem;
    private RootNode behaviorTreeRoot;  // 行为树的根节点

    public SoldierConfig soldierConfig;

    public void Awake()
    {
        soldier = GetComponent<Soldier>();
    }
    
    void Start()
    {
        targetingSystem = new TargetingSystem();
        perceptionSystem = new PerceptionSystem(soldier);
        BehaviorTree();
    }

    void Update()
    {
        // 更新目标
        List<Enemy> enemies = perceptionSystem.DetectEnemies(transform.position);
        if (targetingSystem.ShouldRefreshTarget() || enemies.Count > 0)
        {
            targetingSystem.AcquireNewTarget(transform.position, enemies);
        }
        
        enemy = targetingSystem.CurrentTarget;
        behaviorTreeRoot?.Execute();
    }

    private void BehaviorTree()
    {
        // 创建叶子节点（关联当前AI的soldier实例）
        AIBTNode checkEnemyInRange = new CheckEnemyInRangeNode(perceptionSystem, soldier);
        AIBTNode attackEnemy = new AttackEnemyNode(soldier, enemy);

        AIBTNode sequence = new SequenceNode();
        ((SequenceNode)sequence).children.Add(checkEnemyInRange);
        ((SequenceNode)sequence).children.Add(attackEnemy);

        AIBTNode selector = new SelectorNode();
        ((SelectorNode)selector).children.Add(sequence);

        // 创建行为树
        behaviorTreeRoot = new RootNode();
        behaviorTreeRoot.child = selector;
    }
}