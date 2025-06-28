// 2DCircle = length(pos-uv);
// 3DSphere = length(rayOrigin - sphereCenter) - sphereRadius;

float3 rayOrigin = 1- (cameraDir - worldPos); //calculates the starting point of the ray that originates from the camera's position and moves in the direction of the current pixel
float3 rayStep = cameraDir * -1; // set the step size along the ray direction to the direction of the camera view, which means the ray will move one unit at a time in that direction

float3 lightDirection = normalized(lightPos); //calculates the normalized direction vector from the current pixel towards the light source

for (int i = 0 ; i < 256 ; i++)
{
  float dist = length(rayOrigin - sphereCenter) - sphereRadius; //draw 3d sphere
  
  if (dist < 0.0.1) 
    {
        float3 normal = normalize(rayOrigin - sphereCenter);
        float diffuse = max(dot(normal, lightDirection),0); //calculate shadow area
        float3 reflection = reflect(lightDirection, normal); //calculate highlight
        float3 viewDirection = normalize(-worldPos - rayOrigin);
        float3 specular = pow(max(dot(reflection, viewDirection),0), 16);
        
        return (diffuse * sphereColor) + (specular * highlightColor) ; // if inside this sphere, return color red, based on diffuse (shadow)
    }
  opacityMask = 1;
  rayOrigin += rayStep;
}
opacityMask = 0; //make the outside transparent
return float3(0,0,0); //othereise, return black
