float3 rayOrigin = viewDir - worldPos; // setup ray origin
float3 rayStep = viewDir * 0.1; // adjust the step size if needed

float3 cubeCenter = float3(0, 0, 0); // cube center position
float cubeSize = 1.0; // cube half-side length
float3 cubeColor = float3(1, 0, 0); // cube color (red in this case)

float3 lightDirection = normalize(lightPos - worldPos); // setup light

for (int i = 0; i < 256; i++)
{
    float3 cubeToRay = abs(cubeCenter - rayOrigin) - cubeSize; // calculate distance to cube

    if (dot(cubeToRay, cubeToRay) < 0.1) // adjust the threshold value if needed
    {
        float3 normal = normalize(cubeToRay); // calculate surface normal
        float diffuse = max(dot(normal, lightDirection), 0); // calculate shading
        return diffuse * cubeColor; // apply shading and return the cube color
    }

    rayOrigin += rayStep; // move the ray along its direction
}

return float3(0, 0, 0); // return black if no intersection
