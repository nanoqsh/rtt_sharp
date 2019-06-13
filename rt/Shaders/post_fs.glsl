#version 330 core

uniform sampler2D frame;

in vec2 fs_texture_map;

out vec4 color;

void main()
{
	color = vec4((1.0 - texture2D(frame, fs_texture_map).xyz), 1.0);
}
