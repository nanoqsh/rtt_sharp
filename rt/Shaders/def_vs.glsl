#version 330 core

layout (location = 0) in vec3 position;
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
	fs_texture_map = vec2(texture_map.x, 1.0 - texture_map.y);
	fs_normal = normal;

	vec4 result = projection * view * model * vec4(position, 1.0);
	fs_position = vec3(result);
	gl_Position = result;
}
