using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    bool useGPUInstancing, useLightsPerObject;
    bool allowHDR;
    ShadowSettings shadowSettings;
    PostFXSettings postFXSettings;
    
    int colorLUTResolution;

    public CustomRenderPipeline(bool allowHDR, bool useGPUInstancing, bool useSRPBatcher,
        bool useLightsPerObject, ShadowSettings shadowSettings,
        PostFXSettings postFXSettings, int colorLUTResolution)
    {
        this.colorLUTResolution = colorLUTResolution;
        this.allowHDR = allowHDR;
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        GraphicsSettings.lightsUseLinearIntensity = true;
        this.shadowSettings = shadowSettings;
        this.useLightsPerObject = useLightsPerObject;
        this.postFXSettings = postFXSettings;
        
        InitializeForEditor();
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
    }

    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            renderer.Render(context, cameras[i], allowHDR, useGPUInstancing,
                useLightsPerObject, shadowSettings, postFXSettings, colorLUTResolution);
        }
    }
}