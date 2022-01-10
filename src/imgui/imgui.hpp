#pragma once

class imgui final
{
public:
    static bool shutdown;

    static auto init(::HWND hwnd) -> void;
    static auto update() -> void;
    static auto cleanup(::HWND hwnd, ::WNDCLASSEX wc) -> void;
};