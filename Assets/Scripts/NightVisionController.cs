using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightVisionController : MonoBehaviour
{
    [SerializeField] private Color DefaultAmbientColor;
    [SerializeField] private Color BoostedAmbientColor;
    
    private bool _isNightVisionEnabled;

    private PostProcessVolume _postProcessVolume;
    private void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
        
        _isNightVisionEnabled = false;
        _postProcessVolume.weight = 0;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchNightVisionMode();
        }
    }
    
    private void SwitchNightVisionMode()
    {
        _isNightVisionEnabled = !_isNightVisionEnabled;
        _postProcessVolume.weight = _isNightVisionEnabled ? 1 : 0;
        RenderSettings.ambientLight = _isNightVisionEnabled ? BoostedAmbientColor : DefaultAmbientColor;
    }
}
