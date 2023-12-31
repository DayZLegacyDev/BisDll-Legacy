﻿using System;
using System.Collections.Generic;
using System.Linq;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000025 RID: 37
	public class Section : IDeserializable
	{
		// Token: 0x0600014B RID: 331 RVA: 0x000069D8 File Offset: 0x00004BD8
		public uint[] getFaceIndexes(Polygon[] faces)
		{
			uint num = 0U;
			uint num2 = this.shortIndices ? 8U : 16U;
			uint num3 = this.shortIndices ? 2U : 4U;
			List<uint> list = new List<uint>();
			uint num4 = 0U;
			while ((ulong)num4 < (ulong)((long)faces.Length))
			{
				if ((ulong)num >= (ulong)((long)this.faceLowerIndex) && (ulong)num < (ulong)((long)this.faceUpperIndex))
				{
					list.Add(num4);
				}
				num += num2;
				if (faces[(int)num4].VertexIndices.Length == 4)
				{
					num += num3;
				}
				if ((ulong)num >= (ulong)((long)this.faceUpperIndex))
				{
					break;
				}
				num4 += 1U;
			}
			return list.ToArray();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006A84 File Offset: 0x00004C84
		public void ReadObject(BinaryReaderEx input)
		{
			int version = input.Version;
			this.shortIndices = (version < 69);
			this.faceLowerIndex = input.ReadInt32();
			this.faceUpperIndex = input.ReadInt32();
			this.minBoneIndex = input.ReadInt32();
			this.bonesCount = input.ReadInt32();
			input.ReadUInt32();
			this.textureIndex = input.ReadInt16();
			this.special = input.ReadUInt32();
			this.materialIndex = input.ReadInt32();
			if (this.materialIndex == -1)
			{
				this.mat = input.ReadAsciiz();
			}
			if (version >= 36)
			{
				this.nStages = input.ReadUInt32();
				this.areaOverTex = new float[this.nStages];
				int num = 0;
				while ((long)num < (long)((ulong)this.nStages))
				{
					this.areaOverTex[num] = input.ReadSingle();
					num++;
				}
				if (version >= 67 && input.ReadInt32() >= 1)
				{
					(from _ in Enumerable.Range(0, 11)
					select input.ReadSingle()).ToArray<float>();
					return;
				}
			}
			else
			{
				this.areaOverTex = new float[1];
				this.areaOverTex[0] = input.ReadSingle();
			}
		}

		// Token: 0x04000177 RID: 375
		private int faceLowerIndex;

		// Token: 0x04000178 RID: 376
		private int faceUpperIndex;

		// Token: 0x04000179 RID: 377
		private int minBoneIndex;

		// Token: 0x0400017A RID: 378
		private int bonesCount;

		// Token: 0x0400017B RID: 379
		public short textureIndex;

		// Token: 0x0400017C RID: 380
		public uint special;

		// Token: 0x0400017D RID: 381
		public int materialIndex;

		// Token: 0x0400017E RID: 382
		private string mat;

		// Token: 0x0400017F RID: 383
		private uint nStages;

		// Token: 0x04000180 RID: 384
		private float[] areaOverTex;

		// Token: 0x04000181 RID: 385
		private bool shortIndices;
	}
}
