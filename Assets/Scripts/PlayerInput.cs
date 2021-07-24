using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    public bool isAiming;
    public bool weaponEquipped;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Destroy(this);
    }
    void Update()
    {
        isAiming = weaponEquipped && Input.GetMouseButtonDown(1);
    }
}
