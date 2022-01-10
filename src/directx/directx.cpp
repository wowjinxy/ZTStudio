#include "directx/directx.hpp"

::LPDIRECT3D9 directx::g_pD3D = 0;
::LPDIRECT3DDEVICE9 directx::g_pd3dDevice = 0;
::D3DPRESENT_PARAMETERS directx::g_d3dpp = {};

auto directx::create_device(::HWND hWnd) -> bool
{
    if ((directx::g_pD3D = Direct3DCreate9(D3D_SDK_VERSION)) == 0)
    {
        return false;
    }

    ZeroMemory(&directx::g_d3dpp, sizeof(directx::g_d3dpp));
    directx::g_d3dpp.Windowed = TRUE;
    directx::g_d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
    directx::g_d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;
    directx::g_d3dpp.EnableAutoDepthStencil = TRUE;
    directx::g_d3dpp.AutoDepthStencilFormat = D3DFMT_D16;
    directx::g_d3dpp.PresentationInterval = D3DPRESENT_INTERVAL_ONE;

    if (directx::g_pD3D->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd, D3DCREATE_HARDWARE_VERTEXPROCESSING, &directx::g_d3dpp, &directx::g_pd3dDevice) < 0)
    {
        return false;
    }

    return true;
}

auto directx::cleanup() -> void
{
    if (directx::g_pd3dDevice)
    {
        directx::g_pd3dDevice->Release();
        directx::g_pd3dDevice = 0;
    }

    if (directx::g_pD3D)
    {
        directx::g_pD3D->Release();
        directx::g_pD3D = 0;
    }
}

auto directx::reset_device() -> void
{
    ImGui_ImplDX9_InvalidateDeviceObjects();
    HRESULT hr = directx::g_pd3dDevice->Reset(&directx::g_d3dpp);

    if (hr == D3DERR_INVALIDCALL)
    {
        IM_ASSERT(0);
    }

    ImGui_ImplDX9_CreateDeviceObjects();
}