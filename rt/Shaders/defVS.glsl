#version 330 core

in vec3 position;
in vec2 texture_map;
in vec3 normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fs_position;
out vec2 fs_texture_map;
out vec3 fs_normal;

void main()
{
	fs_position = position;
	fs_texture_map = vec2(texture_map.x, 1.0 - texture_map.y);
	fs_normal = normal;

	gl_Position = projection * view * model * vec4(position, 1.0);
}
