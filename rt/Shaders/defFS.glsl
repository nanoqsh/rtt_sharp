#version 330 core

uniform sampler2D layer0;

in vec3 fs_position;
in vec2 fs_texture_map;
in vec3 fs_normal;

out vec4 color;

void main()
{
	vec3 lightDir = normalize(vec3(0.6, 1.0, 0.2));
	vec3 diffuse = max(dot(fs_normal, lightDir), 0.0) * vec3(0.9, 1.0, 0.7);
	vec3 ambient = vec3(0.5, 0.6, 0.6);

	color = texture2D(layer0, fs_texture_map) * vec4(diffuse + ambient, 1.0);
}
