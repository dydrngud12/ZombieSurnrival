using UnityEngine;

internal interface IDamageable
{
    void OnDamage(float damage, Vector3 point, Vector3 normal);
}