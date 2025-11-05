// ScriptableObject 配置（Unity示例）
[CreateAssetMenu(fileName = "SoldierConfig", menuName = "AI/SoldierConfig")]
public class SoldierConfig : ScriptableObject {
    public float DetectionRadius = 5f;    // 敌兵检测范围
    public float AttackRange = 1.5f;      // 攻击距离
    public int BaseDamage = 10;            // 伤害值
}