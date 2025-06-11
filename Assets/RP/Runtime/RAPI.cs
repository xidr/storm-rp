using UnityEngine;
using UnityEngine.Rendering;

public static class RAPI {
    const string BUFFER_NAME = "DefaultBufferName";

    public static CommandBuffer Buffer { get; set; } = new() { name = BUFFER_NAME };
    public static ScriptableRenderContext Context { get; set; }
    public static Camera CurCamera { get; set; }
    public static Vector2Int bufferSize;

    public static CullingResults CullingResults { get;  set; }

    public static bool useGPUInstancing;
    public static bool useDynamicBatching;
    public static bool useHDR;
    
    public static void ExecuteBuffer () {
        Context.ExecuteCommandBuffer(Buffer);
        Buffer.Clear();
    }
}