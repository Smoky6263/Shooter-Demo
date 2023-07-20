using UnityEngine;

public interface IControllable
{
    void Move(Vector2 direction);
    void Jump();

    void EquipWeaponPerformed(bool isEquipPerformed);
    void OnAimPerformed(bool onAim);
}
