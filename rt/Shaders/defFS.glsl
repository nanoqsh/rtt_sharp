#version 330 core

uniform sampler2D layer0;

in vec2 fs_texture_map;
in vec3 fs_normal;

out vec4 color;

void main()
{
	color = texture2D(layer0, fs_texture_map);
}
