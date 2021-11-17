using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
    public UnityEvent<DamageInfo> OnDamageApplied = new UnityEvent<DamageInfo>();

    public void ApplyDamage(DamageInfo damageInfo)
    {
        Debug.Log($"{name} got dealt {damageInfo.DamageAmount} damage by {damageInfo.Dealer.name}!");
        OnDamageApplied.Invoke(damageInfo);
    }
}
