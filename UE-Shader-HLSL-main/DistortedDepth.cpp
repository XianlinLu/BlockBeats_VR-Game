//uv, cameraDir, texObject, depth, slices, speed, frequency, time

////////distorting texture///////////////////
float3 rayStep = cameraDir * -1;
float4 inputTex = Texture2DSample(texObject, texObjectSampler, uv);

struct texDistort
{
	float2 texScale(float2 uv, float2 scale)
	{
		float2 texScale = (uv- 0.5) * scale + 0.5;
		return texScale;
	}

	float2 texRotate(float2 uv, float angle)
	{
		float2x2 rotationMatrix = float2x2(cos(angle), sin(angle),
										  -sin(angle), cos(angle));
		return mul(uv - 0.5,rotationMatrix ) + 0.5 ;
	}

	float2 texDistortion(float2 uv, float time, float speed, float frequency)
	{
		float angle = atan2(uv.y - 0.5, uv.x - 0.5);
		float radius = length(uv - 0.5);

		float distortion = speed * sin(3 * radius + frequency * time);
		float primDist = sin(6.0 * angle) * distortion;

		return texRotate(uv, primDist);
	}
};

texDistort txd;

////////////////// add depth ///////////////////////

for (int i = 0; i < depth ; i++)
{
	inputTex = Texture2DSample(texObject, texObjectSampler, txd.texDistortion(uv, time, speed, frequency));

	if (inputTex.r > 0.1 && inputTex.g > 0.1 && inputTex.b > 0.1)
	{
		return float3(0,0,i );
	}

	uv += rayStep * slices ;
  inputTex = Texture2DSample(texObject, texObjectSampler, uv.xy);

}

return inputTex;
