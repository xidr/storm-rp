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
    
    protected override RenderPipeline CreatePipeline () {
        return new CustomRenderPipeline(allowHDR, useGPUInstancing, useSRPBatcher, 
            useLightsPerObject, shadows, postFXSettings);
    }
}