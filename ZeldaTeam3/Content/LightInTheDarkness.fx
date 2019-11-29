#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_3
	#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

Texture2D Texture;
sampler s0;
float2 LightCoord;
float2 LightDirection;

sampler2D TextureSampler = sampler_state
{
	Texture = <Texture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 AlphaToRGBShader(VertexShaderOutput input) : COLOR
{
	float4 sourceColor = tex2D(s0, input.TextureCoordinates);
	sourceColor.rgb = sourceColor.a;
	return sourceColor;
}

float4 RayCastShader(VertexShaderOutput input) : COLOR
{
	const float2 windowDim = float2(512, 448);
	const float rayLevel = 0.96f;
	const float transmitCoefficient = 0.95f;
	const float linkRadius = 48.0f;
	const float coneHalfAngle = 0.785f;
	const float coneDecay = 1.2f;

	float4 ray;
	ray.a = rayLevel;
	ray.rgb = 0.0f;
	float2 sourceCoord = input.TextureCoordinates;
	float4 sourceColor = tex2D(s0, sourceCoord);
	float2 targetCoord = LightCoord / windowDim;
	float2 coordDelta;
	float4 targetColor;
	int i;

	float magnitude, halfAngle, distanceAttenuation, coneAttenuation, coneAlpha;

	coordDelta = sourceCoord - targetCoord;

	// Vision cone
	magnitude = length(coordDelta) / 0.5;
	halfAngle = acos(dot(normalize(coordDelta), LightDirection));
	distanceAttenuation = saturate(1.0 - magnitude);
	coneAttenuation = 1.0 - pow(abs(halfAngle / coneHalfAngle), coneDecay);
	coneAlpha = distanceAttenuation * coneAttenuation * step(halfAngle, coneHalfAngle);
	coneAlpha = 1.0f - coneAlpha;

	// Ray march
	// Since the pixels are scaled by 2, every 2 can be skipped in stride
	float2 stride = normalize(coordDelta) / 256.0f;
	float2 marchCoord = targetCoord;

	// Merge level
	ray.a = ray.a + (1 - ray.a) * max(coneAlpha, ray.a);

	[unroll(32)]
	for (i = 0; i < 256; i++) {
		targetColor = tex2D(s0, marchCoord);
		ray.a *= lerp(transmitCoefficient, 1.0f, targetColor.a);

		if (ray.a <= 0.00333f) break;
		marchCoord += stride;
	}

	// Alpha merge
	ray.a = ray.a + (1 - ray.a) * max(coneAlpha, ray.a);

	// Character occlusion
	float dist = distance(sourceCoord, targetCoord);

	if (dist <= linkRadius / windowDim.x && sourceColor.a > 0.99f) {
		ray.a = smoothstep((linkRadius - 1.0f) / windowDim.x, linkRadius / windowDim.x, dist) * ray.a;
	}
	return ray;
}

float4 AlphaDebugShader(VertexShaderOutput input) : COLOR
{
	float4 sourceColor = tex2D(s0, input.TextureCoordinates);
	if (sourceColor.a < 0.9f) {
		sourceColor.r = 1.0f;
		sourceColor.gb = 0.0f;
		sourceColor.a = 1.0f;
	}
	return sourceColor;
}

technique RayCast
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL RayCastShader();
	}
};

technique AlphaDebug
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL AlphaDebugShader();
	}
};