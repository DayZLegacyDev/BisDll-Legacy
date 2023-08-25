using System;
using System.Globalization;
using BisDll.Stream;

namespace BisDll.Common
{
	// Token: 0x02000042 RID: 66
	public struct ColorP
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009450 File Offset: 0x00007650
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00009458 File Offset: 0x00007658
		public float Red { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009464 File Offset: 0x00007664
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000946C File Offset: 0x0000766C
		public float Green { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009478 File Offset: 0x00007678
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009480 File Offset: 0x00007680
		public float Blue { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000948C File Offset: 0x0000768C
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009494 File Offset: 0x00007694
		public float Alpha { get; private set; }

		// Token: 0x060001D6 RID: 470 RVA: 0x000094A0 File Offset: 0x000076A0
		public ColorP(float r, float g, float b, float a)
		{
			this.Red = r;
			this.Green = g;
			this.Blue = b;
			this.Alpha = a;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000094D0 File Offset: 0x000076D0
		public ColorP(BinaryReaderEx input)
		{
			this.Red = input.ReadSingle();
			this.Green = input.ReadSingle();
			this.Blue = input.ReadSingle();
			this.Alpha = input.ReadSingle();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009514 File Offset: 0x00007714
		public void read(BinaryReaderEx input)
		{
			this.Red = input.ReadSingle();
			this.Green = input.ReadSingle();
			this.Blue = input.ReadSingle();
			this.Alpha = input.ReadSingle();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009558 File Offset: 0x00007758
		public void write(BinaryWriterEx output)
		{
			output.Write(this.Red);
			output.Write(this.Green);
			output.Write(this.Blue);
			output.Write(this.Alpha);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000959C File Offset: 0x0000779C
		public override string ToString()
		{
			CultureInfo cultureInfo = new CultureInfo("en-GB");
			return string.Concat(new string[]
			{
				"{",
				this.Red.ToString(cultureInfo.NumberFormat),
				",",
				this.Green.ToString(cultureInfo.NumberFormat),
				",",
				this.Blue.ToString(cultureInfo.NumberFormat),
				",",
				this.Alpha.ToString(cultureInfo.NumberFormat),
				"}"
			});
		}
	}
}
