#version 330 core

uniform sampler2DMS frame;
uniform int frame_width;
uniform int frame_height;

in vec2 fs_texture_map;

out vec4 color;

void main()
{
	//color = texture2D(frame, fs_texture_map);

	ivec2 tex_coords = ivec2(fs_texture_map.x * frame_width, fs_texture_map.y * frame_height);
	vec4 c = texelFetch(frame, tex_coords, 0) +
             texelFetch(frame, tex_coords, 1) +
             texelFetch(frame, tex_coords, 2) +
             texelFetch(frame, tex_coords, 3);
	color = c / 4.0;
}
