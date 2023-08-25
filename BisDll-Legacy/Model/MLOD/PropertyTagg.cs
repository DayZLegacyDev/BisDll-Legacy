using System;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x0200003A RID: 58
	public class PropertyTagg : Tagg
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00008338 File Offset: 0x00006538
		public void Read(BinaryReaderEx input)
		{
			this.name = input.ReadAscii(64);
			this.value = input.ReadAscii(64);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008358 File Offset: 0x00006558
		public void Write(BinaryWriterEx output)
		{
			output.Write(true);
			output.writeAsciiz(base.Name);
			output.Write(base.DataSize);
			output.writeAscii(this.name, 64U);
			output.writeAscii(this.value, 64U);
		}

		// Token: 0x040001B2 RID: 434
		public string name;

		// Token: 0x040001B3 RID: 435
		public string value;
	}
}
