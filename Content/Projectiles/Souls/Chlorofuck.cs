// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.Chlorofuck
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class Chlorofuck : ModProjectile
  {
    public const float Cooldown = 50f;

    public virtual void SetStaticDefaults()
    {
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 18000;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      player.FargoSouls();
      if (((Entity) player).whoAmI == Main.myPlayer && (player.dead || !player.HasEffect<ChloroMinion>()))
      {
        this.Projectile.Kill();
        this.Projectile.netUpdate = true;
      }
      else
      {
        this.Projectile.netUpdate = true;
        this.Projectile.scale = (float) ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.2f + 0.95f;
        float num1 = 75f;
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.4f, 0.2f);
        ((Entity) this.Projectile).position = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(num1, 0.0f), (double) this.Projectile.ai[1], new Vector2()));
        ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
        this.Projectile.ai[1] -= 0.03f;
        if ((double) this.Projectile.ai[1] > 3.1415927410125732)
        {
          this.Projectile.ai[1] -= 6.28318548f;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.rotation = this.Projectile.ai[1] + 1.57079637f;
        if ((double) this.Projectile.ai[0] != 0.0)
        {
          --this.Projectile.ai[0];
        }
        else
        {
          float x = ((Entity) this.Projectile).position.X;
          float y = ((Entity) this.Projectile).position.Y;
          bool flag = false;
          NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 700f, true, new Vector2()), Array.Empty<int>());
          if (npc != null)
          {
            x = ((Entity) npc).Center.X;
            y = ((Entity) npc).Center.Y;
            double num2 = (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center);
            flag = true;
          }
          if (flag)
          {
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector(((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width * 0.5f, ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height * 0.5f);
            float num3 = x - vector2.X;
            float num4 = y - vector2.Y;
            float num5 = 10f / (float) Math.Sqrt((double) num3 * (double) num3 + (double) num4 * (double) num4);
            float num6 = num3 * num5;
            float num7 = num4 * num5;
            if (this.Projectile.owner == Main.myPlayer)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(num6, num7), 227, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            this.Projectile.ai[0] = 50f;
          }
          if (Main.netMode != 2)
            return;
          this.Projectile.netUpdate = true;
        }
      }
    }
  }
}
