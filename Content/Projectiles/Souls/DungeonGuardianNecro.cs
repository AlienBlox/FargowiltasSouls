// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.DungeonGuardianNecro
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class DungeonGuardianNecro : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_197";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = 0;
      this.AIType = 14;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 1000;
    }

    public virtual void AI()
    {
      this.Projectile.rotation += 0.2f;
      int num = (int) this.Projectile.ai[1];
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= (double) num)
        return;
      this.Projectile.ai[0] = (float) num;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f, prioritizeBoss: true), Array.Empty<int>());
      if (!npc.Alive())
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 45f), 0.06666667f);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if (Main.player[this.Projectile.owner].FargoSouls().TerrariaSoul)
        ((NPC.HitModifiers) ref modifiers).SetCrit();
      else
        ((NPC.HitModifiers) ref modifiers).DisableCrit();
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-20, 20), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-20, 20)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
    }
  }
}
