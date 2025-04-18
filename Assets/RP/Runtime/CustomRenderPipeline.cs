using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CustomRenderPipeline : RenderPipeline
{
    private CameraRenderer renderer;

    bool useGPUInstancing, useLightsPerObject;
    //bool allowHDR;
    CameraBufferSettings cameraBufferSettings;
    ShadowSettings shadowSettings;
    PostFXSettings postFXSettings;
    
    int colorLUTResolution;

    public CustomRenderPipeline(CameraBufferSettings cameraBufferSettings, bool useGPUInstancing, bool useSRPBatcher,
        bool useLightsPerObject, ShadowSettings shadowSettings,
        PostFXSettings postFXSettings, int colorLUTResolution, Shader cameraRendererShader)
    {
        this.colorLUTResolution = colorLUTResolution;
        //this.allowHDR = allowHDR;
        this.cameraBufferSettings = cameraBufferSettings;
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        GraphicsSettings.lightsUseLinearIntensity = true;
        this.shadowSettings = shadowSettings;
        this.useLightsPerObject = useLightsPerObject;
        this.postFXSettings = postFXSettings;
        
        InitializeForEditor();
        
        renderer = new CameraRenderer(cameraRendererShader);
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
    }

    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            renderer.Render(context, cameras[i], cameraBufferSettings, useGPUInstancing,
                useLightsPerObject, shadowSettings, postFXSettings, colorLUTResolution);
        }
    }
}