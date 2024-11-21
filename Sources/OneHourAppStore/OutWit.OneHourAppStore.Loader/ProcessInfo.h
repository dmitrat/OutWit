#pragma once
using namespace OutWit::Common::Abstract;
using namespace System;

namespace OutWit::OneHourAppStore::Loader
{
	public ref class ProcessInfo: public ModelBase
	{
	private:
		IntPtr m_processHandle;
		IntPtr m_threadHandle;

		long m_processId;
		long m_threadId;

		IntPtr m_loader;

	internal:
		ProcessInfo();

#if _DEBUG
	public :
		ProcessInfo(IntPtr processHandle, IntPtr threadHandle, long processId, long threadId, IntPtr loader);
#endif

	public:
		bool Is(ModelBase^ modelBase, double tolerance) override;
		bool Is(ModelBase^ modelBase);
		ModelBase^ Clone() override;

	public:
		property IntPtr ProcessHandle
		{
			IntPtr get();

		internal:
			void set(IntPtr value);
		}

		property IntPtr ThreadHandle
		{
			IntPtr get();

		internal:
			void set(IntPtr value);
		}

		property long ProcessId
		{
			long get();

		internal:
			void set(long value);
		}

		property long ThreadId
		{
			long get();

		internal:
			void set(long value);
		}

		property IntPtr Loader
		{
			IntPtr get();

		internal:
			void set(IntPtr value);
		}

	};
}

