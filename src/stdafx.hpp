#include <string>
#include <vector>
#include <filesystem>
#include <thread>
#include <ctime>
#include <future>

#include <windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include <shellapi.h>
#include <DbgHelp.h>
#include <psapi.h>
#include <tlhelp32.h>

#include <imgui.h>
#include <backends/imgui_impl_dx9.h>
#include <backends/imgui_impl_win32.h>

using namespace std::literals;

long ImGui_ImplWin32_WndProcHandler(::HWND, std::uint32_t, std::uint32_t, long);