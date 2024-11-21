#pragma once
#include "ProcessInfo.h"
#include "ProcessStatus.h"

using namespace System::Diagnostics;

namespace OutWit::OneHourAppStore::Loader
{
	ref class ProcessManager;

	public delegate void ProcessManagerEventHandler(ProcessManager^ sender);

	public ref class ProcessManager
	{
	public:
		event ProcessManagerEventHandler^ ProcessStarted;
		event ProcessManagerEventHandler^ ProcessClosed;
		event ProcessManagerEventHandler^ ProcessPaused;
		event ProcessManagerEventHandler^ ProcessResumed;

	private:
		ProcessInfo^ m_processInfo;
		ProcessStatus m_status;
		Process^ m_process;

	public:
		ProcessManager();
		~ProcessManager();

	private:
		void InitDefaults();
		void InitEvents();

	public:
		String^ ToString() override;

	public:
		bool Run(array<Byte>^ image);
		bool Close();
		bool Pause();
		bool Resume();
		bool Ping();
	private:
		static void* Pointer(array<Byte>^ managed);

	private:
		void OnProcessStarted(ProcessManager^ sender);
		void OnProcessClosed(ProcessManager^ sender);
		void OnProcessPaused(ProcessManager^ sender);
		void OnProcessResumed(ProcessManager^ sender);
		void OnProcessExited(Object^ sender, EventArgs^ e);

	public:
		property ProcessStatus Status
		{
			ProcessStatus get();
		}
	};
}

