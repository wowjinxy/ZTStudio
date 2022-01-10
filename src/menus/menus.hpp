#pragma once

namespace menus
{
	auto init() -> void;
	auto update() -> void;

	//Sorted menu functions
	auto main() -> void;
	auto settings() -> void;
	//

	class states
	{
	public:
		static bool settings;
	};

	class settings
	{
	public:
		static char game_path[MAX_PATH];

		static int r;
		static int g;
		static int b;
	};
}