// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MimicTitanGlove
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MimicTitanGlove : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Item_536";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      NPC npc = Main.npc[(int) this.Projectile.ai[1]];
      Player player = Main.player[npc.target];
      ((Entity) this.Projectile).position = Vector2.op_Subtraction(((Entity) npc).Center, new Vector2((float) (((Entity) this.Projectile).width / 2), (float) (((Entity) this.Projectile).height / 2)));
      if ((double) this.Projectile.ai[0] < 60.0)
        this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center)) + 1.57079637f;
      if ((double) this.Projectile.ai[0] > 120.0)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
    }
  }
}
