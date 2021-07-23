using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightSettings : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private PostProcessProfile[] _postProcessProfiles;
    [SerializeField] private GameObject nightVisionUI;
    [SerializeField] private GameObject flashLight;
    private bool _nightVisionActive = false;
    private bool _flashlightActive = false;
    private bool canDrainBattery = true;
  
    void Update()
    {
        canDrainBattery = GameManager.BatteryPower > 0f;
        _postProcessVolume.profile = _nightVisionActive && canDrainBattery ? _postProcessProfiles[1] : _postProcessProfiles[0]; 
        
        ToggleNightVision();
        ToggleFlashLight();
    }

    private void ToggleFlashLight()
    {
        flashLight.SetActive(_flashlightActive && canDrainBattery);
        if (Input.GetKeyUp(KeyCode.F))
        {
            _flashlightActive = !_flashlightActive;
            GameManager.FlashLightActive = !GameManager.FlashLightActive;
        }
    }

    private void ToggleNightVision()
    {
        nightVisionUI.SetActive(_nightVisionActive && canDrainBattery);
        if (Input.GetKeyUp(KeyCode.N))
        {
            _nightVisionActive = !_nightVisionActive;
            GameManager.NightVisionActive = !GameManager.NightVisionActive;
        }
    }
}
