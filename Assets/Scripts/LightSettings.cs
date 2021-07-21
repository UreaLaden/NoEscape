using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightSettings : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private PostProcessProfile[] _postProcessProfiles;
    [SerializeField] private GameObject nightVisionUI;
    private bool _nightVisionActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _postProcessVolume.profile = _nightVisionActive ? _postProcessProfiles[1] : _postProcessProfiles[0];
        nightVisionUI.SetActive(_nightVisionActive);
        if (Input.GetKeyUp(KeyCode.N))
        {
            _nightVisionActive = !_nightVisionActive;
        }
    }
}
