cmake_minimum_required (VERSION 2.6)
project (HPL)

set(HPL_VERSION_MAJOR 1)
set(HPL_VERSION_MINOR 0)

include_directories(
	${CMAKE_CURRENT_SOURCE_DIR}/include
	${CMAKE_CURRENT_SOURCE_DIR}/../OALWrapper/include
	${CMAKE_CURRENT_SOURCE_DIR}/../dependencies/include
)
file(GLOB_RECURSE all_sources
    ${CMAKE_CURRENT_SOURCE_DIR}/sources/*.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/sources/*.c
)
# Removed Sources
file(GLOB_RECURSE to_remove_all
    win32.cpp
    Video*Theora.cpp
    )
list(REMOVE_ITEM all_sources
    ${to_remove_all})
list(REMOVE_DUPLICATES all_sources)

add_library(HPL STATIC ${all_sources})

add_library(Newton STATIC IMPORTED)
set_target_properties(Newton PROPERTIES IMPORTED_LOCATION ${CMAKE_CURRENT_SOURCE_DIR}/../dependencies/lib/linux/libNewton.a)

add_library(angelscript STATIC IMPORTED)
set_target_properties(angelscript PROPERTIES IMPORTED_LOCATION ${CMAKE_CURRENT_SOURCE_DIR}/../dependencies/lib/linux/libAngelscript.a)

INCLUDE(${CMAKE_ROOT}/Modules/FindSDL.cmake)
INCLUDE(${CMAKE_ROOT}/Modules/FindSDL_ttf.cmake)
INCLUDE(${CMAKE_ROOT}/Modules/FindSDL_image.cmake)
INCLUDE(${CMAKE_ROOT}/Modules/FindOpenAL.cmake)
INCLUDE(${CMAKE_ROOT}/Modules/FindOpenGL.cmake)
INCLUDE(${CMAKE_ROOT}/Modules/FindFLTK.cmake)

find_package(SDL REQUIRED)
find_package(SDL_ttf REQUIRED)
find_package(SDL_image REQUIRED)
find_package(OpenAL REQUIRED)
find_package(OpenGL REQUIRED)
find_package(FLTK REQUIRED)

if (NOT SDL_FOUND)
	message(FATAL_ERROR "Package SDL could not be found")
endif (NOT SDL_FOUND)

if (NOT SDLTTF_FOUND)
	message(FATAL_ERROR "Package SDL_ttf could not be found")
endif (NOT SDLTTF_FOUND)

if (NOT SDLIMAGE_FOUND)
	message(FATAL_ERROR "Package SDL_image could not be found")
endif (NOT SDLIMAGE_FOUND)


if (NOT OPENAL_FOUND)
	message (FATAL_ERROR "Package OpenAL could not be found")
endif (NOT OPENAL_FOUND)

if (NOT OPENGL_FOUND)
	message (FATAL_ERROR "Package OpenGL could not be found")
endif (NOT OPENGL_FOUND)

if (NOT FLTK_FOUND)
	message (FATAL_ERROR "Package FLTK could not be found")
endif (NOT FLTK_FOUND)
