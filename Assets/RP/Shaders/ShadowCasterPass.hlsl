#ifndef CUSTOM_SHADOW_CASTER_PASS_INCLUDED
#define CUSTOM_SHADOW_CASTER_PASS_INCLUDED


bool _ShadowPancaking;

struct Attributes {
    float3 positionOS : POSITION;
    float2 baseUV : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings {
    float4 positionCS_SS : SV_POSITION;
    float3 positionWS : VAR_POSITION;
    float2 baseUV : VAR_BASE_UV;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

Varyings ShadowCasterPassVertex (Attributes input) {
    Varyings output;
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    output.positionWS = TransformObjectToWorld(input.positionOS);
    output.positionCS_SS = TransformWorldToHClip(output.positionWS);

    if (_ShadowPancaking)
    {
        #if UNITY_REVERSED_Z
        output.positionCS.z =
            min(output.positionCS.z, output.positionCS.w * UNITY_NEAR_CLIP_VALUE);
        #else
        output.positionCS_SS.z =
            max(output.positionCS_SS.z, output.positionCS_SS.w * UNITY_NEAR_CLIP_VALUE);
        #endif
    }
        
    output.baseUV = TransformBaseUV(input.baseUV);
    return output;
}

void ShadowCasterPassFragment (Varyings input) {
    UNITY_SETUP_INSTANCE_ID(input);

    InputConfig config = GetInputConfig(input.positionCS_SS, input.baseUV);
    ClipLOD(config.fragment, unity_LODFade.x);
    
    float4 base = GetBase(config);
    #if defined(_SHADOWS_CLIP)
    clip(base.a - GetCutoff(config));
    #elif defined(_SHADOWS_DITHER)
    float dither = InterleavedGradientNoise(input.positionCS.xy * 100, 0);
    clip(base.a - dither);
    #endif
}

#endif
