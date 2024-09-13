// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosFireball2
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
  public class CosmosFireball2 : ModProjectile
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
      this.Projectile.timeLeft = 300;
      this.Projectile.aiStyle = -1;
      this.Projectile.extraUpdates = 1;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 2)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame > 3)
          this.Projectile.frame = 0;
      }
      if ((double) --this.Projectile.ai[0] == 0.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 0.1f);
        this.Projectile.netUpdate = true;
      }
      if ((double) --this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.Kill();
      }
      else
      {
        Lighting.AddLight(((Entity) this.Projectile).Center, 1.1f, 0.9f, 0.4f);
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] == 12.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          for (int index1 = 0; index1 < 12; ++index1)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) -((Entity) this.Projectile).width), 2f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 3.14159274101257 / 6.0, new Vector2())), new Vector2(8f, 16f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 6, 0.0f, 0.0f, 160, new Color(), 1f);
            Main.dust[index2].scale = 1.1f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
            Main.dust[index2].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f);
            Main.dust[index2].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), Main.dust[index2].position)), 1.25f);
          }
        }
        if (Utils.NextBool(Main.rand, 4))
        {
          for (int index3 = 0; index3 < 1; ++index3)
          {
            Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
            int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1f);
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
            Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
            Main.dust[index4].fadeIn = 0.9f;
          }
        }
        if (Utils.NextBool(Main.rand, 32))
        {
          for (int index5 = 0; index5 < 1; ++index5)
          {
            Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.392699092626572), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
            int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 155, new Color(), 0.8f);
            Dust dust = Main.dust[index6];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
            Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
            if (Utils.NextBool(Main.rand))
              Main.dust[index6].fadeIn = 1.4f;
          }
        }
        if (!Utils.NextBool(Main.rand))
          return;
        for (int index7 = 0; index7 < 2; ++index7)
        {
          Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.785398185253143), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1.2f);
          Dust dust = Main.dust[index8];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index8].noGravity = true;
          Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand))
            Main.dust[index8].fadeIn = 1.4f;
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      if (!FargoSoulsUtil.HostCheck)
        return;
      int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      if (closest == -1)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(12f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center)), 467, this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
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
