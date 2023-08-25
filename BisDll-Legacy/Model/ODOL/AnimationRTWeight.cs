using System;

namespace BisDll.Model.ODOL
{
	// Token: 0x02000012 RID: 18
	public class AnimationRTWeight : VerySmallArray
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003FAC File Offset: 0x000021AC
		public AnimationRTPair[] AnimationRTPairs
		{
			get
			{
				AnimationRTPair[] array = new AnimationRTPair[this.nSmall];
				for (int i = 0; i < this.nSmall; i++)
				{
					array[i] = new AnimationRTPair(this.smallSpace[i * 2], this.smallSpace[i * 2 + 1]);
				}
				return array;
			}
		}
	}
}
