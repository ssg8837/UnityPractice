 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);
        if(currentAmmoSlot != null)
        {
            return currentAmmoSlot.ammoAmount;
        }
        else
        {
            return 0;
        }
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot currentAmmoSlot = GetAmmoSlot(ammoType);
        if(currentAmmoSlot != null)
        {
            currentAmmoSlot.ammoAmount--;
        }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }

}
