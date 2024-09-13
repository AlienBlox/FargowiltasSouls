// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantHeal
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantHeal : ModProjectile, IPixelatedPrimitiveRenderer
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 20;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 22;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 1800;
      this.Projectile.scale = 0.8f;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        if ((double) this.Projectile.localAI[1] == 0.0)
        {
          this.Projectile.localAI[1] = Utils.NextFloat(Main.rand, MathHelper.ToRadians(1f)) * (Utils.NextBool(Main.rand) ? 1f : -1f);
          this.Projectile.netUpdate = true;
        }
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) this.Projectile.localAI[1], new Vector2()), ((Vector2) ref ((Entity) this.Projectile).velocity).Length() - this.Projectile.ai[1]);
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 0.0099999997764825821)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) this.Projectile.localAI[0] == 1.0)
      {
        for (int index = 0; index < 2; ++index)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
          Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 5f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 29f), vector2), (double) this.Projectile.localAI[1] * 3.0, new Vector2()), 30f);
        }
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > 4.5)
        {
          this.Projectile.localAI[0] = 2f;
          this.Projectile.netUpdate = true;
          this.Projectile.timeLeft = 360;
        }
      }
      else
      {
        this.Projectile.extraUpdates = 1;
        int index1 = (int) Math.Abs(this.Projectile.ai[0]);
        bool flag = (double) this.Projectile.ai[0] < 0.0;
        if (flag)
        {
          --index1;
          if (WorldSavingSystem.MasochistModeReal)
          {
            this.Projectile.Kill();
            return;
          }
        }
        if (index1 < 0 || (flag ? (index1 >= (int) byte.MaxValue || !((Entity) Main.player[index1]).active || Main.player[index1].ghost ? 1 : (Main.player[index1].dead ? 1 : 0)) : (index1 >= Main.maxNPCs ? 1 : (!((Entity) Main.npc[index1]).active ? 1 : 0))) != 0)
        {
          this.Projectile.Kill();
          return;
        }
        Entity entity = flag ? (Entity) (object) Main.player[index1] : (Entity) (object) Main.npc[index1];
        if ((double) ((Entity) this.Projectile).Distance(entity.Center) < 5.0)
        {
          if (flag)
          {
            if (((Entity) Main.player[index1]).whoAmI == Main.myPlayer)
            {
              Main.player[index1].ClearBuff(ModContent.BuffType<MutantFangBuff>());
              Main.player[index1].statLife += this.Projectile.damage;
              Main.player[index1].HealEffect(this.Projectile.damage, true);
              if (Main.player[index1].statLife > Main.player[index1].statLifeMax2)
                Main.player[index1].statLife = Main.player[index1].statLifeMax2;
              this.Projectile.Kill();
            }
          }
          else if (FargoSoulsUtil.HostCheck)
          {
            Main.npc[index1].life += this.Projectile.damage;
            Main.npc[index1].HealEffect(this.Projectile.damage, true);
            if (Main.npc[index1].life > Main.npc[index1].lifeMax || Main.npc[index1].life < 0)
              Main.npc[index1].life = Main.npc[index1].lifeMax;
            Main.npc[index1].netUpdate = true;
            this.Projectile.Kill();
          }
        }
        else
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, entity.Center), 5f);
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 29f), vector2), 30f);
          }
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(Vector2.op_Subtraction(entity.position, entity.oldPosition), 2f));
        }
        for (int index3 = 0; index3 < 3; ++index3)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        }
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color(51, (int) byte.MaxValue, 191, 210), this.Projectile.Opacity), 0.8f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(), FargoSoulsUtil.AprilFools ? Color.Orange : Color.Cyan, this.Projectile.rotation, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.8999999761581421), 3.5f, completionRatio);
    }

    public static Color ColorFunction(float completionRatio)
    {
      return Color.op_Multiply(Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Orange : Color.Cyan, Color.Transparent, completionRatio), 0.7f);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.Projectile.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), MutantHeal.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (MutantHeal.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__11_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(25));
    }
  }
}
