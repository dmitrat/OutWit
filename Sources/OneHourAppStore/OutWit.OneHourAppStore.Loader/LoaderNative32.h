#pragma once
#include "pch.h"

namespace OutWit::OneHourAppStore::Loader
{
#if !defined(_WIN64)
	class LoaderNative
	{
	public:
		LoaderNative();
		~LoaderNative();

	private:
		IMAGE_DOS_HEADER* m_dosHeader; // For Nt DOS Header symbols
		IMAGE_NT_HEADERS* m_ntHeader; // For Nt PE Header objects & symbols
		IMAGE_SECTION_HEADER* m_sectionHeader;

		PROCESS_INFORMATION m_processInfo;
		STARTUPINFOA m_startupInfo;

		CONTEXT* m_context;

		DWORD* m_imageBase; //Base address of the image
		void* m_pImageBase; // Pointer to the image base

	public:
		PROCESS_INFORMATION LoadProcess(void* image);

	private:
		bool InitImage(void* image);
		bool CreateCurrentProcessInstance();
		bool AllocateContext();
		bool ReadProcessImage();
		bool WriteMemory(void* image);
		PROCESS_INFORMATION StartProcess();
	};

#endif

}

