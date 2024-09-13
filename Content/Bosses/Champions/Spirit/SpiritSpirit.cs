// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Spirit.SpiritSpirit
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Spirit
{
  public class SpiritSpirit : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 15;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.hostile = true;
      this.Projectile.scale = 0.8f;
    }

    public virtual void AI()
    {
      if ((double) --this.Projectile.ai[1] < 0.0 && (double) this.Projectile.ai[1] > -300.0)
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<SpiritChampion>());
        if (npc != null)
        {
          Player player = Main.player[npc.target];
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 200.0 && (double) npc.ai[0] == 3.0)
          {
            for (int index = 0; index < 3; ++index)
            {
              Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 2.2f);
              ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 29f), vector2), 30f);
            }
          }
          else
            this.Projectile.ai[1] = -300f;
        }
        else
          this.Projectile.ai[0] = (float) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      }
      else if ((double) this.Projectile.ai[1] < -300.0 && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 2.2000000476837158)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.022f);
      }
      for (int index = 0; index < 3; ++index)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360, true, false);
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Vector2.op_Division(Utils.Size(new Rectangle(0, num1 * this.Projectile.frame, texture2D1.Width, num1)), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.2f)
      {
        Player player = Main.player[this.Projectile.owner];
        Texture2D texture2D2 = texture2D1;
        Color color = Color.op_Multiply(alpha, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num2 = this.Projectile.scale * (((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          Vector2 vector2_1 = Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0));
          double num3 = (double) index1 % 1.0 * 3.1415927410125732 / 6.8499999046325684;
          Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D2), 2f), num2, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }
  }
}
