#version 330 core

in vec3 position;
in vec2 textureCoords;
in vec3 normal;

out vec2 pass_textureCoords;
out vec3 surfaceNormal;
out vec3 toLightVector;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform vec3 lightPosition;

void main()
{
    gl_Position = vec4(position, 1.0) * transformationMatrix * viewMatrix * projectionMatrix;
    pass_textureCoords = textureCoords;

    surfaceNormal = mat3(transformationMatrix) * normal;
    toLightVector = lightPosition - (transformationMatrix * vec4(position, 1.0)).xyz;
}
