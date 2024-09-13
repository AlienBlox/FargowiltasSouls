// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosFireball
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosFireball : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_467";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 325;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        float num = 0.5f;
        for (int index6 = 0; index6 < 4; ++index6)
        {
          int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore = Main.gore[index7];
          gore.velocity = Vector2.op_Multiply(gore.velocity, num);
          ++Main.gore[index7].velocity.X;
          ++Main.gore[index7].velocity.Y;
        }
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 2)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame > 3)
          this.Projectile.frame = 0;
      }
      int index8 = (int) this.Projectile.ai[0];
      Vector2 vector2_1 = Utils.RotatedBy(new Vector2(120f, 0.0f), (double) this.Projectile.ai[1], new Vector2());
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.npc[index8]).Center, vector2_1);
      this.Projectile.ai[1] -= 0.17f;
      this.Projectile.rotation = this.Projectile.ai[1];
      ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation - 1.57079637f);
      Lighting.AddLight(((Entity) this.Projectile).Center, 1.1f, 0.9f, 0.4f);
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] == 12.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        for (int index9 = 0; index9 < 12; ++index9)
        {
          Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) -((Entity) this.Projectile).width), 2f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index9 * 3.14159274101257 / 6.0, new Vector2())), new Vector2(8f, 16f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
          int index10 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 6, 0.0f, 0.0f, 160, new Color(), 1f);
          Main.dust[index10].scale = 1.1f;
          Main.dust[index10].noGravity = true;
          Main.dust[index10].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2);
          Main.dust[index10].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f);
          Main.dust[index10].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), Main.dust[index10].position)), 1.25f);
        }
      }
      if (Utils.NextBool(Main.rand, 4))
      {
        for (int index11 = 0; index11 < 1; ++index11)
        {
          Vector2 vector2_3 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index12 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust = Main.dust[index12];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index12].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_3, (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index12].fadeIn = 0.9f;
        }
      }
      if (Utils.NextBool(Main.rand, 32))
      {
        for (int index13 = 0; index13 < 1; ++index13)
        {
          Vector2 vector2_4 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.392699092626572), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index14 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 155, new Color(), 0.8f);
          Dust dust = Main.dust[index14];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index14].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_4, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand))
            Main.dust[index14].fadeIn = 1.4f;
        }
      }
      if (!Utils.NextBool(Main.rand))
        return;
      for (int index15 = 0; index15 < 2; ++index15)
      {
        Vector2 vector2_5 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.785398185253143), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
        int index16 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1.2f);
        Dust dust = Main.dust[index16];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
        Main.dust[index16].noGravity = true;
        Main.dust[index16].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_5, (float) ((Entity) this.Projectile).width), 2f));
        if (Utils.NextBool(Main.rand))
          Main.dust[index16].fadeIn = 1.4f;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      int index = (int) this.Projectile.ai[0];
      if (index <= -1 || index >= Main.maxNPCs || !((Entity) Main.npc[index]).active || Main.npc[index].type != ModContent.NPCType<CosmosChampion>() || WorldSavingSystem.EternityMode && (double) Main.npc[index].localAI[2] != 0.0 || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(12f, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2())), 467, this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (!NPC.AnyNPCs(ModContent.NPCType<CosmosChampion>()))
        return;
      ref AddableFloat local = ref modifiers.ScalingArmorPenetration;
      local = AddableFloat.op_Addition(local, 0.25f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(67, 120, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
