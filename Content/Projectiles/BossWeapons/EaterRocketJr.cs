// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.EaterRocketJr
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  [LegacyName(new string[] {"EaterStaff"})]
  public class EaterRocketJr : ModProjectile
  {
    private bool sweetspot;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 19;
      ((Entity) this.Projectile).height = 19;
      this.Projectile.scale = 1f;
      this.Projectile.aiStyle = -1;
      this.AIType = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 1200;
    }

    public virtual void AI()
    {
      int index = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 24, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
      Main.dust[index].noGravity = true;
      ((Entity) this.Projectile).velocity.Y += 0.11f;
      this.Projectile.rotation += (float) (0.78539818525314331 * ((double) ((Entity) this.Projectile).velocity.X / 40.0));
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      Player player = Main.player[this.Projectile.owner];
      int num1 = 300;
      int rockeaterDistance = player.FargoSouls().RockeaterDistance;
      int num2 = (num1 + rockeaterDistance) / 2;
      Vector2 desiredLocation = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) target).Center), (float) num2));
      float num3 = Vector2.Distance(FargoSoulsUtil.ClosestPointInHitbox(((Entity) target).Hitbox, desiredLocation), ((Entity) player).Center);
      if ((double) num3 <= (double) num1 || (double) num3 >= (double) rockeaterDistance)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 1.5f);
      this.sweetspot = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 24, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
      if (this.sweetspot)
      {
        for (int index3 = 0; index3 < 40; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 24, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].noLight = true;
          Main.dust[index4].velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 9f), Utils.NextVector2Circular(Main.rand, 5f, 5f));
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.25f);
          Main.dust[index4].scale *= Utils.NextFloat(Main.rand, 1.5f, 1.25f);
        }
      }
      SoundEngine.PlaySound(ref SoundID.NPCDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index5 = 0; index5 < 5; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 24, 0.0f, 0.0f, 100, new Color(), 1f);
        Main.dust[index6].noGravity = true;
        Dust dust1 = Main.dust[index6];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 4f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 24, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index7];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = 1;
      for (int index8 = 0; index8 < num; ++index8)
      {
        int index9 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2((float) Main.rand.Next(-10, 10), (float) Main.rand.Next(-10, 10)), 307, this.Projectile.damage / 2, this.Projectile.knockBack / 6f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        if (index9 != Main.maxProjectiles)
          Main.projectile[index9].DamageType = DamageClass.Ranged;
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
