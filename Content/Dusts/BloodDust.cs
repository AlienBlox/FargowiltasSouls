// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Dusts.BloodDust
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Dusts
{
  internal class BloodDust : ModDust
  {
    public virtual void OnSpawn(Dust dust)
    {
      Dust dust1 = dust;
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.4f);
      dust.noGravity = true;
      dust.scale *= 0.5f;
      dust.frame = new Rectangle(0, 0, 10, 10);
    }

    public virtual bool Update(Dust dust)
    {
      float num = dust.scale * 0.6f;
      Lighting.AddLight(dust.position, num * 1.68f, num * 0.08f, num * 0.67f);
      return true;
    }
  }
}
