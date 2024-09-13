// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Spirit.SpiritCrossBoneReverse
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Spirit
{
  public class SpiritCrossBoneReverse : SpiritCrossBone
  {
    public override string Texture => "Terraria/Images/Projectile_532";

    public virtual void AI()
    {
      base.AI();
      ((Entity) this.Projectile).velocity.Y -= 0.4f;
    }
  }
}
