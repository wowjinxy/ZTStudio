workspace "ZTStudio"
	location ".\\build\\"

	targetdir "%{wks.location}\\bin\\%{cfg.buildcfg}\\"
	objdir "%{wks.location}\\obj\\%{cfg.buildcfg}\\%{prj.name}\\"
	buildlog "%{wks.location}\\obj\\%{cfg.buildcfg}\\%{prj.name}.log"

	vectorextensions "sse2"
	largeaddressaware "on"
	editandcontinue "off"
	staticruntime "on"

	systemversion "latest"
	characterset "unicode"
	architecture "x86"
	warnings "extra"

	syslibdirs {
		"$(DXSDK_DIR)lib\\x86\\",
	}

	includedirs {
		".\\src\\",
		"$(DXSDK_DIR)include\\",
		".\\deps\\imgui\\",
	}

	buildoptions {
		"/Zm200",
		"/utf-8",
		"/std:c++17",
		"/bigobj",
	}

	flags {
		"noincrementallink",
		"no64bitchecks",
		"shadowedvariables",
		"undefinedidentifiers",
		"multiprocessorcompile",
	}

	defines {
		"NOMINMAX",
		"WIN32_LEAN_AND_MEAN",
		"_CRT_SECURE_NO_WARNINGS",
		"_SILENCE_ALL_CXX17_DEPRECATION_WARNINGS",
	}

	platforms {
		"x86",
	}

	configurations {
		"Release",
		"Debug",
	}

	configuration "Release"
		defines "NDEBUG"
		optimize "debug"
		runtime "debug"
		symbols "on"

	configuration "Debug"
		defines "DEBUG"
		optimize "debug"
		runtime "debug"
		symbols "on"

	project "ZTStudio"
		targetname "ZTStudio"
		language "c++"
		kind "windowedapp"
		warnings "off"

		pchheader "stdafx.hpp"
		pchsource "src/stdafx.cpp"
		forceincludes "stdafx.hpp"
		
		dependson {
			"ImGui",
		}

		links {
			"d3d9",
			"d3dx9",
			"imgui",
		}
		
		files {
			".\\src\\**",
		}

		includedirs {
			".\\src\\",
		}
		
	group "Dependencies"
	
	project "ImGui"
		targetname "imgui"

		language "c++"
		kind "staticlib"

		files {
			".\\deps\\imgui\\*.h",
			".\\deps\\imgui\\*.cpp",
			".\\deps\\imgui\\backends\\imgui_impl_dx9.h",
			".\\deps\\imgui\\backends\\imgui_impl_dx9.cpp",
			".\\deps\\imgui\\backends\\imgui_impl_win32.h",
			".\\deps\\imgui\\backends\\imgui_impl_win32.cpp",
		}

		includedirs {
			".\\deps\\imgui\\",
			".\\deps\\imgui\\backends\\",
		}