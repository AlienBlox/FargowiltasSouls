// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantRitual : BaseArena
  {
    private const float realRotation = 0.0224399474f;
    private bool MutantDead;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_454" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphere_April";
      }
    }

    public MutantRitual()
      : base((float) Math.PI / 140f, 1200f, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>(), visualCount: 48)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
    }

    public override bool? CanDamage() => this.MutantDead ? new bool?(false) : base.CanDamage();

    protected override void Movement(NPC npc)
    {
      float num;
      if ((double) npc.ai[0] == 19.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        num = -1f * (float) Math.PI / 280f;
      }
      else if ((double) npc.ai[0] == 49.0)
      {
        if (npc.HasValidTarget && (double) npc.ai[1] < 30.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) this.Projectile).Center), 10f);
          num = (float) Math.PI / 140f;
        }
        else
        {
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          num = -1f * (float) Math.PI / 280f;
        }
      }
      else
      {
        ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
        if ((double) npc.ai[0] == 36.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 20f);
        }
        else if ((double) npc.ai[0] == 22.0 || (double) npc.ai[0] == 23.0 || (double) npc.ai[0] == 25.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 40f);
        }
        else
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 60f);
        }
        num = (float) Math.PI / 140f;
      }
      if ((double) this.rotationPerTick < (double) num)
      {
        this.rotationPerTick += 0.0005609987f;
        if ((double) this.rotationPerTick > (double) num)
          this.rotationPerTick = num;
      }
      else if ((double) this.rotationPerTick > (double) num)
      {
        this.rotationPerTick -= 0.0005609987f;
        if ((double) this.rotationPerTick < (double) num)
          this.rotationPerTick = num;
      }
      this.MutantDead = (double) npc.ai[0] <= -6.0;
    }

    public override void AI()
    {
      base.AI();
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
        if (WorldSavingSystem.MasochistModeReal && (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == -5.0)
        {
          if (!target.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
          {
            SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudo", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
          }
          target.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 300, true, false);
        }
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), (double) this.targetPlayer != (double) Main.myPlayer || this.MutantDead ? 0.15f : 1f));
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D2.Height;
      int num3 = 0;
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, num3, texture2D2.Width, height);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
      Color color1 = Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      for (int index1 = 0; index1 < 32; ++index1)
      {
        Vector2 vector2_3 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) ((double) this.threshold * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2()), 0.19634954631328583 * (double) index1, new Vector2());
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Color color2 = Color.op_Multiply(alpha, (float) (4 - index2) / 4f);
          Vector2 vector2_4 = Vector2.op_Addition(Vector2.op_Addition(this.Projectile.oldPos[index2], Vector2.op_Division(Utils.Size(((Entity) this.Projectile).Hitbox), 2f)), Utils.RotatedBy(vector2_3, (double) this.rotationPerTick * (double) -index2, new Vector2()));
          float rotation = this.Projectile.rotation;
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color2, rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), Color.op_Multiply(color1, (float) (4 - index2) / 4f), this.Projectile.rotation, vector2_2, this.Projectile.scale * 1.4f, (SpriteEffects) 0, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), color1, this.Projectile.rotation, vector2_2, this.Projectile.scale * 1.3f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
