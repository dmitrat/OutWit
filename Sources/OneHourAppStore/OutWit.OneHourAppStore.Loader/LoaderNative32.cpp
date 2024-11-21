#include "pch.h"
#include "LoaderNative32.h"

#if !defined(_WIN64)
OutWit::OneHourAppStore::Loader::LoaderNative::LoaderNative()
{
	m_dosHeader = nullptr;
	m_ntHeader = nullptr;
	m_sectionHeader = nullptr;

	m_context = nullptr;

	m_imageBase = nullptr;
	m_pImageBase = nullptr;
}

OutWit::OneHourAppStore::Loader::LoaderNative::~LoaderNative()
{
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
	m_ntHeader = PIMAGE_NT_HEADERS(DWORD(image) + m_dosHeader->e_lfanew);

	return m_ntHeader->Signature == IMAGE_NT_SIGNATURE;
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::CreateCurrentProcessInstance()
{
	char currentFilePath[1024];

	GetModuleFileNameA(nullptr, currentFilePath, 1024);

	ZeroMemory(&m_processInfo, sizeof(m_processInfo)); // Null the memory
	ZeroMemory(&m_startupInfo, sizeof(m_startupInfo)); // Null the memory

	return CreateProcessA(currentFilePath, nullptr, nullptr, nullptr, FALSE,
		CREATE_SUSPENDED, nullptr, nullptr, &m_startupInfo, &m_processInfo);
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::AllocateContext()
{
	m_context = LPCONTEXT(VirtualAlloc(NULL, sizeof(m_context), MEM_COMMIT, PAGE_READWRITE));
	m_context->ContextFlags = CONTEXT_FULL;

	return GetThreadContext(m_processInfo.hThread, LPCONTEXT(m_context));
}

bool OutWit::OneHourAppStore::Loader::LoaderNative::ReadProcessImage()
{
	try
	{
		ReadProcessMemory(m_processInfo.hProcess, LPCVOID(m_context->Ebx + 8), LPVOID(&m_imageBase), 4, 0);

		m_pImageBase = VirtualAllocEx(m_processInfo.hProcess, LPVOID(m_ntHeader->OptionalHeader.ImageBase),
			m_ntHeader->OptionalHeader.SizeOfImage, 0x3000, PAGE_EXECUTE_READWRITE);

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
		WriteProcessMemory(m_processInfo.hProcess, m_pImageBase, image, m_ntHeader->OptionalHeader.SizeOfHeaders, NULL);

		for (int count = 0; count < m_ntHeader->FileHeader.NumberOfSections; count++)
		{
			m_sectionHeader = PIMAGE_SECTION_HEADER(DWORD(image) + m_dosHeader->e_lfanew + 248 + (count * 40));

			WriteProcessMemory(m_processInfo.hProcess, LPVOID(DWORD(m_pImageBase) + m_sectionHeader->VirtualAddress),
				LPVOID(DWORD(image) + m_sectionHeader->PointerToRawData), m_sectionHeader->SizeOfRawData, 0);
		}
		WriteProcessMemory(m_processInfo.hProcess, LPVOID(m_context->Ebx + 8),
			LPVOID(&m_ntHeader->OptionalHeader.ImageBase), 4, 0);

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
		m_context->Eax = DWORD(m_pImageBase) + m_ntHeader->OptionalHeader.AddressOfEntryPoint;
		SetThreadContext(m_processInfo.hThread, LPCONTEXT(m_context));
		ResumeThread(m_processInfo.hThread);

		return m_processInfo;
	}
	catch (...)
	{
		return {};
	}

}

#endif
