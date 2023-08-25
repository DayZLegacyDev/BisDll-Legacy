using System;
using System.IO;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000036 RID: 54
	public class MLOD : P3D
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007700 File Offset: 0x00005900
		public override P3D_LOD[] LODs
		{
			get
			{
				return this.lods;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000771C File Offset: 0x0000591C
		public override float Mass
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007724 File Offset: 0x00005924
		public MLOD(string fileName)
		{
			byte[] array = File.ReadAllBytes(fileName);
			BinaryReaderEx binaryReaderEx = new BinaryReaderEx(new MemoryStream(array, 0, array.Length, false, true));
			this.Read(binaryReaderEx);
			binaryReaderEx.Close();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007764 File Offset: 0x00005964
		public MLOD( System.IO.Stream stream )
		{
			this.Read(new BinaryReaderEx(stream));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007778 File Offset: 0x00005978
		public MLOD(MLOD_LOD[] lods)
		{
			this.lods = lods;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007788 File Offset: 0x00005988
		private void Read(BinaryReaderEx input)
		{
			if (input.ReadAscii(4) != "MLOD")
			{
				throw new Exception("MLOD signature expected");
			}
			base.Version = input.ReadUInt32();
			if (base.Version != 257U)
			{
				throw new Exception("Unknown MLOD version");
			}
			uint num = input.ReadUInt32();
			this.lods = new MLOD_LOD[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.lods[num2] = new MLOD_LOD();
				this.lods[num2].Read(input);
				num2++;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007828 File Offset: 0x00005A28
		private void Write(BisDll.Stream.BinaryWriterEx output)
		{
			output.writeAscii("MLOD", 4U);
			output.Write(257);
			output.Write(this.lods.Length);
			for (int i = 0; i < this.lods.Length; i++)
			{
				this.lods[i].Write(output);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007888 File Offset: 0x00005A88
		public bool WriteToFile(string file, bool allowOverwriting = false)
		{
			try
			{
				FileMode mode = (!allowOverwriting) ? FileMode.CreateNew : FileMode.Create;
				BisDll.Stream.BinaryWriterEx BinaryWriterEx = new BisDll.Stream.BinaryWriterEx(new FileStream(file, mode));
				this.Write(BinaryWriterEx);
				BinaryWriterEx.Close();
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
			return true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000078E8 File Offset: 0x00005AE8
		public MemoryStream WriteToMemory()
		{
			MemoryStream memoryStream = new MemoryStream(100000);
			BisDll.Stream.BinaryWriterEx BinaryWriterEx = new BisDll.Stream.BinaryWriterEx(memoryStream);
			this.Write(BinaryWriterEx);
			BinaryWriterEx.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000791C File Offset: 0x00005B1C
		public void WriteToStream( System.IO.Stream stream )
		{
			BisDll.Stream.BinaryWriterEx output = new BisDll.Stream.BinaryWriterEx(stream);
			this.Write(output);
		}

		// Token: 0x040001A9 RID: 425
		private MLOD_LOD[] lods;
	}
}
