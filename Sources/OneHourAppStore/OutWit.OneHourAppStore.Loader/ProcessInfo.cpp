#include "ProcessInfo.h"

OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessInfo()
{
}

#if _DEBUG
OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessInfo(IntPtr processHandle, IntPtr threadHandle,
	long processId, long threadId, IntPtr loader)
{
	m_processHandle = processHandle;
	m_threadHandle = threadHandle;

	m_processId = processId;
	m_threadId = threadId;

	m_loader = loader;
}

#endif

bool OutWit::OneHourAppStore::Loader::ProcessInfo::Is(ModelBase^ modelBase, double tolerance)
{
	auto processInfo = safe_cast<ProcessInfo^>(modelBase);
	if (processInfo == nullptr)
		return false;

	return	processInfo->ProcessHandle == m_processHandle &&
		processInfo->ThreadHandle == m_threadHandle &&
		processInfo->ProcessId == m_processId &&
		processInfo->ThreadId == m_threadId &&
		processInfo->Loader == m_loader;
}

bool OutWit::OneHourAppStore::Loader::ProcessInfo::Is(ModelBase^ modelBase)
{
	return Is(modelBase, DEFAULT_TOLERANCE);
}

ModelBase^ OutWit::OneHourAppStore::Loader::ProcessInfo::Clone()
{
	auto processInfo = gcnew ProcessInfo();

	processInfo->ProcessHandle = m_processHandle;
	processInfo->ThreadHandle = m_threadHandle;

	processInfo->m_processId = m_processId;
	processInfo->m_threadId = m_threadId;

	processInfo->m_loader = m_loader;

	return processInfo;
}

IntPtr OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessHandle::get()
{
	return m_processHandle;
}

void OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessHandle::set(IntPtr value)
{
	m_processHandle = value;
}

IntPtr OutWit::OneHourAppStore::Loader::ProcessInfo::ThreadHandle::get()
{
	return m_threadHandle;
}

void OutWit::OneHourAppStore::Loader::ProcessInfo::ThreadHandle::set(IntPtr value)
{
	m_threadHandle = value;
}

long OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessId::get()
{
	return m_processId;
}

void OutWit::OneHourAppStore::Loader::ProcessInfo::ProcessId::set(long value)
{
	m_processId = value;
}

long OutWit::OneHourAppStore::Loader::ProcessInfo::ThreadId::get()
{
	return m_threadId;
}

void OutWit::OneHourAppStore::Loader::ProcessInfo::ThreadId::set(long value)
{
	m_threadId = value;
}

IntPtr OutWit::OneHourAppStore::Loader::ProcessInfo::Loader::get()
{
	return m_loader;
}

void OutWit::OneHourAppStore::Loader::ProcessInfo::Loader::set(IntPtr value)
{
	m_loader = value;
}
