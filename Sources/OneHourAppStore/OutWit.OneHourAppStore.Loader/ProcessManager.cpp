#include "windows.h"

#if defined(_WIN64)
#include "LoaderNative64.h"
#else
#include "LoaderNative32.h"
#endif

#include "ProcessManager.h"

using namespace OutWit::OneHourAppStore::Loader;

#pragma region Constructors

ProcessManager::ProcessManager()
{
	InitDefaults();
	InitEvents();
}

ProcessManager::~ProcessManager()
{

}


#pragma endregion

#pragma region Initialization

void ProcessManager::InitDefaults()
{
	m_processInfo = nullptr;
	m_process = nullptr;
	m_status = ProcessStatus::NotRunning;
}

void ProcessManager::InitEvents()
{
	ProcessStarted += gcnew ProcessManagerEventHandler(this, &ProcessManager::OnProcessStarted);
	ProcessClosed += gcnew ProcessManagerEventHandler(this, &ProcessManager::OnProcessClosed);
	ProcessPaused += gcnew ProcessManagerEventHandler(this, &ProcessManager::OnProcessPaused);
	ProcessResumed += gcnew ProcessManagerEventHandler(this, &ProcessManager::OnProcessResumed);
}

String^ ProcessManager::ToString()
{
	return m_status.ToString();
}

bool ProcessManager::Run(array<unsigned char>^ image)
{
	if (m_status != ProcessStatus::NotRunning || m_processInfo != nullptr)
		return false;

	try
	{
		LoaderNative* loader = new LoaderNative();

		PROCESS_INFORMATION info = loader->LoadProcess(Pointer(image));

		m_processInfo = gcnew ProcessInfo();

		m_processInfo->ProcessHandle = static_cast<IntPtr>(info.hProcess);
		m_processInfo->ThreadHandle = static_cast<IntPtr>(info.hThread);

		m_processInfo->ProcessId = info.dwProcessId;
		m_processInfo->ThreadId = info.dwThreadId;

		m_processInfo->Loader = static_cast<IntPtr>(loader);

		if (m_processInfo->ProcessId == 0 || m_processInfo->ThreadId == 0)
			throw gcnew Exception();

		m_process = Process::GetProcessById((int)m_processInfo->ProcessId);
		m_process->EnableRaisingEvents = true;
		m_process->Exited += gcnew System::EventHandler(this, &ProcessManager::OnProcessExited);

		m_status = ProcessStatus::Active;

		ProcessStarted(this);

		return true;
	}
	catch (...)
	{
		InitDefaults();

		return false;
	}
}

bool ProcessManager::Close()
{
	if (m_status == ProcessStatus::NotRunning || m_processInfo == nullptr)
		return false;

	try
	{
		auto result = TerminateProcess(static_cast<void*>(m_processInfo->ProcessHandle), 0);

		if (!result)
			return false;

		delete m_processInfo->Loader;

		InitDefaults();
		ProcessClosed(this);

		return true;
	}
	catch (...)
	{
		return false;
	}
}

bool ProcessManager::Pause()
{
	if (m_status != ProcessStatus::Active || m_processInfo == nullptr)
		return false;

	try
	{
		auto result = SuspendThread(static_cast<void*>(m_processInfo->ThreadHandle));
		if (result == -1)
			return false;

		m_status = ProcessStatus::Paused;
		ProcessPaused(this);

		return true;
	}
	catch (...)
	{
		return false;
	}
}

bool ProcessManager::Resume()
{
	if (m_status != ProcessStatus::Paused || m_processInfo == nullptr)
		return false;

	try
	{
		auto result = ResumeThread(static_cast<void*>(m_processInfo->ThreadHandle));
		if (result == -1)
			return false;

		m_status = ProcessStatus::Active;
		ProcessResumed(this);

		return true;
	}
	catch (...)
	{
		return false;
	}
}

bool ProcessManager::Ping()
{
	if (m_status == ProcessStatus::NotRunning || m_processInfo == nullptr)
		return false;

	try
	{
		DWORD exitCode;

		if (GetExitCodeProcess(static_cast<void*>(m_processInfo->ProcessHandle), &exitCode))
			return true;

		m_status = ProcessStatus::NotRunning;
		return false;
	}
	catch (...)
	{
		m_status = ProcessStatus::NotRunning;
		return false;
	}
}

void* ProcessManager::Pointer(array<unsigned char>^ managed)
{
	if (managed == nullptr || managed->Length == 0)
		return nullptr;

	const pin_ptr<void> dataPin = &managed[0];

	return dataPin;
}

#pragma endregion

#pragma region Event Handlers

void ProcessManager::OnProcessStarted(ProcessManager^ sender)
{
}

void ProcessManager::OnProcessClosed(ProcessManager^ sender)
{
}

void ProcessManager::OnProcessPaused(ProcessManager^ sender)
{
}

void ProcessManager::OnProcessResumed(ProcessManager^ sender)
{
}

void ProcessManager::OnProcessExited(Object^ sender, EventArgs^ e)
{
	try
	{
		delete m_processInfo->Loader;
	}
	catch (...)
	{
		
	}

	InitDefaults();

	ProcessClosed(this);
}

#pragma endregion


#pragma region Event Handlers

ProcessStatus ProcessManager::Status::get()
{
	return m_status;
}

#pragma endregion
