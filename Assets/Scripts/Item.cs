using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        APPLE,AMMO,BATTERY,KNIFE,CROSSBOW,HANDGUN,AXE,BAT,BOLT, CABIN_KEY,HOUSE_KEY,ROOM_KEY
    }
    public ItemType selectedItem;
    
}
