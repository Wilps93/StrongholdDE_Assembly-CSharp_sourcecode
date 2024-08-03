using System;
using System.Runtime.InteropServices;

public static class MinimumWindowSize
{
	private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	private struct Minmaxinfo
	{
		public Point ptReserved;

		public Point ptMaxSize;

		public Point ptMaxPosition;

		public Point ptMinTrackSize;

		public Point ptMaxTrackSize;
	}

	private struct Point
	{
		public int x;

		public int y;
	}

	private const int DefaultValue = -1;

	private const uint WM_GETMINMAXINFO = 36u;

	private const int GWLP_WNDPROC = -4;

	private static int width;

	private static int height;

	private static bool enabled;

	private static HandleRef hMainWindow;

	private static IntPtr unityWndProcHandler;

	private static IntPtr customWndProcHandler;

	private static WndProcDelegate procDelegate;

	public static void Set(int minWidth, int minHeight)
	{
		if (minWidth < 0 || minHeight < 0)
		{
			throw new ArgumentException("Any component of min size cannot be less than 0");
		}
		width = minWidth;
		height = minHeight;
		if (!enabled)
		{
			hMainWindow = new HandleRef(null, GetActiveWindow());
			procDelegate = WndProc;
			customWndProcHandler = Marshal.GetFunctionPointerForDelegate(procDelegate);
			unityWndProcHandler = SetWindowLongPtr(hMainWindow, -4, customWndProcHandler);
			enabled = true;
		}
	}

	public static void Reset()
	{
		if (enabled)
		{
			SetWindowLongPtr(hMainWindow, -4, unityWndProcHandler);
			hMainWindow = new HandleRef(null, IntPtr.Zero);
			unityWndProcHandler = IntPtr.Zero;
			customWndProcHandler = IntPtr.Zero;
			procDelegate = null;
			width = 0;
			height = 0;
			enabled = false;
		}
	}

	private static IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
	{
		if (msg != 36)
		{
			return CallWindowProc(unityWndProcHandler, hWnd, msg, wParam, lParam);
		}
		Minmaxinfo structure = (Minmaxinfo)Marshal.PtrToStructure(lParam, typeof(Minmaxinfo));
		structure.ptMinTrackSize = new Point
		{
			x = width,
			y = height
		};
		Marshal.StructureToPtr(structure, lParam, fDeleteOld: false);
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll", EntryPoint = "CallWindowProcA")]
	private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll", EntryPoint = "DefWindowProcA")]
	private static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

	private static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
	{
		if (IntPtr.Size == 8)
		{
			return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}
		return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
	}

	[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
	private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
	private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
}
