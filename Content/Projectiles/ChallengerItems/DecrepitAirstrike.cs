// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.DecrepitAirstrike
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class DecrepitAirstrike : TrojanAcorn
  {
    private const int maxTime = 180;
    private SoundStyle Beep = new SoundStyle("FargowiltasSouls/Assets/Sounds/NukeBeep", (SoundType) 0);

    public override string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/TargetingReticle";
    }

    public override void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 82;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 180;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public ref float State => ref this.Projectile.ai[0];

    public ref float Timer => ref this.Projectile.ai[1];

    public ref float slotsConsumed => ref this.Projectile.ai[2];

    public override void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if ((double) this.State != 0.0)
        return;
      this.Projectile.netUpdate = true;
      this.Projectile.alpha -= 4;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      int num1 = Math.Min(60, 180 - this.Projectile.timeLeft);
      this.Projectile.scale = (float) (4.0 - 0.05000000074505806 * (double) num1);
      this.Projectile.rotation = 0.209439516f * (float) num1;
      if (this.Projectile.timeLeft % 60 == 0 && this.Projectile.timeLeft > 30)
      {
        SoundEngine.PlaySound(ref this.Beep, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        CombatText.NewText(((Entity) this.Projectile).Hitbox, Color.Red, this.Projectile.timeLeft / 60, true, false);
      }
      if (this.Projectile.timeLeft != 30)
        return;
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitY, -700f), Vector2.op_Multiply(Vector2.UnitX, Utils.NextFloat(Main.rand, -300f, 300f)));
      int num2 = this.Projectile.damage + this.Projectile.damage / 2 * ((int) this.slotsConsumed - 1);
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Vector2.Zero, ModContent.ProjectileType<DecrepitAirstrikeNuke>(), num2, 2f, this.Projectile.owner, ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, (float) this.Projectile.timeLeft);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      if ((double) this.State == 0.0)
      {
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
        int num2 = num1 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
        Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
