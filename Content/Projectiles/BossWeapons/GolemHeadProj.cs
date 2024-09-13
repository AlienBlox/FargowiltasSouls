// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.GolemHeadProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  internal class GolemHeadProj : ModProjectile
  {
    private int headsStacked;
    private const int maxHeadsStacked = 10;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 180;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.hide = true;
      this.Projectile.scale *= 0.75f;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.headsStacked);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.headsStacked = reader.Read7BitEncodedInt();
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.ai[0] = -1f;
        foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == this.Projectile.type && p.owner == this.Projectile.owner && (double) p.ai[1] == 0.0 && ((Entity) p).whoAmI != ((Entity) this.Projectile).whoAmI)))
          ++this.headsStacked;
        if (this.headsStacked == 9)
        {
          SoundEngine.PlaySound(ref SoundID.NPCHit41, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          FargoSoulsUtil.DustRing(((Entity) this.Projectile).Center, 96, 87, 12f, new Color(), 2f);
        }
        if (this.headsStacked >= 10 && this.Projectile.owner == Main.myPlayer)
        {
          this.headsStacked = 0;
          this.Projectile.ai[1] = 1000f;
          this.Projectile.localAI[0] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.MouseWorld));
          this.Projectile.netUpdate = true;
        }
      }
      if ((double) this.Projectile.ai[1] >= 0.0)
      {
        ++this.Projectile.timeLeft;
        Player player = Main.player[this.Projectile.owner];
        Projectile projectile1 = this.Projectile;
        ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition), 0.9f));
        if (Vector2.op_Inequality(((Entity) this.Projectile).Center, ((Entity) player).Center))
        {
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) < 120.0)
          {
            Projectile projectile2 = this.Projectile;
            ((Entity) projectile2).velocity = Vector2.op_Addition(((Entity) projectile2).velocity, Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) player).Center), 0.75f));
          }
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 96.0)
            ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(24f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center)), 0.06666667f);
        }
        foreach (Projectile projectile3 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == this.Projectile.type && ((Entity) p).whoAmI != ((Entity) this.Projectile).whoAmI && (double) ((Entity) p).Distance(((Entity) this.Projectile).Center) < (double) ((Entity) this.Projectile).width)))
        {
          ((Entity) this.Projectile).velocity.X += (float) (0.10000000149011612 * ((double) ((Entity) this.Projectile).position.X < (double) ((Entity) projectile3).position.X ? -1.0 : 1.0));
          ((Entity) this.Projectile).velocity.Y += (float) (0.10000000149011612 * ((double) ((Entity) this.Projectile).position.Y < (double) ((Entity) projectile3).position.Y ? -1.0 : 1.0));
          ((Entity) projectile3).velocity.X += (float) (0.10000000149011612 * ((double) ((Entity) projectile3).position.X < (double) ((Entity) this.Projectile).position.X ? -1.0 : 1.0));
          ((Entity) projectile3).velocity.Y += (float) (0.10000000149011612 * ((double) ((Entity) projectile3).position.Y < (double) ((Entity) this.Projectile).position.Y ? -1.0 : 1.0));
        }
        if ((double) this.Projectile.ai[1] == 0.0 && this.Projectile.owner == Main.myPlayer && (player.dead || player.ghost || !player.controlUseItem || player.HeldItem.type != ModContent.ItemType<GolemTome2>()))
        {
          this.Projectile.ai[1] = 1f;
          this.Projectile.localAI[0] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, Main.MouseWorld));
          this.Projectile.netUpdate = true;
        }
        if ((double) this.Projectile.ai[1] > 0.0)
        {
          Projectile projectile4 = this.Projectile;
          ((Entity) projectile4).velocity = Vector2.op_Multiply(((Entity) projectile4).velocity, 0.97f);
          if ((double) ++this.Projectile.ai[1] > (double) ((player.ownedProjectileCounts[this.Projectile.type] - this.headsStacked) * 4))
          {
            SoundEngine.PlaySound(ref SoundID.NPCHit41, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (this.Projectile.owner == Main.myPlayer)
            {
              this.Projectile.damage = (int) ((double) this.Projectile.damage * (1.0 + 2.0 * (double) this.headsStacked / 10.0));
              this.Projectile.ai[1] = -1f;
              ((Entity) this.Projectile).velocity = Vector2.op_Multiply(24f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, Main.MouseWorld));
              this.Projectile.netUpdate = true;
            }
          }
        }
      }
      else
      {
        if (!this.Projectile.tileCollide)
        {
          this.Projectile.tileCollide = !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
          if (this.Projectile.tileCollide)
          {
            ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
            ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width / 0.75);
            ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height / 0.75);
            this.Projectile.scale /= 0.75f;
            ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
          }
        }
        if ((double) this.Projectile.ai[0] == -1.0)
        {
          if ((double) ++this.Projectile.localAI[1] > (double) (25 - this.headsStacked * 2))
          {
            this.Projectile.localAI[1] = 0.0f;
            this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, (float) (400.0 + 80.0 * (double) this.headsStacked), true);
            this.Projectile.netUpdate = true;
          }
        }
        else
        {
          ++this.Projectile.timeLeft;
          NPC npc = Main.npc[(int) this.Projectile.ai[0]];
          if (((Entity) npc).active && npc.CanBeChasedBy((object) null, false))
          {
            ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 48f), 0.06666667f * (float) (0.75 + 0.75 * (double) this.headsStacked / 10.0));
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.localAI[1] = 0.0f;
            this.Projectile.netUpdate = true;
          }
        }
      }
      if ((double) this.Projectile.ai[1] >= 0.0)
        this.Projectile.rotation += 0.2f * (float) ((Entity) Main.player[this.Projectile.owner]).direction;
      else
        this.Projectile.rotation += 0.3f * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = ((Entity) this.Projectile).width / 4;
      height = ((Entity) this.Projectile).height / 4;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void OnKill(int timeLeft)
    {
      if (timeLeft > 0)
      {
        this.Projectile.timeLeft = 0;
        this.Projectile.penetrate = -1;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = 300;
        ((Entity) this.Projectile).height = 300;
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        this.Projectile.Damage();
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 45; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 3; ++index6)
      {
        float num = 0.4f;
        if (index6 == 1)
          num = 0.8f;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
        int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index8];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index8].velocity.X;
        ++Main.gore[index8].velocity.Y;
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index9];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index9].velocity.X;
        --Main.gore[index9].velocity.Y;
        int index10 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index10];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index10].velocity.X;
        --Main.gore[index10].velocity.Y;
      }
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num1 = Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type] < 16 ? 8 : 4;
      for (int index11 = 0; index11 < num1; ++index11)
      {
        int index12 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), 6.2831854820251465 / (double) num1 * ((double) index11 + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)), new Vector2()), Utils.NextFloat(Main.rand, 12f, 20f)), ModContent.ProjectileType<GolemGib>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, (float) (Main.rand.Next(11) + 1), 0.0f);
        if (index12 != Main.maxProjectiles)
          Main.projectile[index12].timeLeft = Main.rand.Next(45, 90) * 2;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Color color1 = Color.op_Multiply(Color.Orange, (float) ((double) this.Projectile.Opacity * (double) this.headsStacked / 10.0));
      ((Color) ref color1).A = (byte) 20;
      if ((double) this.Projectile.ai[1] == 0.0)
        color1 = Color.op_Multiply(color1, 0.5f);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color2 = color1;
        float num3 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color3 = Color.op_Multiply(color2, num3 * num3);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num4 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color3, num4, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      if ((double) this.Projectile.ai[1] < 0.0)
      {
        Texture2D texture2D2 = TextureAssets.Golem[1].Value;
        Rectangle rectangle2;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle2).\u002Ector(0, texture2D2.Height / 2, texture2D2.Width, texture2D2.Height / 2);
        Vector2 vector2_3 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
        vector2_3.Y -= 4f;
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2_3, this.Projectile.scale, spriteEffects, 0.0f);
      }
      return false;
    }
  }
}
