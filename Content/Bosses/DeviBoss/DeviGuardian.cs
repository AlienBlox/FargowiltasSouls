// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviGuardian
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviGuardian : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_197";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.timeLeft = 600;
      this.Projectile.hide = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 0.0f, 6.28318548f);
        this.Projectile.hide = false;
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 50; ++index1)
        {
          int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-20, 20), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-20, 20)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
        }
      }
      ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.3f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-20, 20), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-20, 20)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.guardBoss, 68))
        return;
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      NPC sourceNpc = this.Projectile.GetSourceNPC();
      Texture2D texture2D = (sourceNpc == null || sourceNpc.type != 35 && sourceNpc.type != 36 && sourceNpc.type != 68 || !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/DeviBoss/DeviGuardian_Recolor", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
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
