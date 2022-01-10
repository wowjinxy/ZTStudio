#include "menus/menus.hpp"
#include "imgui/imgui.hpp"
#include "directx/directx.hpp"

#include "studio.hpp"

auto menus::init() -> void
{
	auto& io = ImGui::GetIO();
	io.IniFilename = nullptr;

	auto& s = ImGui::GetStyle();

	s.WindowBorderSize = {};
	s.WindowPadding = {};
	s.WindowRounding = {};
	s.ChildBorderSize = {};
}

auto menus::update() -> void
{
	menus::main();
}

auto menus::main() -> void
{
	auto& s = ImGui::GetStyle();
	auto& io = ImGui::GetIO();

	ImGui::SetNextWindowPos({ 0, 0 });
	ImGui::SetNextWindowSize({ WIDTH, HEIGHT });

	ImGui::Begin("ZTStudio", nullptr, ImGuiWindowFlags_NoDecoration);
	{
		ImGui::SetCursorPos({ 5, 5 });
		ImGui::Text("ZTStudio");
		ImGui::SetCursorPos({ WIDTH - 20, 5 });
		if (ImGui::Button("X"))
		{
			imgui::shutdown = true;
		}
	}
	ImGui::End();
}