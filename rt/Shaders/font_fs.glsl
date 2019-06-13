#version 330 core

in vec2 texture_map;

uniform sampler2D font;
uniform vec4 font_color;
uniform bool inverted;

out vec4 color;

void main()
{
	float r;

	if (inverted)
		r = 1.0 - texture(font, texture_map).r;
	else
		r = texture(font, texture_map).r;

    color = font_color * vec4(1.0, 1.0, 1.0, r);
}
