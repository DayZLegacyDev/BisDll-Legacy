using System;
using BisDll.Stream;

namespace BisDll.Common
{
	// Token: 0x02000043 RID: 67
	public static class Logging_Functions
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009648 File Offset: 0x00007848
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00009650 File Offset: 0x00007850
		public static bool Verbose { get; set; }

		// Token: 0x060001DD RID: 477 RVA: 0x00009658 File Offset: 0x00007858
		public static void Echo(string message)
		{
			if (!Logging_Functions.Verbose)
			{
				return;
			}
			Console.WriteLine(message);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000966C File Offset: 0x0000786C
		public static void Echo(BinaryReaderEx input, int variable, string name, bool hex)
		{
			if (!Logging_Functions.Verbose)
			{
				return;
			}
			string text = string.Concat(new string[]
			{
				name,
				": ",
				variable.ToString(),
				" Position: ",
				input.Position.ToString()
			});
			if (hex)
			{
				text = text + " Hex: " + variable.ToString("X4");
			}
			Console.WriteLine(text);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000096E8 File Offset: 0x000078E8
		public static void Echo(BinaryReaderEx input, uint variable, string name, bool hex)
		{
			if (!Logging_Functions.Verbose)
			{
				return;
			}
			string text = string.Concat(new string[]
			{
				name,
				": ",
				variable.ToString(),
				" Position: ",
				input.Position.ToString()
			});
			if (hex)
			{
				text = text + " Hex: " + variable.ToString("X4");
			}
			Console.WriteLine(text);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009764 File Offset: 0x00007964
		public static void Echo<T>(BinaryReaderEx input, T variable, string name)
		{
			if (!Logging_Functions.Verbose)
			{
				return;
			}
			string[] array = new string[5];
			array[0] = name;
			array[1] = ": ";
			int num = 2;
			T t = variable;
			array[num] = ((t != null) ? t.ToString() : null);
			array[3] = " Position: ";
			array[4] = input.Position.ToString();
			Console.WriteLine(string.Concat(array));
		}
	}
}
