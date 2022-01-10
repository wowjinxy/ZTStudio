#include "studio.hpp"
#include "directx/directx.hpp"
#include "imgui/imgui.hpp"

::RECT studio::rect;
::POINT studio::p, studio::sp;
bool studio::is_down;
std::vector<const char*> studio::args{};

::HWND studio::hwnd = 0;

auto studio::find_arg(const char* arg) -> bool
{
    auto retn = false;

    for (auto i = 0; i < studio::args.size(); i++)
    {
        if (!strcmp(arg, studio::args[i]))
        {
            retn = true;
        }
    }

    return retn;
}

auto WINAPI studio::wnd_proc(::HWND hWnd, ::UINT msg, ::WPARAM wParam, ::LPARAM lParam) -> ::LRESULT
{
    if (::ImGui_ImplWin32_WndProcHandler(hWnd, msg, wParam, lParam))
    {
        return true;
    }

    switch (msg)
    {
        case WM_SIZE:
            if (directx::g_pd3dDevice != NULL && wParam != SIZE_MINIMIZED)
            {
                directx::g_d3dpp.BackBufferWidth = LOWORD(lParam);
                directx::g_d3dpp.BackBufferHeight = HIWORD(lParam);
                directx::reset_device();
            }
        return 0;

        case WM_SYSCOMMAND:
            if ((wParam & 0xfff0) == SC_KEYMENU)
            {
                return 0;
            }
        break;

        case WM_LBUTTONDOWN:
            ::SetCapture(hWnd);
            ::GetWindowRect(hWnd, &studio::rect);
            ::GetCursorPos(&studio::sp);

            //Detect if we should allow moving
            POINT temp;
            ::GetCursorPos(&temp);
            ::ScreenToClient(studio::hwnd, &temp);

            if (temp.y <= 20)
            {
                is_down = true;
            }
        return 0;

        case WM_LBUTTONUP:
            is_down = false;
            ::ReleaseCapture();
        return 0;

        case WM_MOUSEMOVE:
        if (is_down)
        {
            ::GetCursorPos(&p);
            ::MoveWindow(hWnd, studio::rect.left + studio::p.x - studio::sp.x, studio::rect.top + studio::p.y - studio::sp.y,
                studio::rect.right - studio::rect.left, studio::rect.bottom - studio::rect.top, true);
        }
        return 0;

        case WM_DESTROY:
            ::PostQuitMessage(0);
        return 0;
    }
    return ::DefWindowProc(hWnd, msg, wParam, lParam);
}

auto studio::cleanup(::HWND hwnd, ::WNDCLASSEX wc) -> void
{
    imgui::cleanup(hwnd, wc);
    directx::cleanup();
    ::DestroyWindow(hwnd);
    ::UnregisterClass(wc.lpszClassName, wc.hInstance);
}

auto studio::init() -> void
{
    ::WNDCLASSEX wc = { sizeof(WNDCLASSEX), CS_CLASSDC, studio::wnd_proc, 0, 0, ::GetModuleHandleA(0), 0, 0, 0, 0, L"ZTStudio", 0 };
    ::RegisterClassEx(&wc);

    ::RECT desktop;
    ::GetWindowRect(::GetDesktopWindow(), &desktop);

    studio::hwnd = ::CreateWindow(wc.lpszClassName, L"ZTStudio", 0x80000000, (desktop.right * 0.5f) - (WIDTH * 0.5f),
        (desktop.bottom * 0.5f) - (HEIGHT * 0.5f), WIDTH, HEIGHT, 0, 0, wc.hInstance, 0);

    if (!directx::create_device(hwnd))
    {
        directx::cleanup();
        ::UnregisterClass(wc.lpszClassName, wc.hInstance);
        ::MessageBox(nullptr, L"An error occured when creating the D3D9 device!", L"ZTStudio", 0);
    }

    ::ShowWindow(studio::hwnd, SW_SHOWDEFAULT);
    ::UpdateWindow(studio::hwnd);

    imgui::init(studio::hwnd);
    imgui::update();

    studio::cleanup(studio::hwnd, wc);

    return;
}

int __stdcall WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd)
{
    for (auto i = 0; i < __argc; i++)
    {
        studio::args.emplace_back(__argv[i]);
    }

    studio::init();
    return 0;
}