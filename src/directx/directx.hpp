#pragma once

class directx final
{
public:
	static ::LPDIRECT3D9 g_pD3D;
	static ::LPDIRECT3DDEVICE9 g_pd3dDevice;
	static ::D3DPRESENT_PARAMETERS g_d3dpp;

	static auto create_device(::HWND hWnd) -> bool;
	static auto cleanup() -> void;
	static auto reset_device() -> void;
};