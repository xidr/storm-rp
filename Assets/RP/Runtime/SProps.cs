using UnityEngine;
using UnityEngine.Rendering;

public class SProps {
    public static class Common {
        public static ShaderTagId StencilPrePassId = new ShaderTagId("StencilPrePass");

        public static int ColorFiller = Shader.PropertyToID("_CameraColorAttachment");
        public static int DepthBuffer = Shader.PropertyToID("_CameraDepthAttachment");
        public static int DepthBufferAux = Shader.PropertyToID("_DepthBufferAux");

        public static int ScreenSize = Shader.PropertyToID("_ScreenSize");
        public static int Matrix_P = Shader.PropertyToID("_Matrix_P");
        public static int Matrix_I_P = Shader.PropertyToID("_Matrix_I_P");
        public static int Matrix_V = Shader.PropertyToID("_Matrix_V");
        public static int Matrix_I_V = Shader.PropertyToID("_Matrix_I_V");
        public static int WorldSpaceCameraPos = Shader.PropertyToID("_WorldSpaceCameraPos");
        public static int NearFarPlanes = Shader.PropertyToID("_NearFarPlanes");
    }
}