using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    bool useGPUInstancing, useLightsPerObject;
    ShadowSettings shadowSettings;

    public CustomRenderPipeline(bool useGPUInstancing, bool useSRPBatcher,
        bool useLightsPerObject, ShadowSettings shadowSettings)
    {
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        GraphicsSettings.lightsUseLinearIntensity = true;
        this.shadowSettings = shadowSettings;
        this.useLightsPerObject = useLightsPerObject;
        
        InitializeForEditor();
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
    }

    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            renderer.Render(context, cameras[i], useGPUInstancing,
                useLightsPerObject, shadowSettings);
        }
    }
}