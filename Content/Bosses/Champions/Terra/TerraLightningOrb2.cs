// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Terra.TerraLightningOrb2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Terra
{
  public class TerraLightningOrb2 : ModProjectile
  {
    private bool firsttick;

    public virtual string Texture => "Terraria/Images/Projectile_465";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 70;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 360;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 0.5f;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.Projectile.scale);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width / (double) this.Projectile.scale);
      ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height / (double) this.Projectile.scale);
      this.Projectile.scale = reader.ReadSingle();
      ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale);
      ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale);
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
    }

    public virtual void AI()
    {
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      if (!this.firsttick)
      {
        for (int index = 0; index < 8; ++index)
        {
          Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, 0.78539818525314331 * (double) index, new Vector2());
          Vector2 vector2_2 = Vector2.Normalize(vector2_1);
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<TerraLightningOrbDeathray>(), this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(vector2_1), (float) ((Entity) this.Projectile).whoAmI, 0.0f);
        }
        this.Projectile.rotation = this.Projectile.localAI[0];
        this.firsttick = true;
      }
      if ((double) this.Projectile.localAI[0] > 0.0)
        this.Projectile.rotation += (float) ((double) this.Projectile.localAI[1] * (6.0 - (double) this.Projectile.scale) * 0.012000000104308128);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TerraChampion>());
      if (npc != null)
      {
        this.Projectile.alpha -= 10;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(4f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[npc.target]).Center));
        if ((double) ++this.Projectile.ai[1] > 60.0)
        {
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
          ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
          ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width / (double) this.Projectile.scale);
          ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height / (double) this.Projectile.scale);
          ++this.Projectile.scale;
          ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale);
          ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale);
          ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
          this.MakeDust();
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
      }
      else
      {
        if (this.Projectile.timeLeft < 2)
          this.Projectile.timeLeft = 2;
        this.Projectile.alpha += 10;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
        }
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.85f, 0.9f);
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 3)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame > 3)
          this.Projectile.frame = 0;
      }
      if (!Utils.NextBool(Main.rand, 3))
        return;
      float num1 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
      if ((double) num1 < -0.5)
        num1 = -0.5f;
      if ((double) num1 > 0.5)
        num1 = 0.5f;
      Vector2 vector2_3 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.2f * this.Projectile.scale, 0.0f), (double) num1 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
      int index1 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
      Main.dust[index1].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_3, this.Projectile.scale));
      Main.dust[index1].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.dust[index1].position, ((Entity) this.Projectile).Center)), 2f);
      Main.dust[index1].noGravity = true;
      float num2 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
      if ((double) num2 < -0.5)
        num2 = -0.5f;
      if ((double) num2 > 0.5)
        num2 = 0.5f;
      Vector2 vector2_4 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.6f * this.Projectile.scale, 0.0f), (double) num2 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
      int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
      Main.dust[index2].velocity = Vector2.Zero;
      Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_4, this.Projectile.scale));
      Main.dust[index2].noGravity = true;
    }

    private void MakeDust()
    {
      for (int index1 = 0; index1 < 25; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f * this.Projectile.scale);
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4f * this.Projectile.scale);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].noLight = true;
      }
      for (int index4 = 0; index4 < 80; ++index4)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 10f), this.Projectile.scale), (double) (index4 - 39) * 6.2831854820251465 / 80.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index5 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 92, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index5].noGravity = true;
        Main.dust[index5].velocity = vector2_2;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.MakeDust();
      if (!Main.dedServ)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      if (this.Projectile.alpha != 0 || !FargoSoulsUtil.HostCheck)
        return;
      if (!Main.dedServ)
      {
        SoundStyle thunder = SoundID.Thunder;
        ((SoundStyle) ref thunder).Volume = 0.8f;
        ((SoundStyle) ref thunder).Pitch = 0.5f;
        SoundEngine.PlaySound(ref thunder, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      for (int index = 0; index < 8; ++index)
      {
        Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, 0.78539818525314331 * (double) index + (double) this.Projectile.rotation, new Vector2());
        float num = Utils.NextBool(Main.rand) ? 1f : -1f;
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), 54f);
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<HostileLightning>(), this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(vector2_1), num / 2f, 0.0f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
