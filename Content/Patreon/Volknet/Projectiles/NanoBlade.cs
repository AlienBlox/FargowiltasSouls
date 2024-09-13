// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.NanoBlade
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  internal class NanoBlade : ModProjectile
  {
    private int chainsawSoundTimer;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.hide = true;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.light = 1.1f;
      this.Projectile.scale = 1.75f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().CanSplit = false;
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

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 0 && player.channel)
      {
        this.Projectile.frame = (int) this.Projectile.localAI[0];
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, (int) this.Projectile.localAI[0] * 47, 340, 47);
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        Color color1 = Color.op_Multiply(this.Projectile.GetAlpha(lightColor), Utils.NextFloat(Main.rand, 0.2f, 0.3f));
        for (int index = 0; index < 5; ++index)
        {
          Color color2 = color1;
          Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 8f, 8f);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(vector2, ((Entity) player).Center), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (160.0 * (double) this.Projectile.scale - 24.0))), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, this.Projectile.rotation, Vector2.op_Division(Utils.Size(rectangle), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      float num = 0.0f;
      return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 296f), this.Projectile.scale)), 30f, ref num));
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      target.AddBuff(195, 600, false);
      target.AddBuff(36, 600, false);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 2;
      if (this.chainsawSoundTimer <= 0)
      {
        this.chainsawSoundTimer = 10;
        SoundStyle soundStyle = SoundID.Item22;
        ((SoundStyle) ref soundStyle).Pitch = 0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[this.Projectile.owner]).Center), (SoundUpdateCallback) null);
      }
      for (int index1 = 0; index1 < 12; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 157, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Main.dust[index2].velocity = Vector2.op_Addition(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) target).Center), 9f), Utils.NextVector2Circular(Main.rand, 12f, 12f));
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
    }

    public virtual void AI()
    {
      this.CastLights();
      this.Projectile.timeLeft = 2;
      if (((Entity) Main.player[this.Projectile.owner]).active)
      {
        Player player = Main.player[this.Projectile.owner];
        if (!player.dead && player.HeldItem.type == ModContent.ItemType<NanoCore>())
        {
          if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 0 && player.channel)
          {
            this.Projectile.damage = (int) ((double) this.Projectile.ai[1] * (double) player.GetWeaponDamage(player.HeldItem, false));
            this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
            this.Projectile.ai[0] = (float) (((double) this.Projectile.ai[0] + 1.0) % 30.0);
            if ((double) this.Projectile.ai[0] == 15.0)
              SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
            ((Entity) this.Projectile).Center = ((Entity) player).Center;
            this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center));
            this.Projectile.localAI[0] += 0.5f;
            if ((double) this.Projectile.localAI[0] > 3.0)
              this.Projectile.localAI[0] = 3f;
          }
          else if (this.Projectile.owner == Main.myPlayer)
            this.Projectile.Kill();
        }
        else if (this.Projectile.owner == Main.myPlayer)
          this.Projectile.Kill();
      }
      else if (this.Projectile.owner == Main.myPlayer)
        this.Projectile.Kill();
      this.Projectile.alpha = 0;
      if (this.chainsawSoundTimer <= 0)
        return;
      --this.chainsawSoundTimer;
    }

    public virtual void CutTiles()
    {
      DelegateMethods.tilecut_0 = (TileCuttingContext) 2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 296f), this.Projectile.scale)), (float) (((Entity) this.Projectile).width + 16) * this.Projectile.scale, NanoBlade.\u003C\u003EO.\u003C0\u003E__CutTiles ?? (NanoBlade.\u003C\u003EO.\u003C0\u003E__CutTiles = new Utils.TileActionAttempt((object) null, __methodptr(CutTiles))));
    }

    private void CastLights()
    {
      DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 0.6f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 296f), this.Projectile.scale)), (float) ((Entity) this.Projectile).width, NanoBlade.\u003C\u003EO.\u003C1\u003E__CastLight ?? (NanoBlade.\u003C\u003EO.\u003C1\u003E__CastLight = new Utils.TileActionAttempt((object) null, __methodptr(CastLight))));
    }
  }
}
