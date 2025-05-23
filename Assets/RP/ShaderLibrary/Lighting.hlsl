#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

bool RenderingLayersOverlap (Surface surface, Light light) {
    return (surface.renderingLayerMask & light.renderingLayerMask) != 0;
}

float3 IncomingLight (Surface surface, Light light) {
    return saturate(dot(surface.normal, light.direction)) * light.color * light.attenuation;
}

float3 GetLighting (Surface surface, BRDF brdf, Light light) {
    return IncomingLight(surface, light) * DirectBRDF(surface, brdf, light);;
}

float3 GetLighting (Surface surfaceWS, BRDF brdf, GI gi) {
    ShadowData shadowData = GetShadowData(surfaceWS);
    shadowData.shadowMask = gi.shadowMask;
    // return gi.shadowMask.shadows.rgb;
    
    float3 color = IndirectBRDF(surfaceWS, brdf, gi.diffuse, gi.specular);
    for (int i = 0; i < GetDirectionalLightCount(); i++) {
        Light light = GetDirectionalLight(i, surfaceWS, shadowData);
        if (RenderingLayersOverlap(surfaceWS, light)) {
            color += GetLighting(surfaceWS, brdf, light);
        }
        // color = light.attenuation;
    }

    
    #if defined(_LIGHTS_PER_OBJECT)
    for (int j = 0; j < min(unity_LightData.y, 8); j++) {
        int lightIndex = unity_LightIndices[(uint)j / 4][(uint)j % 4];
        Light light = GetOtherLight(lightIndex, surfaceWS, shadowData);
        if (RenderingLayersOverlap(surfaceWS, light)) {
            color += GetLighting(surfaceWS, brdf, light);
        }
    }
    #else
    for (int j = 0; j < GetOtherLightCount(); j++) {
        Light light = GetOtherLight(j, surfaceWS, shadowData);
        if (RenderingLayersOverlap(surfaceWS, light)) {
            color += GetLighting(surfaceWS, brdf, light);
        }
    }
    #endif
    // return shadowData.strength;
    return color;
}

#endif

