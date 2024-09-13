// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Gittle.RoombaPetProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Gittle
{
  public class RoombaPetProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 16;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults((int) sbyte.MaxValue);
      this.AIType = (int) sbyte.MaxValue;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].turtle = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.dead)
        modPlayer.RoombaPet = false;
      if (modPlayer.RoombaPet)
        this.Projectile.timeLeft = 2;
      int index = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X - (float) (((Entity) this.Projectile).direction * (((Entity) this.Projectile).width / 2)), ((Entity) this.Projectile).Center.Y + (float) (((Entity) this.Projectile).height / 2)), ((Entity) this.Projectile).width, 6, 76, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index].noGravity = true;
      Dust dust = Main.dust[index];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
      Main.dust[index].noLight = true;
    }
  }
}
