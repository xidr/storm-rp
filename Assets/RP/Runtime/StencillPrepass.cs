using UnityEngine;
using UnityEngine.Rendering;

public class StencillPrepass {
    private const string BUFFER_NAME = "StencilPrePass";

    public void Render() {
        // RAPI.BeginSample(BUFFER_NAME);

        var cameraWidth = RAPI.CurCamera.pixelWidth;
        var cameraHeight = RAPI.CurCamera.pixelHeight;

        RAPI.Buffer.GetTemporaryRT(SProps.Common.ColorFiller, RAPI.bufferSize.x, RAPI.bufferSize.y,0, FilterMode.Bilinear, RAPI.useHDR ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default);
        RAPI.Buffer.GetTemporaryRT(SProps.Common.DepthBuffer, RAPI.bufferSize.x, RAPI.bufferSize.y,32, FilterMode.Point, RenderTextureFormat.Depth);

        RenderTargetIdentifier[] colorTargets = { new RenderTargetIdentifier(SProps.Common.ColorFiller), };

        RAPI.Buffer.SetRenderTarget(colorTargets, SProps.Common.DepthBuffer);
        RAPI.Buffer.ClearRenderTarget(true, true, Color.clear);

        RAPI.ExecuteBuffer();

        var sortingSettings = new SortingSettings(RAPI.CurCamera) { criteria = SortingCriteria.CommonTransparent };

        var drawingSettings = new DrawingSettings(SProps.Common.StencilPrePassId, sortingSettings) {
            enableDynamicBatching = RAPI.useDynamicBatching,
            enableInstancing = RAPI.useGPUInstancing,
        };

        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

        RAPI.Context.DrawRenderers(RAPI.CullingResults, ref drawingSettings, ref filteringSettings);

        RAPI.ExecuteBuffer();

        // RAPI.EndSample(BUFFER_NAME);
    }
}