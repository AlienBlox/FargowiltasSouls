// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraTrapper
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraTrapper : PlanteraSnatchers
  {
    public virtual string Texture => FargoSoulsUtil.VanillaTextureNPC(175);

    public override string VineTexture => "Terraria/Images/Chains_1";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = Main.npcFrameCount[43];
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }
  }
}
