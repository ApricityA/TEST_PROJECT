using UnityEngine;

/// <summary>
/// 检测范围内的敌人,存在敌人则返回成功，否则返回失败
/// </summary>
public class CheckEnemyInRangeNode : AIBTNode
{
    private PerceptionSystem perceptionSystem;
    private Soldier soldier;

    public CheckEnemyInRangeNode(PerceptionSystem perceptionSystem, Soldier soldier)
    {
        this.perceptionSystem = perceptionSystem;
        this.soldier = soldier;
    }

    public override BTState Execute()
    {
        List<Enemy> enemies = perceptionSystem.DetectEnemies(soldier.transform.position);
        if (enemies.Count > 0)
        {
            return BTState.BT_SUCCESS;
        }
        else
        {
            return BTState.BT_FAILURE;
        }
    }
}