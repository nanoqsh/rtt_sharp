#version 330 core

uniform sampler2D layer0;
uniform vec4 fog_colour;

in vec3 fs_position;
in vec2 fs_texture_map;
in vec3 fs_normal;

out vec4 color;

void main()
{
	float fog_maxdist = 24.0;
	float fog_mindist = 0.1;

	float dist = length(fs_position);
	float fog_factor = (fog_maxdist - dist) / (fog_maxdist - fog_mindist);
	fog_factor = clamp(fog_factor, 0.0, 1.0);

	vec3 lightDir = normalize(vec3(0.6, 1.0, 0.2));
	vec3 diffuse = max(dot(fs_normal, lightDir), 0.0) * vec3(0.9, 1.0, 0.7);
	vec3 ambient = vec3(0.5, 0.6, 0.6);

	vec4 res_color = texture2D(layer0, fs_texture_map) * vec4(diffuse + ambient, 1.0);
	color = mix(fog_colour, res_color, fog_factor);
}
