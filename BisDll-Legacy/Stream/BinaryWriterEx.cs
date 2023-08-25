namespace BisDll.Stream
{
	public class BinaryWriterEx: BinaryWriter
	{
		public long Position
		{
			get
			{
				return this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = value;
			}
		}
		public BinaryWriterEx( System.IO.Stream dstStream ) : base(dstStream)
		{
		}

		public void writeAscii(string text, uint len)
		{
			this.Write(text.ToCharArray());
			uint num = (uint)((ulong)len - (ulong)((long)text.Length));
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.Write('\0');
				num2++;
			}
		}

		public void writeAsciiz(string text)
		{
			this.Write(text.ToCharArray());
			this.Write('\0');
		}
	}
}
