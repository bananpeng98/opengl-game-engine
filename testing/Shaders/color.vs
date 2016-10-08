#version 440

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec3 tangent;
layout(location = 3) in vec3 color;
layout(location = 4) in vec3 uv;

uniform mat4 M;
uniform mat4 V;
uniform mat4 P;
uniform mat4 MVP;

void main() {
	gl_Position = MVP * vec4(position, 1.0);
}