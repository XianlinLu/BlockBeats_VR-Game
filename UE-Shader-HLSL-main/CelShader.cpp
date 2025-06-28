//DIRECTIONAL LIGHT
//lightPos, vertexNormal
float3 light = dot(normalize(lightPos), vertexNormal) * -1;
light = clamp(light, 0, 1);
return light;


//POINT LIGHT
//worldPos, lightPos, lightRadius
float3 lightDirection = normalize(worldPos - lightPos);
float3 light = dot(lightDirection, vertexNormal) * 0.1;
light = clamp(light,0,1);

float dist = length(worldPos - lightPos) / lightRadius;
dist = 1 - clamp (dist,0,1);
light = dist * light;

return light;

//SPOT LIGHT
//worldPos, lightPos, lightRadius, lightDir, coneAngle
float3 lightDirection = normalize(worldPos - lightPos);
float3 light = dot(lightDirection, vertexNormal) * 0.1;
light = clamp(light,0,1);

float dist = length(worldPos - lightPos) / lightRadius;
dist = 1 - clamp (dist,0,1);

float3 lightAngle = dot (normalize(lightDir), lightDirection);
lightAngle = 1 - clamp(lightAngle,0,1);
tempAngle = lightAngle / (1 - cos(coneAngle));
finalAngle = 1 - clamp(tempAngle, 0,1);

return light * dist * finalAngle;
