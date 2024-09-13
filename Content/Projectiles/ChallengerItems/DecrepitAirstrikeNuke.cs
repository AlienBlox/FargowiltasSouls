// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.DecrepitAirstrikeNuke
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using FargowiltasSouls.Content.Projectiles.Minions;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class DecrepitAirstrikeNuke : TrojanAcorn
  {
    public static readonly int ExplosionDiameter = 450;
    private SoundStyle Beep = new SoundStyle("FargowiltasSouls/Assets/Sounds/NukeBeep", (SoundType) 0);
    private Vector2 origPos = Vector2.Zero;
    private bool firstTick = true;
    private int HitCount;

    public override string Texture => "FargowiltasSouls/Content/Bosses/BanishedBaron/BaronNuke";

    public override void SetStaticDefaults() => Main.projFrames[this.Type] = 4;

    public override void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.sentry = false;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 3600;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 60;
    }

    public ref float TargetX => ref this.Projectile.ai[0];

    public ref float TargetY => ref this.Projectile.ai[1];

    public ref float TimeLeft => ref this.Projectile.ai[2];

    public virtual bool? CanHitNPC(NPC target) => new bool?(this.HitCount <= 4);

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => ++this.HitCount;

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public override void AI()
    {
      if (this.firstTick)
      {
        if ((double) this.Projectile.timeLeft > (double) this.TimeLeft)
          this.Projectile.timeLeft = (int) this.TimeLeft + 3;
        this.origPos = ((Entity) this.Projectile).Center;
        this.firstTick = false;
        this.Projectile.netUpdate = true;
      }
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(this.TargetX, Vector2.UnitX), Vector2.op_Multiply(this.TargetY, Vector2.UnitY));
      this.Projectile.rotation = Utils.ToRotation(Vector2.op_UnaryNegation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2)));
      if (this.Projectile.timeLeft <= 3)
      {
        ((Entity) this.Projectile).width = DecrepitAirstrikeNuke.ExplosionDiameter;
        ((Entity) this.Projectile).height = DecrepitAirstrikeNuke.ExplosionDiameter;
        ((Entity) this.Projectile).Center = vector2;
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      }
      else
        ((Entity) this.Projectile).Center = Vector2.Lerp(this.origPos, vector2, (this.TimeLeft - (float) (this.Projectile.timeLeft - 3)) / this.TimeLeft);
      if (this.Projectile.timeLeft % 5 != 0)
        return;
      SoundEngine.PlaySound(ref this.Beep, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public override void OnKill(int timeLeft)
    {
      ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      int maxMinions = Main.player[this.Projectile.owner].maxMinions;
      foreach (Projectile p in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.type == ModContent.ProjectileType<KamikazeSquirrel>() && ((Entity) p).active && !p.hostile && p.owner == Main.myPlayer && p.minion && FargoSoulsUtil.IsSummonDamage(p, false, false) && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) p).Hitbox))))
        EchsplodeMinion(p, ref maxMinions);
      foreach (Projectile p in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.type != ModContent.ProjectileType<KamikazeSquirrel>() && ((Entity) p).active && !p.hostile && p.owner == Main.myPlayer && p.minion && FargoSoulsUtil.IsSummonDamage(p, false, false) && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) p).Hitbox))))
        EchsplodeMinion(p, ref maxMinions);
      for (int index1 = 0; index1 < 100; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(new Vector2(0.0f, Utils.NextFloat(Main.rand, (float) DecrepitAirstrikeNuke.ExplosionDiameter * 0.8f)), (double) Utils.NextFloat(Main.rand, 6.28318548f), new Vector2())), 0, 0, 219, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
      float num = 2f;
      for (int index = 0; index < 20; ++index)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.UnitX, 5f), 6.2831854820251465), Main.rand.Next(61, 64), num);
      SoundStyle soundStyle = SoundID.Item62;
      ((SoundStyle) ref soundStyle).Pitch = -0.2f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);

      void EchsplodeMinion(Projectile p, ref int hitsLeft)
      {
        if (hitsLeft <= 0)
          return;
        --hitsLeft;
        for (int index = 0; index < 5; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), 15f);
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) p).Center, Vector2.op_Addition(((Entity) p).velocity, vector2), ModContent.ProjectileType<DecrepitAirstrikeNukeSplinter>(), this.Projectile.damage / 6, this.Projectile.knockBack / 10f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        }
        p.Kill();
        SoundEngine.PlaySound(ref SoundID.Item67, new Vector2?(((Entity) p).Center), (SoundUpdateCallback) null);
      }
    }

    public override bool PreDraw(ref Color lightColor)
    {
      if (this.Projectile.timeLeft <= 2)
        return false;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
