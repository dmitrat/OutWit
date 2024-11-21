#include "pch.h"
#include "LoaderNative64.h"

#if defined(_WIN64)
OutWit::OneHourAppStore::Loader::LoaderNative::LoaderNative()
{
	m_dosHeader = nullptr;
	m_ntHeader = nullptr;
	m_sectionHeader = nullptr;

	m_pImageBase = nullptr;
}

OutWit::OneHourAppStore::Loader::LoaderNative::~LoaderNative()
{
	delete m_dosHeader;
	delete m_ntHeader;
	delete m_sectionHeader;
}

PROCESS_INFORMATION OutWit::OneHourAppStore::Loader::LoaderNative::LoadProcess(void* image)
{
	if (!InitImage(image))
		return {};

	if (!CreateCurrentProcessInstance())
		return {};

	if (!AllocateContext())
		return {};

	if (!ReadProcessImage())
		return {};

	if (!WriteMemory(image))
		return {};

	return StartProcess();
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::InitImage(void* image)
{
	m_dosHeader = PIMAGE_DOS_HEADER(image);
	m_ntHeader = PIMAGE_NT_HEADERS(DWORD64(image) + m_dosHeader->e_lfanew);

	return m_ntHeader->Signature == IMAGE_NT_SIGNATURE;
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::CreateCurrentProcessInstance()
{
	char currentFilePath[1024];

	GetModuleFileNameA(nullptr, currentFilePath, 1024);

	ZeroMemory(&m_processInfo, sizeof(m_processInfo)); // Null the memory
	ZeroMemory(&m_startupInfo, sizeof(m_startupInfo)); // Null the memory
	m_startupInfo.cb = sizeof(m_startupInfo);

	return CreateProcessA(currentFilePath, nullptr, nullptr, nullptr, FALSE,
		CREATE_SUSPENDED, nullptr, nullptr, &m_startupInfo, &m_processInfo);
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::AllocateContext()
{
	m_context.ContextFlags = CONTEXT_FULL;

	return GetThreadContext(m_processInfo.hThread, &m_context);
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::ReadProcessImage()
{
	try
	{
		ReadProcessMemory(m_processInfo.hProcess, LPCVOID(m_context.Rdx + 16), &m_imageBase,
			sizeof(m_imageBase), nullptr);

		m_pImageBase = VirtualAllocEx(m_processInfo.hProcess, LPVOID(m_ntHeader->OptionalHeader.ImageBase),
			m_ntHeader->OptionalHeader.SizeOfImage, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

		if (m_pImageBase == 00000000)
		{
			ResumeThread(m_processInfo.hThread);
			TerminateProcess(m_processInfo.hProcess, 0);
			return false;
		}

		return true;
	}
	catch (...)
	{
		return false;
	}

}

bool OutWit::OneHourAppStore::Loader::LoaderNative::WriteMemory(void* image)
{
	try
	{
		WriteProcessMemory(m_processInfo.hProcess, m_pImageBase, image, m_ntHeader->OptionalHeader.SizeOfHeaders, nullptr);

		for (int count = 0; count < m_ntHeader->FileHeader.NumberOfSections; count++)
		{
			m_sectionHeader = PIMAGE_SECTION_HEADER(DWORD64(image) + m_dosHeader->e_lfanew +
				sizeof(IMAGE_NT_HEADERS64) + (count * sizeof(IMAGE_SECTION_HEADER)));

			WriteProcessMemory(m_processInfo.hProcess, LPVOID(DWORD64(m_pImageBase) + m_sectionHeader->VirtualAddress),
				LPVOID(DWORD64(image) + m_sectionHeader->PointerToRawData), m_sectionHeader->SizeOfRawData, nullptr);
		}
		WriteProcessMemory(m_processInfo.hProcess, LPVOID(m_context.Rdx + 16),
			&m_ntHeader->OptionalHeader.ImageBase, sizeof(m_ntHeader->OptionalHeader.ImageBase), nullptr);

		return true;
	}
	catch (...)
	{
		return false;
	}
}

PROCESS_INFORMATION OutWit::OneHourAppStore::Loader::LoaderNative::StartProcess()
{
	try
	{
		m_context.Rcx = DWORD64(m_pImageBase) + m_ntHeader->OptionalHeader.AddressOfEntryPoint;
		SetThreadContext(m_processInfo.hThread, &m_context);
		ResumeThread(m_processInfo.hThread);

		return m_processInfo;
	}
	catch (...)
	{
		return {};
	}

}

#endif
