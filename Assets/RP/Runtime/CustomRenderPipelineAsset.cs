using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "RP/Custom Render Pipeline")]
public partial class CustomRenderPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    bool useGPUInstancing = true, useSRPBatcher = true,
        useLightsPerObject = true;
    
    //[SerializeField]
    //bool allowHDR = true;

    [SerializeField]
    CameraBufferSettings cameraBuffer = new CameraBufferSettings {
        allowHDR = true,
        renderScale = 1f
    };
    
    [SerializeField]
    ShadowSettings shadows = default;
    
    [SerializeField]
    PostFXSettings postFXSettings = default;
    
    public enum ColorLUTResolution { _16 = 16, _32 = 32, _64 = 64 }

    [SerializeField]
    ColorLUTResolution colorLUTResolution = ColorLUTResolution._32;
    
    [SerializeField]
    Shader cameraRendererShader = default;
    
    protected override RenderPipeline CreatePipeline () {
        return new CustomRenderPipeline(cameraBuffer, useGPUInstancing, useSRPBatcher, 
            useLightsPerObject, shadows, postFXSettings, (int)colorLUTResolution,
            cameraRendererShader);
    }
}