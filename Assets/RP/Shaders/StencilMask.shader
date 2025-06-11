Shader "DopeRP/Shaders/StencilMask"
{
    Properties
    {
        [InRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }

    SubShader
    {
		
		
        Pass {
			
            Tags { "RenderType"="Opaque" "LightMode"="StencilPrePass"}

            Blend Zero One
            ZWrite off
            Cull Back
			
            Stencil
            {
                ref [_StencilID]
                Comp Always
                Pass Replace
            }
            
//            HLSLPROGRAM

            
//			ENDHLSL
            
        }

    }
}