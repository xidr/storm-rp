using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "RP/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    bool useGPUInstancing = true, useSRPBatcher = true,
        useLightsPerObject = true;
    
    [SerializeField]
    bool allowHDR = true;
    
    [SerializeField]
    ShadowSettings shadows = default;
    
    [SerializeField]
    PostFXSettings postFXSettings = default;
    
    public enum ColorLUTResolution { _16 = 16, _32 = 32, _64 = 64 }

    [SerializeField]
    ColorLUTResolution colorLUTResolution = ColorLUTResolution._32;
    
    protected override RenderPipeline CreatePipeline () {
        return new CustomRenderPipeline(allowHDR, useGPUInstancing, useSRPBatcher, 
            useLightsPerObject, shadows, postFXSettings, (int)colorLUTResolution);
    }
}