using UnityEngine;

public interface  IAttackPattern
{
    void ExecuteAttack(EnemyController enemy);

    void OnAttackStart();
}
