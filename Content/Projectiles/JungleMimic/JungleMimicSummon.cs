// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.JungleMimicSummon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class JungleMimicSummon : ModProjectile
  {
    public int counter;
    public bool trailbehind;

    public virtual void SetStaticDefaults()
    {
      Main.projPet[this.Projectile.type] = true;
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.minion = true;
      this.Projectile.minionSlots = 2f;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = 26;
      ((Entity) this.Projectile).width = 52;
      ((Entity) this.Projectile).height = 56;
      this.AIType = 266;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 15;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual bool MinionContactDamage() => true;

    public virtual bool PreAI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active)
        player.ClearBuff(ModContent.BuffType<JungleMimicSummonBuff>());
      if (player.HasBuff(ModContent.BuffType<JungleMimicSummonBuff>()))
        this.Projectile.timeLeft = 2;
      ++this.counter;
      if (this.counter % 15 == 0 && this.Projectile.owner == Main.myPlayer)
      {
        NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f, true), Array.Empty<int>());
        if (npc != null)
        {
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center);
          SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Addition(Vector2.op_Multiply(vector2, 14f), Vector2.op_Division(((Entity) npc).velocity, 2f)), ModContent.ProjectileType<JungleMimicSummonCoin>(), this.Projectile.damage / 4, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if (this.counter > 180)
      {
        if (this.counter > 300)
          this.counter = 0;
        if (this.Projectile.owner == Main.myPlayer)
        {
          NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f, true), Array.Empty<int>());
          if (npc != null)
          {
            ++this.Projectile.frameCounter;
            this.trailbehind = true;
            if (this.Projectile.frameCounter > 8)
            {
              ++this.Projectile.frame;
              if (this.Projectile.frame > 5)
                this.Projectile.frame = 2;
            }
            for (int index = 0; index < 1000; ++index)
            {
              if (index != ((Entity) this.Projectile).whoAmI && ((Entity) Main.projectile[index]).active && Main.projectile[index].owner == this.Projectile.owner && Main.projectile[index].type == this.Projectile.type && (double) Math.Abs(((Entity) this.Projectile).position.X - ((Entity) Main.projectile[index]).position.X) + (double) Math.Abs(((Entity) this.Projectile).position.Y - ((Entity) Main.projectile[index]).position.Y) < (double) ((Entity) this.Projectile).width)
              {
                if ((double) ((Entity) this.Projectile).position.X < (double) ((Entity) Main.projectile[index]).position.X)
                  ((Entity) this.Projectile).velocity.X -= 0.05f;
                else
                  ((Entity) this.Projectile).velocity.X += 0.05f;
                if ((double) ((Entity) this.Projectile).position.Y < (double) ((Entity) Main.projectile[index]).position.Y)
                  ((Entity) this.Projectile).velocity.Y -= 0.05f;
                else
                  ((Entity) this.Projectile).velocity.Y += 0.05f;
              }
            }
            ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 18f), 0.03f);
            this.Projectile.rotation = 0.0f;
            this.Projectile.tileCollide = false;
            ((Entity) this.Projectile).direction = Math.Sign(((Entity) this.Projectile).velocity.X);
            this.Projectile.spriteDirection = -((Entity) this.Projectile).direction;
            return false;
          }
        }
      }
      this.trailbehind = false;
      this.Projectile.tileCollide = true;
      return true;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type] && this.trailbehind; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(this.Projectile.GetAlpha(lightColor), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY - 4f)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
