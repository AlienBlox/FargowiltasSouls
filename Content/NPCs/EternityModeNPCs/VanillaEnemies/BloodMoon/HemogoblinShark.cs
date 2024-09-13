// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.HemogoblinShark
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class HemogoblinShark : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(620);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer < 360)
      {
        if (!npc.HasValidTarget || Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
          return;
        this.AttackTimer += 9;
      }
      else if (this.AttackTimer == 370)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 8f, 180f, 0.0f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 8f, 200f, 0.0f);
      }
      else
      {
        if (this.AttackTimer < 415)
          return;
        this.AttackTimer = 0;
        if (!FargoSoulsUtil.HostCheck || !npc.HasValidTarget)
          return;
        for (int index = 0; index < 10; ++index)
        {
          Vector2 targetSpot = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.NextVector2Circular(Main.rand, 8f, 8f));
          Vector2 worldCoordinates = Utils.ToWorldCoordinates(HemogoblinShark.FindSharpTearsSpot(Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0) ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center, targetSpot), (float) Main.rand.Next(17), (float) Main.rand.Next(17));
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), worldCoordinates, Vector2.op_Multiply(16f, Vector2.Normalize(Vector2.op_Subtraction(targetSpot, worldCoordinates))), 756, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, Utils.NextFloat(Main.rand, 0.5f, 1f), 0.0f);
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
    }

    private static Point FindSharpTearsSpot(Vector2 origin, Vector2 targetSpot)
    {
      Utils.ToTileCoordinates(targetSpot);
      Vector2 vector2_1 = origin;
      Vector2 vector2_2 = targetSpot;
      int num1 = 3;
      float num2 = 4f;
      Vector2 vector2_3;
      float[] numArray;
      Collision.AimingLaserScan(vector2_1, vector2_2, num2, num1, ref vector2_3, ref numArray);
      float num3 = float.PositiveInfinity;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if ((double) numArray[index] < (double) num3)
          num3 = numArray[index];
      }
      targetSpot = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Utils.SafeNormalize(vector2_3, Vector2.Zero), num3));
      Point tileCoordinates = Utils.ToTileCoordinates(targetSpot);
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(tileCoordinates.X, tileCoordinates.Y, 1, 1);
      ((Rectangle) ref rectangle1).Inflate(6, 16);
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, 0, Main.maxTilesX, Main.maxTilesY);
      ((Rectangle) ref rectangle2).Inflate(-40, -40);
      rectangle1 = Rectangle.Intersect(rectangle1, rectangle2);
      List<Point> pointList1 = new List<Point>();
      List<Point> pointList2 = new List<Point>();
      for (int left = ((Rectangle) ref rectangle1).Left; left <= ((Rectangle) ref rectangle1).Right; ++left)
      {
        for (int top = ((Rectangle) ref rectangle1).Top; top <= ((Rectangle) ref rectangle1).Bottom; ++top)
        {
          if (WorldGen.SolidTile(left, top, false))
          {
            Vector2 vector2_4;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_4).\u002Ector((float) (left * 16 + 8), (float) (top * 16 + 8));
            if ((double) Vector2.Distance(targetSpot, vector2_4) <= 200.0)
            {
              if (HemogoblinShark.FindSharpTearsOpening(left, top, left > tileCoordinates.X, left < tileCoordinates.X, top > tileCoordinates.Y, top < tileCoordinates.Y))
                pointList1.Add(new Point(left, top));
              else
                pointList2.Add(new Point(left, top));
            }
          }
        }
      }
      if (pointList1.Count == 0 && pointList2.Count == 0)
        pointList1.Add(Utils.ToPoint(Vector2.op_Addition(Utils.ToVector2(Utils.ToTileCoordinates(origin)), Utils.NextVector2Square(Main.rand, -2f, 2f))));
      List<Point> pointList3 = pointList1;
      if (pointList3.Count == 0)
        pointList3 = pointList2;
      int index1 = Main.rand.Next(pointList3.Count);
      return pointList3[index1];
    }

    private static bool FindSharpTearsOpening(
      int x,
      int y,
      bool acceptLeft,
      bool acceptRight,
      bool acceptUp,
      bool acceptDown)
    {
      if (acceptLeft && !WorldGen.SolidTile(x - 1, y, false) || acceptRight && !WorldGen.SolidTile(x + 1, y, false) || acceptUp && !WorldGen.SolidTile(x, y - 1, false))
        return true;
      return acceptDown && !WorldGen.SolidTile(x, y + 1, false);
    }
  }
}
