// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.ChibiDevi
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class ChibiDevi : ModProjectile
  {
    private Vector2 oldMouse;
    private static bool haveDoneInitScramble;
    private static bool usePlayerDiedSpawnText;
    private Vector2 target;
    private Vector2 targetSpeed;
    private int syncTimer;
    public static int[] TalkCounters = new int[10];
    public static int[] TalkCDs = new int[10];
    private int universalTalkCD = 30;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.LightPet[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 44;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt((int) this.target.X);
      writer.Write7BitEncodedInt((int) this.target.Y);
      Utils.WritePackedVector2(writer, this.targetSpeed);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.target.X = (float) reader.Read7BitEncodedInt();
      this.target.Y = (float) reader.Read7BitEncodedInt();
      this.targetSpeed = Utils.ReadPackedVector2(reader);
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!ChibiDevi.haveDoneInitScramble)
      {
        ChibiDevi.haveDoneInitScramble = true;
        for (int index = 0; index < ChibiDevi.TalkCounters.Length; ++index)
          ChibiDevi.TalkCounters[index] = Main.rand.Next(ChibiDevi.MaxThingsToSay[index]);
        ChibiDevi.TalkCDs[2] = ChibiDevi.MediumCD;
      }
      this.TryTalkWithCD(ChibiDevi.usePlayerDiedSpawnText ? ChibiDevi.TalkType.Respawn : ChibiDevi.TalkType.Spawn, ChibiDevi.ShortCD);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      ChibiDevi.usePlayerDiedSpawnText = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead || player.ghost)
        fargoSoulsPlayer.ChibiDevi = false;
      if (fargoSoulsPlayer.ChibiDevi)
        this.Projectile.timeLeft = 2;
      DelegateMethods.v3_1 = new Vector3(1f, 0.5f, 0.9f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 6f)), 20f, ChibiDevi.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (ChibiDevi.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Left, ((Entity) this.Projectile).Right, 20f, ChibiDevi.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (ChibiDevi.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        Vector2 target = this.target;
        this.target = Main.MouseWorld;
        if (Vector2.op_Inequality(target, Vector2.Zero))
          this.targetSpeed = Vector2.op_Subtraction(this.target, target);
        if (++this.syncTimer > 20)
        {
          this.syncTimer = 0;
          this.Projectile.netUpdate = true;
        }
      }
      Vector2 targetPos = Vector2.op_Addition(this.target, this.targetSpeed);
      bool flag1 = (double) this.Projectile.ai[0] == 1.0;
      if (flag1)
      {
        this.Projectile.tileCollide = true;
        this.Projectile.ignoreWater = false;
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = (double) ((Entity) this.Projectile).velocity.Y == 0.0 ? 5 : 4;
        ((Entity) this.Projectile).velocity.X *= 0.95f;
        ((Entity) this.Projectile).velocity.Y += 0.3f;
        if (this.Projectile.owner == Main.myPlayer && (double) ((Entity) this.Projectile).Distance(targetPos) > 180.0)
        {
          this.Projectile.ai[0] = 0.0f;
          this.Projectile.netUpdate = true;
          this.TryTalkWithCD(ChibiDevi.TalkType.Wake, ChibiDevi.ShortCD);
        }
      }
      else
      {
        this.Projectile.tileCollide = false;
        this.Projectile.ignoreWater = true;
        ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).Center.X < (double) targetPos.X ? 1 : -1;
        float num1 = 2500f;
        float num2 = (float) ((double) ((Entity) Main.player[this.Projectile.owner]).Distance(targetPos) / 2.0 + 100.0);
        if ((double) num1 < (double) num2)
          num1 = num2;
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > (double) num1 && (double) ((Entity) this.Projectile).Distance(this.target) > (double) num1)
        {
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
        }
        if ((double) ((Entity) this.Projectile).Distance(targetPos) > 30.0)
        {
          float speedModifier = MathHelper.Lerp(0.1f, 0.8f, Math.Min(((Entity) this.Projectile).Distance(targetPos) / 1200f, 1f));
          this.Movement(targetPos, speedModifier, (float) (16.0 + (double) ((Vector2) ref ((Entity) Main.player[this.Projectile.owner]).velocity).Length() / 2.0));
        }
        if (Vector2.op_Equality(this.oldMouse, targetPos))
        {
          ++this.Projectile.ai[1];
          if ((double) this.Projectile.ai[1] > 600.0)
          {
            bool flag2 = !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
            if (flag2)
            {
              flag2 = false;
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height);
              for (int index = 0; index < 10; ++index)
              {
                vector2.Y += 16f;
                Tile tileSafely = Framing.GetTileSafely(vector2);
                if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType])
                {
                  flag2 = true;
                  break;
                }
              }
            }
            if (flag2)
            {
              this.Projectile.ai[0] = 1f;
              this.Projectile.ai[1] = 0.0f;
              this.TryTalkWithCD(ChibiDevi.TalkType.Sleep, ChibiDevi.ShortCD);
              this.Projectile.netUpdate = true;
            }
            else
              this.Projectile.ai[1] = 540f;
          }
        }
        else
        {
          this.Projectile.ai[1] = 0.0f;
          this.oldMouse = this.target;
        }
        if (++this.Projectile.frameCounter > 6)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= 4)
            this.Projectile.frame = 0;
        }
      }
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      bool flag3 = Luminance.Common.Utilities.Utilities.AnyBosses();
      if (ChibiDevi.TalkCDs[2] < 60 && (flag3 | flag1 || player.statLife < player.statLifeMax2 / 2))
        ChibiDevi.TalkCDs[2] = Math.Max(ChibiDevi.TalkCDs[2], Luminance.Common.Utilities.Utilities.SecondsToFrames(12f));
      if (flag3)
      {
        this.TryTalkWithCD(ChibiDevi.TalkType.BossSpawn, ChibiDevi.ShortCD);
        if (Main.npc[FargoSoulsGlobalNPC.boss].life < Main.npc[FargoSoulsGlobalNPC.boss].lifeMax / 4)
          this.TryTalkWithCD(ChibiDevi.TalkType.BossAlmostDead, ChibiDevi.MediumCD);
      }
      else
      {
        this.TryTalkWithCD(ChibiDevi.TalkType.Idle, ChibiDevi.LongCD);
        ChibiDevi.TalkCDs[7] = Math.Max(ChibiDevi.TalkCDs[7], 1800);
        ChibiDevi.TalkCDs[8] = Math.Max(ChibiDevi.TalkCDs[8], 1800);
      }
      if (this.universalTalkCD > 0)
        --this.universalTalkCD;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      for (int index = 0; index < ChibiDevi.TalkCDs.Length; ++index)
      {
        if ((!flag3 || index != 2 && index != 9) && ChibiDevi.TalkCDs[index] > 0)
          --ChibiDevi.TalkCDs[index];
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || player.ghost)
      {
        this.TryTalkWithCD(ChibiDevi.TalkType.PlayerDeath, ChibiDevi.MediumCD);
        if (this.Projectile.owner != Main.myPlayer)
          return;
        ChibiDevi.usePlayerDiedSpawnText = true;
      }
      else
        this.TryTalkWithCD(ChibiDevi.TalkType.ProjDeath, ChibiDevi.ShortCD);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if ((double) ((Entity) this.Projectile).Center.X < (double) targetPos.X)
      {
        ((Entity) this.Projectile).velocity.X += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
          ((Entity) this.Projectile).velocity.X *= 0.95f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.X -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
          ((Entity) this.Projectile).velocity.X *= 0.95f;
      }
      if ((double) ((Entity) this.Projectile).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.Projectile).velocity.Y += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y *= 0.95f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y *= 0.95f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > (double) cap)
        ((Entity) this.Projectile).velocity.X = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.Projectile).velocity.Y = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.Y);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Color color1;
      // ISSUE: explicit constructor call
      ((Color) ref color1).\u002Ector((int) byte.MaxValue, 51, 153, 50);
      float num3 = Math.Min((float) ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 16.0 / 2.0), 1f);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.2f)
      {
        Color color2 = Color.op_Multiply(Color.op_Multiply(color1, 0.4f), num3);
        float num4 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color3 = Color.op_Multiply(color2, num4 * num4);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num5 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num5, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Color color4 = Color.op_Multiply(color1, (float) Math.Sqrt((double) num3));
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color4, this.Projectile.rotation, vector2_1, this.Projectile.scale * 1.25f, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual void Unload()
    {
      ChibiDevi.TalkCounters = (int[]) null;
      ChibiDevi.TalkCDs = (int[]) null;
    }

    private static int[] MaxThingsToSay
    {
      get => new int[11]{ 7, 7, 12, 5, 5, 4, 7, 6, 7, 8, 1 };
    }

    public static int ShortCD => 600;

    public static int MediumCD => Main.rand.Next(3600, 7200);

    public static int LongCD => ChibiDevi.MediumCD * 2;

    public void TryTalkWithCD(ChibiDevi.TalkType talkType, int CD)
    {
      int index = (int) talkType;
      if (ChibiDevi.TalkCDs[index] > 0 || this.universalTalkCD > 0 || (double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > 352.0)
        return;
      ChibiDevi.TalkCounters[index] = (ChibiDevi.TalkCounters[index] + 1) % ChibiDevi.MaxThingsToSay[index];
      ChibiDevi.TalkCDs[index] = CD;
      this.universalTalkCD = 0;
      if (this.Projectile.owner != Main.myPlayer || !ModContent.GetInstance<SoulConfig>().DeviChatter)
        return;
      if (!Main.player[this.Projectile.owner].dead && !Main.player[this.Projectile.owner].ghost)
        EmoteBubble.MakeLocalPlayerEmote(0);
      SoundEngine.PlaySound(ref SoundID.LucyTheAxeTalk, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      string name = Enum.GetName<ChibiDevi.TalkType>(talkType);
      int num = ChibiDevi.TalkCounters[index] + 1;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
      interpolatedStringHandler.AppendLiteral("Mods.FargowiltasSouls.Items.ChibiHat.DeviChatter.");
      interpolatedStringHandler.AppendFormatted(name);
      interpolatedStringHandler.AppendFormatted<int>(num);
      string textValue = Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
      PopupText.NewText(new AdvancedPopupRequest()
      {
        Text = textValue,
        DurationInFrames = 420,
        Velocity = Vector2.op_Multiply(7f, Vector2.op_UnaryNegation(Vector2.UnitY)),
        Color = Color.op_Multiply(Color.HotPink, 1.15f)
      }, Vector2.Lerp(Vector2.Lerp(this.target, ((Entity) this.Projectile).Center, 0.5f), ((Entity) Main.player[this.Projectile.owner]).Center, 0.5f));
    }

    public enum TalkType
    {
      Spawn,
      Respawn,
      Idle,
      Sleep,
      Wake,
      ProjDeath,
      PlayerDeath,
      BossAlmostDead,
      KillBoss,
      BossSpawn,
      Count,
    }
  }
}
