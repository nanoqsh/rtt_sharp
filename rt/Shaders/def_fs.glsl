#version 330 core

uniform sampler2D layer0;
uniform vec4 fog_colour;

uniform sampler2D shadow_map;

in vec3 fs_position;
in vec2 fs_texture_map;
in vec3 fs_normal;

in vec3 l_position;
in vec3 l_normal;
in vec2 l_texture_map;
in vec4 l_position_light_space;

out vec4 color;

float shadow_calc(vec4 position_light_space)
{
    vec3 proj_coords = position_light_space.xyz / position_light_space.w;
    proj_coords = proj_coords * 0.5 + 0.5;
    float closest_depth = texture(shadow_map, proj_coords.xy).r;

    return proj_coords.z > closest_depth ? 1.0 : 0.0;
}

void main()
{
	float fog_maxdist = 60.0;
	float fog_mindist = 0.1;

	float dist = length(fs_position);
	float fog_factor = (fog_maxdist - dist) / (fog_maxdist - fog_mindist);
	fog_factor = clamp(fog_factor, 0.0, 1.0);

	vec3 light_dir = normalize(vec3(0.6, 1.0, 0.2));
	vec3 diffuse = max(dot(fs_normal, light_dir), 0.0) * vec3(0.9, 1.0, 0.7);
	vec3 ambient = vec3(0.5, 0.6, 0.6);

	float shadow = shadow_calc(l_position_light_space);

	vec4 res_color = texture2D(layer0, fs_texture_map) * vec4((1.0 - shadow) * (diffuse + ambient), 1.0);
	color = mix(fog_colour, res_color, fog_factor);
}
