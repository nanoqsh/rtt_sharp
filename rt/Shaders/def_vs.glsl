#version 330 core

layout (location = 0) in vec3 position;
in vec2 texture_map;
in vec3 normal;

out vec3 l_position;
out vec3 l_normal;
out vec2 l_texture_map;
out vec4 l_position_light_space;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat4 light_space;

out vec3 fs_position;
out vec2 fs_texture_map;
out vec3 fs_normal;

void main()
{
	l_position = vec3(model * vec4(position, 1.0));
    l_normal = transpose(inverse(mat3(model))) * normal;
    l_texture_map = texture_map;
    l_position_light_space = light_space * vec4(l_position, 1.0);

	fs_texture_map = vec2(texture_map.x, 1.0 - texture_map.y);
	fs_normal = normal;

	vec4 result = projection * view * model * vec4(position, 1.0);
	fs_position = vec3(result);
	gl_Position = result;
}
