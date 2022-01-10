#pragma once

#define WIDTH 800
#define HEIGHT 600

class studio final
{
public:
    static auto init() -> void;
    static ::RECT rect;
    static ::POINT p, sp;
    static bool is_down;
    static ::HWND hwnd;
    static std::vector<const char*> args;
    static auto find_arg(const char* arg) -> bool;

private:
    static auto WINAPI wnd_proc(::HWND hWnd, ::UINT msg, ::WPARAM wParam, ::LPARAM lParam) -> ::LRESULT;
    static auto cleanup(::HWND hwnd, ::WNDCLASSEX wc) -> void;
};