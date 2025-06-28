float3 rayStep = cameraDir * -1;
float4 inputTex = Texture2DSample(texObject, texObjectSampler, uv);

for (int i = 0 ; i < depth ; i ++)
{
  if(inputTex.r > 0.1 && inputTex.g > 0.1 && inputTex.b > 0.1)
  {
    return float3(0,0,i  ); //brighter and brighter red: gradient
  }

  uv += rayStep * slices; //distance between the slices

  inputTex = Texture2DSample(texObject, texObjectSampler, uv.xy);
}

return inputTex;
