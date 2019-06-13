#version 330 core

in vec4 vertex;

out vec2 fs_texture_map;

void main()
{
	fs_texture_map = vertex.zw;
	gl_Position = vec4(vertex.xy, 0.0, 1.0);
}
