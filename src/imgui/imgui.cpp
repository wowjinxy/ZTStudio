#include "menus/menus.hpp"
#include "imgui/imgui.hpp"
#include "directx/directx.hpp"

bool imgui::shutdown = false;

auto imgui::init(::HWND hwnd) -> void
{
    ImGui::CreateContext();

    menus::init();

    ImGui_ImplWin32_Init(hwnd);
    ImGui_ImplDX9_Init(directx::g_pd3dDevice);

    ImGui::StyleColorsDark();
}

auto imgui::update() -> void
{
    while (!imgui::shutdown)
    {
        ::MSG msg;
        {
            while (::PeekMessageA(&msg, 0, 0, 0, PM_REMOVE))
            {
                ::TranslateMessage(&msg);
                ::DispatchMessageA(&msg);
                if (msg.message == WM_QUIT)
                {
                    imgui::shutdown = true;
                }
            }

            if (imgui::shutdown)
            {
                break;
            }
        }

        ImGui_ImplDX9_NewFrame();
        ImGui_ImplWin32_NewFrame();
        ImGui::NewFrame();
        menus::update();
        ImGui::EndFrame();

        {
            directx::g_pd3dDevice->SetRenderState(D3DRS_ZENABLE, FALSE);
            directx::g_pd3dDevice->SetRenderState(D3DRS_ALPHABLENDENABLE, FALSE);
            directx::g_pd3dDevice->SetRenderState(D3DRS_SCISSORTESTENABLE, FALSE);

            ::D3DCOLOR clear_col_dx = D3DCOLOR_RGBA(0, 0, 0, 255);
            directx::g_pd3dDevice->Clear(0, NULL, D3DCLEAR_TARGET | D3DCLEAR_ZBUFFER, clear_col_dx, 1.0f, 0);

            if (directx::g_pd3dDevice->BeginScene() >= 0)
            {
                ImGui::Render();
                ImGui_ImplDX9_RenderDrawData(ImGui::GetDrawData());
                directx::g_pd3dDevice->EndScene();
            }
            ::HRESULT result = directx::g_pd3dDevice->Present(NULL, NULL, NULL, NULL);

            if (result == D3DERR_DEVICELOST && directx::g_pd3dDevice->TestCooperativeLevel() == D3DERR_DEVICENOTRESET)
            {
                directx::reset_device();
            }
        }
    }
}

auto imgui::cleanup(::HWND hwnd, ::WNDCLASSEX wc) -> void
{
    ImGui_ImplDX9_Shutdown();
    ImGui_ImplWin32_Shutdown();
    ImGui::DestroyContext();
}