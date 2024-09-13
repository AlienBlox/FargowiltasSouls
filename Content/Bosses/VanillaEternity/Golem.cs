// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Golem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Golem : GolemPart
  {
    public int StompAttackCounter;
    public int SpikyBallTimer;
    public bool DoStompBehaviour;
    public bool HaveBoostedJumpHeight;
    public bool IsInTemple;
    public bool DroppedSummon;

    public Golem()
      : base(180)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(245);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.StompAttackCounter);
      binaryWriter.Write7BitEncodedInt(this.SpikyBallTimer);
      bitWriter.WriteBit(this.DoStompBehaviour);
      bitWriter.WriteBit(this.HaveBoostedJumpHeight);
      bitWriter.WriteBit(this.IsInTemple);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.StompAttackCounter = binaryReader.Read7BitEncodedInt();
      this.SpikyBallTimer = binaryReader.Read7BitEncodedInt();
      this.DoStompBehaviour = bitReader.ReadBit();
      this.HaveBoostedJumpHeight = bitReader.ReadBit();
      this.IsInTemple = bitReader.ReadBit();
    }

    public override void SetDefaults(NPC npc)
    {
      base.SetDefaults(npc);
      npc.lifeMax *= 3;
      npc.damage = (int) ((double) npc.damage * 1.2);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      NPC.golemBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      foreach (Player player in Main.player)
      {
        if (((Entity) player).active && (double) ((Entity) player).Distance(((Entity) npc).Center) < 2000.0)
          player.AddBuff(ModContent.BuffType<LowGroundBuff>(), 2, true, false);
      }
      this.HealPerSecond = WorldSavingSystem.MasochistModeReal ? 360 : 180;
      if (!this.IsInTemple)
      {
        this.HealPerSecond *= 2;
        ((Entity) npc).position.X += ((Entity) npc).velocity.X / 2f;
        if ((double) ((Entity) npc).velocity.Y < 0.0)
        {
          ((Entity) npc).position.Y += ((Entity) npc).velocity.Y * 0.5f;
          if ((double) ((Entity) npc).velocity.Y > -2.0)
            ((Entity) npc).velocity.Y = 20f;
        }
      }
      if ((double) ((Entity) npc).velocity.Y < 0.0)
      {
        if (!this.HaveBoostedJumpHeight)
        {
          this.HaveBoostedJumpHeight = true;
          ((Entity) npc).velocity.Y *= 1.25f;
          if (!this.IsInTemple && (double) ((Entity) Main.player[npc.target]).Center.Y < (double) ((Entity) npc).Center.Y - 480.0)
            ((Entity) npc).velocity.Y *= 1.5f;
        }
      }
      else
        this.HaveBoostedJumpHeight = false;
      if (this.DoStompBehaviour)
      {
        if ((double) ((Entity) npc).velocity.Y == 0.0)
        {
          this.DoStompBehaviour = false;
          this.IsInTemple = Golem.CheckTempleWalls(((Entity) npc).Center);
          if (this.IsInTemple)
          {
            ++this.StompAttackCounter;
            if (this.StompAttackCounter == 1)
            {
              if (WorldSavingSystem.MasochistModeReal)
                ++this.StompAttackCounter;
              Vector2 center;
              // ISSUE: explicit constructor call
              ((Vector2) ref center).\u002Ector(((Entity) npc).position.X, ((Entity) npc).Center.Y);
              center.X -= (float) (((Entity) npc).width * 7);
              for (int index = 0; index < 6; ++index)
              {
                int num1 = (int) center.X / 16 + ((Entity) npc).width * index * 3 / 16;
                int num2 = (int) center.Y / 16;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num1 * 16 + 8), (float) (num2 * 16 + 8), 0.0f, 0.0f, ModContent.ProjectileType<GolemGeyser2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
              }
              center = ((Entity) npc).Center;
              for (int index = -3; index <= 3; ++index)
              {
                int num3 = (int) center.X / 16 + ((Entity) npc).width * index * 3 / 16;
                int num4 = (int) center.Y / 16;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num3 * 16 + 8), (float) (num4 * 16 + 8), 0.0f, 0.0f, ModContent.ProjectileType<GolemGeyser>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
              }
            }
            else if (this.StompAttackCounter != 2)
            {
              if (this.StompAttackCounter == 3)
              {
                if (WorldSavingSystem.MasochistModeReal)
                  this.StompAttackCounter = 0;
                if (npc.HasPlayerTarget)
                {
                  if (!Main.dedServ)
                    ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.5f);
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 683, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  for (int index1 = -2; index1 <= 2; ++index1)
                  {
                    int num5 = (int) ((Entity) Main.player[npc.target]).Center.X / 16;
                    int num6 = (int) ((Entity) Main.player[npc.target]).Center.Y / 16;
                    int num7 = num5 + 4 * index1;
                    for (int index2 = 0; index2 < 100; ++index2)
                    {
                      Tile tileSafely1 = Framing.GetTileSafely(num7, num6);
                      if (((Tile) ref tileSafely1).HasUnactuatedTile)
                      {
                        bool[] tileSolid = Main.tileSolid;
                        Tile tileSafely2 = Framing.GetTileSafely(num7, num6);
                        int index3 = (int) ((Tile) ref tileSafely2).TileType;
                        if (tileSolid[index3])
                          --num6;
                        else
                          break;
                      }
                      else
                        break;
                    }
                    for (int index4 = 0; index4 < 100; ++index4)
                    {
                      Tile tileSafely3 = Framing.GetTileSafely(num7, num6);
                      if (((Tile) ref tileSafely3).HasUnactuatedTile)
                      {
                        bool[] tileSolid = Main.tileSolid;
                        Tile tileSafely4 = Framing.GetTileSafely(num7, num6);
                        int index5 = (int) ((Tile) ref tileSafely4).TileType;
                        if (tileSolid[index5])
                          break;
                      }
                      --num6;
                    }
                    Vector2 vector2;
                    // ISSUE: explicit constructor call
                    ((Vector2) ref vector2).\u002Ector((float) (num7 * 16 + 8), (float) (num6 * 16 + 8));
                    if (FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<GolemBoulder>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
              }
              else
                this.StompAttackCounter = 0;
            }
          }
          else
          {
            Vector2 vector2_1;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_1).\u002Ector(((Entity) npc).position.X, ((Entity) npc).Center.Y);
            vector2_1.X -= (float) (((Entity) npc).width * 7);
            for (int index6 = 0; index6 < 6; ++index6)
            {
              int num8 = (int) vector2_1.X / 16 + ((Entity) npc).width * index6 * 3 / 16;
              int num9 = (int) vector2_1.Y / 16;
              for (int index7 = 0; index7 < 100; ++index7)
              {
                Tile tileSafely5 = Framing.GetTileSafely(num8, num9);
                if (((Tile) ref tileSafely5).HasUnactuatedTile)
                {
                  bool[] tileSolid = Main.tileSolid;
                  Tile tileSafely6 = Framing.GetTileSafely(num8, num9);
                  int index8 = (int) ((Tile) ref tileSafely6).TileType;
                  if (tileSolid[index8])
                    break;
                }
                ++num9;
              }
              if (FargoSoulsUtil.HostCheck)
              {
                if (npc.HasPlayerTarget && (double) ((Entity) Main.player[npc.target]).position.Y > (double) (num9 * 16))
                {
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num8 * 16 + 8), (float) (num9 * 16 + 8), 6.3f, 6.3f, 188, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num8 * 16 + 8), (float) (num9 * 16 + 8), -6.3f, 6.3f, 188, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num8 * 16 + 8), (float) (num9 * 16 + 8), 0.0f, -8f, 654, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num8 * 16 + 8), (float) (num9 * 16 + 8 - 640), 0.0f, -8f, 654, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num8 * 16 + 8), (float) (num9 * 16 + 8 - 640), 0.0f, 8f, 654, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if (npc.HasPlayerTarget)
            {
              for (int index9 = -3; index9 <= 3; ++index9)
              {
                int num10 = (int) ((Entity) Main.player[npc.target]).Center.X / 16;
                int num11 = (int) ((Entity) Main.player[npc.target]).Center.Y / 16;
                int num12 = num10 + 10 * index9;
                for (int index10 = 0; index10 < 30; ++index10)
                {
                  Tile tileSafely7 = Framing.GetTileSafely(num12, num11);
                  if (((Tile) ref tileSafely7).HasUnactuatedTile)
                  {
                    bool[] tileSolid = Main.tileSolid;
                    Tile tileSafely8 = Framing.GetTileSafely(num12, num11);
                    int index11 = (int) ((Tile) ref tileSafely8).TileType;
                    if (tileSolid[index11])
                      break;
                  }
                  --num11;
                }
                Vector2 vector2_2;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_2).\u002Ector((float) (num12 * 16 + 8), (float) (num11 * 16 + 8));
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, Vector2.Zero, ModContent.ProjectileType<GolemBoulder>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
          }
        }
      }
      else if ((double) ((Entity) npc).velocity.Y > 0.0)
        this.DoStompBehaviour = true;
      if (WorldSavingSystem.MasochistModeReal && ++this.SpikyBallTimer >= 900)
      {
        if (Golem.CheckTempleWalls(((Entity) npc).Center))
        {
          if ((double) ((Entity) npc).velocity.Y > 0.0)
          {
            this.SpikyBallTimer = WorldSavingSystem.MasochistModeReal ? 600 : 0;
            for (int index = 0; index < 8; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), Utils.NextFloat(Main.rand, -0.3f, 0.3f), Utils.NextFloat(Main.rand, -10f, -6f), ModContent.ProjectileType<GolemSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        else
        {
          this.SpikyBallTimer = 600;
          for (int index = 0; index < 16; ++index)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), Utils.NextFloat(Main.rand, -1f, 1f), (float) Main.rand.Next(-20, -9), ModContent.ProjectileType<GolemSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      EModeUtils.DropSummon(npc, "LihzahrdPowerCell2", NPC.downedGolemBoss, ref this.DroppedSummon, NPC.downedPlantBoss);
      return flag;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.9f);
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      for (int type = 1; type <= 3; ++type)
        EModeNPCBehaviour.LoadGolem(recolor, type);
      EModeNPCBehaviour.LoadExtra(recolor, 107);
      GolemPart.LoadGolemSpriteBuffered(recolor, 5, TextureAssets.NpcHeadBoss, FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPCHeadBoss, "NPC_Head_Boss_");
    }

    public static bool CheckTempleWalls(Vector2 pos)
    {
      Tile tileSafely = Framing.GetTileSafely(pos);
      int num = (int) ((Tile) ref tileSafely).WallType;
      Mod mod;
      ModWall modWall;
      return num == 87 || Terraria.ModLoader.ModLoader.TryGetMod("Remnants", ref mod) && mod.TryFind<ModWall>("temple", ref modWall) && num == (int) ((ModBlockType) modWall).Type;
    }
  }
}
