using UnityEngine;

public class Soldier : MonoBehaviour
{
    public bool CanAttack(Enemy enemy)
    {
        // 攻击逻辑
        if (enemy == null) return false;
        return Vector3.Distance(transform.position, enemy.Position) <= 1.5f;
    }

    public void Attack(Enemy enemy)
    {
        // 攻击逻辑
        Debug.Log("攻击敌人");
    }

    public void MoveTo(Vector3 targetPosition)
    {
        // 移动逻辑
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}