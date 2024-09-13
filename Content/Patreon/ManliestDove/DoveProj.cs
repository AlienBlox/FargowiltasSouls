// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.ManliestDove.DoveProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.ManliestDove
{
  public class DoveProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 5;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(208);
      this.AIType = 208;
      ((Entity) this.Projectile).height = 22;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].parrot = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.dead)
        modPlayer.DovePet = false;
      if (!modPlayer.DovePet)
        return;
      this.Projectile.timeLeft = 2;
    }
  }
}
