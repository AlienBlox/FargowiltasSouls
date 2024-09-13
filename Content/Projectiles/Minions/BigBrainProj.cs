// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.BigBrainProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class BigBrainProj : ModProjectile
  {
    public const int MaxMinionSlots = 16;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 12;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[this.Type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
      this.Projectile.minionSlots = 1f;
      this.Projectile.timeLeft = 18000;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.minion = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.BigBrainMinion = false;
      if (fargoSoulsPlayer.BigBrainMinion)
        this.Projectile.timeLeft = 2;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= 8)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = (this.Projectile.frame + 1) % 12;
      }
      float num1 = 2.28571439f * Math.Min(this.Projectile.minionSlots / 16f, 1f);
      this.Projectile.ai[0] += (float) (0.10000000149011612 + 0.30000001192092896 * (double) num1);
      this.Projectile.alpha = (int) (Math.Cos((double) this.Projectile.ai[0] / 0.40000000596046448 * 6.2831854820251465 / 180.0) * 60.0) + 60;
      float scale = this.Projectile.scale;
      this.Projectile.scale = (float) (0.75 + 0.5 * (double) num1);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale / (double) scale);
      ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale / (double) scale);
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: Main.player[this.Projectile.owner].MountedCenter), Array.Empty<int>());
      if (npc != null && (double) ++this.Projectile.localAI[0] > 5.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        if (this.Projectile.owner == Main.myPlayer)
        {
          int num2 = (int) ((double) this.Projectile.damage * (double) this.Projectile.scale);
          int num3 = ModContent.ProjectileType<BigBrainIllusion>();
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Utils.RotatedBy(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) Main.player[this.Projectile.owner]).Center), 1.5707963705062866 * (double) Main.rand.Next(4), new Vector2()));
          Vector2 vector2_2 = Vector2.op_Multiply(18f, Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) npc).velocity, 15f)), vector2_1)));
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, vector2_2, num3, num2, this.Projectile.knockBack, this.Projectile.owner, this.Projectile.scale, 0.0f, 0.0f);
        }
      }
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(0.0f, (float) (200 + this.Projectile.alpha) * this.Projectile.scale), (double) this.Projectile.ai[1] + (double) this.Projectile.ai[0] / 6.2831854820251465, new Vector2()));
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, this.Projectile.scale);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 6;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      for (int index = 0; index <= 3; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Utils.RotatedBy(new Vector2(0.0f, (float) (200 + this.Projectile.alpha) * this.Projectile.scale), (double) index * 1.5707963705062866 + (double) this.Projectile.ai[1] + (double) this.Projectile.ai[0] / 6.2831854820251465, new Vector2()));
        int num1 = (int) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width);
        int num2 = (int) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).height);
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector((int) vector2.X - num1 / 2, (int) vector2.Y - num2 / 2, num1, num2);
        if (((Rectangle) ref rectangle).Intersects(targetHitbox))
          return new bool?(true);
      }
      return new bool?(false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num = texture2D.Height / Main.projFrames[this.Projectile.type];
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, this.Projectile.frame * num, texture2D.Width, num);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, Vector2.op_Division(Utils.Size(rectangle), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      for (int index = 1; index <= 3; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Utils.RotatedBy(new Vector2(0.0f, (float) (200 + this.Projectile.alpha) * this.Projectile.scale), (double) index * 1.5707963705062866 + (double) this.Projectile.ai[1] + (double) this.Projectile.ai[0] / 6.2831854820251465, new Vector2()));
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(lightColor, this.Projectile.Opacity), this.Projectile.Opacity), this.Projectile.Opacity), (float) Main.mouseTextColor / (float) byte.MaxValue);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2, Main.screenPosition), new Rectangle?(rectangle), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(rectangle), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
