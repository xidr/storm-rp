using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "RP/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    bool useGPUInstancing = true, useSRPBatcher = true;
    
    [SerializeField]
    ShadowSettings shadows = default;
    
    protected override RenderPipeline CreatePipeline () {
        return new CustomRenderPipeline(useGPUInstancing, useSRPBatcher, shadows);
    }
}