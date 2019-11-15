#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
sampler s0;

// -0.5 to 0.5
float InSaturationOffset;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

// rgb to hsv based on code from
// http://developer.download.nvidia.com/shaderlibrary/webpages/shader_library.html
// r = h, g = s, b = v, a = a

float4 rgba_to_hsva(float4 rgba)
{
	float4 hsva;
	float3 delta_rgb;
	float min, max, delta;
	hsva.a = rgba.a;

	// Find max and minimum color channel
	if (rgba.r > rgba.g)
	{
		max = rgba.r;
		min = rgba.g;
	}
	else
	{
		max = rgba.g;
		min = rgba.r;
	}

	if (rgba.b > max) max = rgba.b;
	if (rgba.b < min) min = rgba.b;

	hsva.rg = 0.0f;
	hsva.b = max;

	delta = max - min;
	// If maximum and minimum are identical, assume gray so no saturation or hue
	if (delta == 0) return hsva;

	// Green is the "strongest" color, desaturate relative to maximum
	hsva.g = delta / hsva.b;
	delta_rgb = (hsva.bbb - rgba.rgb + 3.0 * delta) / 6.0 / delta;
	if (rgba.r == hsva.b) hsva.r = delta_rgb.b - delta_rgb.g;
	else if (rgba.g == hsva.b) hsva.r = 1.0 / 3.0 + delta_rgb.r - delta_rgb.z;
	else if (rgba.b == hsva.b) hsva.r = 2.0 / 3.0 + delta_rgb.g - delta_rgb.r;

	return hsva;
}

float4 hsva_to_rgba(float4 hsva)
{
	float4 rgba;

	float hue = hsva.x * 6.0;
	float hue_floored = floor(hue);
	float sat_value1 = hsva.b * (1.0 - hsva.g);
	float sat_value2 = hsva.b * (1.0 - hsva.g * (hue - hue_floored));
	float sat_value3 = hsva.b * (1.0 - hsva.g * (1 -  hue + hue_floored));

	if (hue_floored == 0.0)
		rgba = float4(hsva.b, sat_value3, sat_value1, hsva.a);
	else if (hue_floored == 1.0)
		rgba = float4(sat_value2, hsva.b, sat_value1, hsva.a);
	else if (hue_floored == 2.0)
		rgba = float4(sat_value1, hsva.b, sat_value3, hsva.a);
	else if (hue_floored == 3.0)
		rgba = float4(sat_value1, sat_value2, hsva.b, hsva.a);
	else if (hue_floored == 4.0)
		rgba = float4(sat_value3, sat_value1, hsva.b, hsva.a);
	else
		rgba = float4(hsva.b, sat_value1, sat_value2, hsva.a);

	return rgba;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	// Visual range is -0.5 to 0.5
	float4 color = tex2D(s0, input.TextureCoordinates);
	float4 hsva = rgba_to_hsva(color);
	hsva.r -= InSaturationOffset;
	return hsva_to_rgba(hsva);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};