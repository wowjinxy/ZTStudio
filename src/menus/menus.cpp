#include "menus/menus.hpp"
#include "imgui/imgui.hpp"
#include "directx/directx.hpp"

#include "studio.hpp"

auto menus::init() -> void
{
	auto& io = ImGui::GetIO();
	auto& s = ImGui::GetStyle();

	io.IniFilename = nullptr;

	s.WindowBorderSize = {};
	s.WindowPadding = {};
	s.WindowRounding = {};
	s.ChildBorderSize = {};
}

auto menus::update() -> void
{
	menus::main();

	if (menus::states::settings) menus::settings();
}

auto menus::main() -> void
{
	ImGui::SetNextWindowPos({ 0, 0 });
	ImGui::SetNextWindowSize({ WIDTH, 40 });

	ImGui::Begin("ZTStudio", nullptr, ImGuiWindowFlags_NoCollapse | ImGuiWindowFlags_NoResize | ImGuiWindowFlags_NoBringToFrontOnFocus);
	{
		ImGui::BeginTabBar("Tab");
		{
			if (ImGui::TabItemButton("Settings"))
			{
				menus::states::settings = true;
			}

			if (ImGui::TabItemButton("Close"))
			{
				imgui::shutdown = true;
			}
		}
		ImGui::EndTabBar();
	}
	ImGui::End();
}

auto menus::settings() -> void
{
	ImGui::SetNextWindowSize({ 300, 300 });

	ImGui::Begin("Settings", &menus::states::settings, ImGuiWindowFlags_NoCollapse | ImGuiWindowFlags_NoResize);
	{
		ImGui::BeginChild("Child");
		{
			ImGui::Text("Game Path:");
			ImGui::InputText(" ", menus::settings::game_path, MAX_PATH, ImGuiInputTextFlags_ReadOnly);
			ImGui::SameLine();
			ImGui::Button("Browse");

			ImGui::NewLine();

			ImGui::Text("Background Color:");
			ImGui::SliderInt("R", &menus::settings::r, 0, 255);
			ImGui::SliderInt("G", &menus::settings::g, 0, 255);
			ImGui::SliderInt("B", &menus::settings::b, 0, 255);
		}
		ImGui::EndChild();
	}
	ImGui::End();
}

//states
bool menus::states::settings = false;
//

//settings
char menus::settings::game_path[MAX_PATH];
//clear color
int menus::settings::r = 0.0f;
int menus::settings::g = 0.0f;
int menus::settings::b = 0.0f;
//