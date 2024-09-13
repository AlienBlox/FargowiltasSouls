// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronArenaWhirlpool
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronArenaWhirlpool : ModProjectile
  {
    private static int BaseMaxDistance = 1000;
    private int WaterwallDistance;

    public virtual string Texture => "FargowiltasSouls/Content/Bosses/BanishedBaron/BaronWhirlpool";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 16;
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 99999999;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 186;
      ((Entity) this.Projectile).height = 48;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = false;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.timeLeft = 216000;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      ref float local1 = ref this.Projectile.ai[1];
      ref float local2 = ref this.Projectile.ai[2];
      int firstNpc = NPC.FindFirstNPC(ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
      if (!firstNpc.IsWithinBounds(Main.maxNPCs))
      {
        this.Projectile.Kill();
      }
      else
      {
        NPC npc = Main.npc[firstNpc];
        if (npc == null || !((Entity) npc).active || npc.type != ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>())
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.timeLeft = 216000;
          float num1 = (float) (npc.lifeMax / 2);
          float num2 = 400f * (float) (1.0 - (double) npc.life / (double) num1);
          float num3 = (float) BaronArenaWhirlpool.BaseMaxDistance - num2;
          if (WorldSavingSystem.masochistModeReal)
            num3 -= 300f;
          Player player = npc.target.IsWithinBounds((int) byte.MaxValue) ? Main.player[npc.target] : (Player) null;
          if (player != null && ((Entity) player).active && !player.dead && !player.ghost)
          {
            if ((double) local1 == 0.0)
            {
              int num4 = Math.Sign((float) (Main.maxTilesX * 8) - ((Entity) this.Projectile).Center.X);
              ((Entity) this.Projectile).Center = Vector2.op_Addition(Main.screenPosition, new Vector2((float) (200 * num4 + Main.screenWidth / 2), (float) (Main.screenHeight / 2)));
            }
            ((Entity) this.Projectile).Center = Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).Center.X, Vector2.UnitX), Vector2.op_Multiply(((Entity) player).Center.Y, Vector2.UnitY));
            int num5 = WorldSavingSystem.MasochistModeReal ? 55 : (WorldSavingSystem.EternityMode ? 70 : 80);
            if ((double) local2 == 1.0 && (double) local1 % (double) num5 == 0.0 && FargoSoulsUtil.HostCheck)
            {
              int side = Luminance.Common.Utilities.Utilities.NonZeroSign(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X);
              if (WorldSavingSystem.MasochistModeReal)
                side = (double) local1 % (double) (num5 * 2) == 0.0 ? 1 : -1;
              this.FireBolts(player, side);
            }
          }
          if (this.Projectile.alpha > 0)
            this.Projectile.alpha -= 3;
          else
            this.Projectile.alpha = 0;
          if (++this.Projectile.frameCounter > 2)
          {
            if (++this.Projectile.frame >= Main.projFrames[this.Type])
              this.Projectile.frame = 0;
            this.Projectile.frameCounter = 0;
          }
          if ((double) this.WaterwallDistance < (double) num3)
            this.WaterwallDistance += 10;
          if ((double) this.WaterwallDistance > (double) num3 + 5.0)
            this.WaterwallDistance -= 5;
          this.WaterWalls(((Entity) this.Projectile).Center, this.WaterwallDistance);
          ++local1;
        }
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, 0, texture2D.Width, num1);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = -1; index1 < 2; index1 += 2)
      {
        for (int index2 = -50; index2 <= 20; ++index2)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).Center.X, Vector2.UnitX), Vector2.op_Multiply(((Entity) Main.LocalPlayer).Center.Y, Vector2.UnitY)), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index1), (float) (this.WaterwallDistance + rectangle1.Width / 2))), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) index2), (float) ((Entity) this.Projectile).height));
          vector2_1.Y = (float) Math.Floor((double) vector2_1.Y / (double) rectangle1.Height) * (float) rectangle1.Height;
          int num2 = (this.Projectile.frame + (int) ((double) vector2_1.Y / (double) rectangle1.Height)) % Main.projFrames[this.Projectile.type];
          int num3 = num1 * num2;
          Rectangle rectangle2;
          // ISSUE: explicit constructor call
          ((Rectangle) ref rectangle2).\u002Ector(0, num3, texture2D.Width, num1);
          Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
          if (!Collision.SolidCollision(Vector2.op_Subtraction(vector2_1, Vector2.op_Division(Utils.Size(rectangle2), 2f)), rectangle2.Width, rectangle2.Height))
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_1, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), alpha, this.Projectile.rotation, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      return false;
    }

    private void WaterWalls(Vector2 location, int threshold)
    {
      Player localPlayer = Main.LocalPlayer;
      bool flag1 = (double) Math.Abs(((Entity) localPlayer).Center.X - location.X) >= (double) threshold;
      bool flag2 = (double) Math.Abs(((Entity) localPlayer).Center.X - location.X) >= (double) (threshold * 2);
      bool flag3 = (double) Math.Abs(((Entity) localPlayer).Center.X - location.X) >= (double) (threshold * 4);
      if (!((Entity) localPlayer).active || localPlayer.dead || localPlayer.ghost || !flag1 || flag3)
        return;
      if (flag2)
      {
        localPlayer.controlLeft = false;
        localPlayer.controlRight = false;
        localPlayer.controlUp = false;
        localPlayer.controlDown = false;
        localPlayer.controlUseItem = false;
        localPlayer.controlUseTile = false;
        localPlayer.controlJump = false;
        localPlayer.controlHook = false;
        if (localPlayer.grapCount > 0)
          localPlayer.RemoveAllGrapplingHooks();
        if (localPlayer.mount.Active)
          localPlayer.mount.Dismount(localPlayer);
        ((Entity) localPlayer).velocity.X = 0.0f;
        localPlayer.FargoSouls().NoUsingItems = 2;
      }
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(location.X - ((Entity) localPlayer).Center.X, 0.0f);
      float num1 = ((Vector2) ref vector2).Length() - (float) threshold;
      ((Vector2) ref vector2).Normalize();
      vector2 = Vector2.op_Multiply(vector2, (double) num1 < 17.0 ? num1 : 17f);
      Player player = localPlayer;
      ((Entity) player).position = Vector2.op_Addition(((Entity) player).position, vector2);
      localPlayer.AddBuff(30, 120, true, false);
      int num2 = Math.Sign(((Entity) localPlayer).Center.X - location.X);
      if (Math.Sign(((Entity) localPlayer).velocity.X) != 0 && Math.Sign(((Entity) localPlayer).velocity.X) != num2)
        ((Entity) localPlayer).velocity.X = 0.0f;
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int num3 = 29;
        int index2 = Dust.NewDust(((Entity) localPlayer).position, ((Entity) localPlayer).width, ((Entity) localPlayer).height, num3, 0.0f, 0.0f, 0, new Color(), 1.25f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      }
    }

    private void FireBolts(Player player, int side)
    {
      for (int index = -10; index <= 10; ++index)
      {
        double num1 = (double) ((Entity) this.Projectile).Center.X + (double) side * ((double) ((Entity) this.Projectile).width * 0.800000011920929 + (double) this.WaterwallDistance);
        float num2 = ((Entity) player).Center.Y + (float) (index * ((Entity) this.Projectile).height) * 3.2f + Utils.NextFloat(Main.rand, -9f, 9f);
        Vector2 unitX = Vector2.UnitX;
        Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Multiply((float) num1, unitX), Vector2.op_Multiply(num2, Vector2.UnitY));
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) side), (double) Utils.NextFloat(Main.rand, -1f * (float) Math.PI / 28f, (float) Math.PI / 28f), new Vector2()), 7f);
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), vector2_1, vector2_2, ModContent.ProjectileType<BaronWhirlpoolBolt>(), this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 2f, (float) -side, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) side), (float) ((Entity) this.Projectile).width * 0.8f + (float) this.WaterwallDistance))), (SoundUpdateCallback) null);
    }
  }
}
