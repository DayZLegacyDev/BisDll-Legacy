using System;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000027 RID: 39
	public class StageTexture
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public void read(BinaryReaderEx input, uint matVersion)
		{
			if (matVersion >= 5U)
			{
				this.textureFilter = (StageTexture.TextureFilterType)input.ReadUInt32();
			}
			this.texture = input.ReadAsciiz();
			if (matVersion >= 8U)
			{
				this.stageID = input.ReadUInt32();
			}
			if (matVersion >= 11U)
			{
				this.useWorldEnvMap = input.ReadBoolean();
			}
		}

		// Token: 0x04000186 RID: 390
		public StageTexture.TextureFilterType textureFilter;

		// Token: 0x04000187 RID: 391
		public string texture;

		// Token: 0x04000188 RID: 392
		public uint stageID;

		// Token: 0x04000189 RID: 393
		public bool useWorldEnvMap;

		// Token: 0x02000063 RID: 99
		public enum TextureFilterType
		{
			// Token: 0x040002E9 RID: 745
			Point,
			// Token: 0x040002EA RID: 746
			Linear,
			// Token: 0x040002EB RID: 747
			Triliniear,
			// Token: 0x040002EC RID: 748
			Anisotropic
		}
	}
}
