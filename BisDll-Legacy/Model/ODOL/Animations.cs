using System;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000013 RID: 19
	public class Animations
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004008 File Offset: 0x00002208
		public void Read(BinaryReaderEx input)
		{
			this.animationClasses = input.ReadArray<Animations.AnimationClass>();
			int num = this.animationClasses.Length;
			this.nAnimLODs = input.ReadInt32();
			this.Bones2Anims = new uint[this.nAnimLODs][][];
			for (int i = 0; i < this.nAnimLODs; i++)
			{
				uint num2 = input.ReadUInt32();
				this.Bones2Anims[i] = new uint[num2][];
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					uint num4 = input.ReadUInt32();
					this.Bones2Anims[i][num3] = new uint[num4];
					int num5 = 0;
					while ((long)num5 < (long)((ulong)num4))
					{
						this.Bones2Anims[i][num3][num5] = input.ReadUInt32();
						num5++;
					}
					num3++;
				}
			}
			this.Anims2Bones = new int[this.nAnimLODs][];
			this.axisData = new Vector3P[this.nAnimLODs][][];
			for (int j = 0; j < this.nAnimLODs; j++)
			{
				this.Anims2Bones[j] = new int[num];
				this.axisData[j] = new Vector3P[num][];
				for (int k = 0; k < num; k++)
				{
					this.Anims2Bones[j][k] = input.ReadInt32();
					if (this.Anims2Bones[j][k] != -1 && this.animationClasses[k].animType != Animations.AnimationClass.AnimType.Direct && this.animationClasses[k].animType != Animations.AnimationClass.AnimType.Hide)
					{
						this.axisData[j][k] = new Vector3P[2];
						this.axisData[j][k][0] = new Vector3P(input);
						this.axisData[j][k][1] = new Vector3P(input);
					}
				}
			}
		}

		// Token: 0x04000071 RID: 113
		public Animations.AnimationClass[] animationClasses;

		// Token: 0x04000072 RID: 114
		public int nAnimLODs;

		// Token: 0x04000073 RID: 115
		public uint[][][] Bones2Anims;

		// Token: 0x04000074 RID: 116
		public int[][] Anims2Bones;

		// Token: 0x04000075 RID: 117
		public Vector3P[][][] axisData;

		// Token: 0x0200005A RID: 90
		public class AnimationClass : IDeserializable
		{
			// Token: 0x06000276 RID: 630 RVA: 0x0000B490 File Offset: 0x00009690
			public void ReadObject(BinaryReaderEx input)
			{
				int version = input.Version;
				this.animType = (Animations.AnimationClass.AnimType)input.ReadUInt32();
				this.animName = input.ReadAsciiz();
				this.animSource = input.ReadAsciiz();
				this.minPhase = input.ReadSingle();
				this.maxPhase = input.ReadSingle();
				this.minValue = input.ReadSingle();
				this.maxValue = input.ReadSingle();
				if (version >= 56)
				{
					this.animPeriod = input.ReadSingle();
					this.initPhase = input.ReadSingle();
				}
				this.sourceAddress = (Animations.AnimationClass.AnimAddress)input.ReadUInt32();
				switch (this.animType)
				{
				case Animations.AnimationClass.AnimType.Rotation:
				case Animations.AnimationClass.AnimType.RotationX:
				case Animations.AnimationClass.AnimType.RotationY:
				case Animations.AnimationClass.AnimType.RotationZ:
					this.angle0 = input.ReadSingle();
					this.angle1 = input.ReadSingle();
					return;
				case Animations.AnimationClass.AnimType.Translation:
				case Animations.AnimationClass.AnimType.TranslationX:
				case Animations.AnimationClass.AnimType.TranslationY:
				case Animations.AnimationClass.AnimType.TranslationZ:
					this.offset0 = input.ReadSingle();
					this.offset1 = input.ReadSingle();
					return;
				case Animations.AnimationClass.AnimType.Direct:
					this.axisPos = new Vector3P(input);
					this.axisDir = new Vector3P(input);
					this.angle = input.ReadSingle();
					this.axisOffset = input.ReadSingle();
					return;
				case Animations.AnimationClass.AnimType.Hide:
					this.hideValue = input.ReadSingle();
					if (version >= 55)
					{
						input.ReadSingle();
						return;
					}
					return;
				default:
					throw new Exception("Unknown AnimType encountered: " + this.animType.ToString());
				}
			}

			// Token: 0x04000211 RID: 529
			public Animations.AnimationClass.AnimType animType;

			// Token: 0x04000212 RID: 530
			public string animName;

			// Token: 0x04000213 RID: 531
			public string animSource;

			// Token: 0x04000214 RID: 532
			public float minValue;

			// Token: 0x04000215 RID: 533
			public float maxValue;

			// Token: 0x04000216 RID: 534
			public float minPhase;

			// Token: 0x04000217 RID: 535
			public float maxPhase;

			// Token: 0x04000218 RID: 536
			public float animPeriod;

			// Token: 0x04000219 RID: 537
			public float initPhase;

			// Token: 0x0400021A RID: 538
			public Animations.AnimationClass.AnimAddress sourceAddress;

			// Token: 0x0400021B RID: 539
			public float angle0;

			// Token: 0x0400021C RID: 540
			public float angle1;

			// Token: 0x0400021D RID: 541
			public float offset0;

			// Token: 0x0400021E RID: 542
			public float offset1;

			// Token: 0x0400021F RID: 543
			public Vector3P axisPos;

			// Token: 0x04000220 RID: 544
			public Vector3P axisDir;

			// Token: 0x04000221 RID: 545
			public float angle;

			// Token: 0x04000222 RID: 546
			public float axisOffset;

			// Token: 0x04000223 RID: 547
			public float hideValue;

			// Token: 0x02000068 RID: 104
			public enum AnimType
			{
				// Token: 0x04000301 RID: 769
				Rotation,
				// Token: 0x04000302 RID: 770
				RotationX,
				// Token: 0x04000303 RID: 771
				RotationY,
				// Token: 0x04000304 RID: 772
				RotationZ,
				// Token: 0x04000305 RID: 773
				Translation,
				// Token: 0x04000306 RID: 774
				TranslationX,
				// Token: 0x04000307 RID: 775
				TranslationY,
				// Token: 0x04000308 RID: 776
				TranslationZ,
				// Token: 0x04000309 RID: 777
				Direct,
				// Token: 0x0400030A RID: 778
				Hide
			}

			// Token: 0x02000069 RID: 105
			public enum AnimAddress
			{
				// Token: 0x0400030C RID: 780
				AnimClamp,
				// Token: 0x0400030D RID: 781
				AnimLoop,
				// Token: 0x0400030E RID: 782
				AnimMirror,
				// Token: 0x0400030F RID: 783
				NAnimAddress
			}
		}
	}
}
