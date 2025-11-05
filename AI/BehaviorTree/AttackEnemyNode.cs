public class AttackEnemyNode : AIBTNode
{
    private Soldier soldier; // 士兵
    private Enemy enemy; // 敌人

    public AttackEnemyNode(Soldier soldier, Enemy enemy)
    {
        this.soldier = soldier;
        this.enemy = enemy;
    }

    public override BTState Execute()
    {
        if (soldier.CanAttack(enemy))
        {
            // 攻击逻辑
            soldier.Attack(enemy);
            return BTState.BT_RUNNING;
        }
        return BTState.BT_SUCCESS;
        
    }
}