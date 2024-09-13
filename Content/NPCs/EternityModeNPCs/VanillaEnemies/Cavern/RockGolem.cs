// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.RockGolem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class RockGolem : EModeNPCBehaviour
  {
    public int JumpTimer;
    public bool Jumped;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(631);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.JumpTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.JumpTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.JumpTimer = 300 + Main.rand.Next(60);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (this.JumpTimer > 360)
      {
        this.JumpTimer = 0;
        this.Jumped = true;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2;
          if (((Entity) Main.player[index]).active && !Main.player[index].dead && !Main.player[index].ghost)
          {
            vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) npc).Bottom);
          }
          else
          {
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((double) ((Entity) npc).Center.X < (double) ((Entity) Main.player[index]).Center.X ? -300f : 300f, -100f);
          }
          vector2.X /= 90f;
          vector2.Y = (float) ((double) vector2.Y / 90.0 - 18.0);
          npc.ai[1] = 90f;
          npc.ai[2] = vector2.X;
          npc.ai[3] = vector2.Y;
          npc.netUpdate = true;
        }
        return false;
      }
      if (this.JumpTimer == 330)
      {
        ++this.JumpTimer;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if ((double) npc.ai[1] > 0.0)
      {
        --npc.ai[1];
        npc.noTileCollide = true;
        ((Entity) npc).velocity.X = npc.ai[2];
        ((Entity) npc).velocity.Y = npc.ai[3];
        npc.ai[3] += 0.4f;
        int num = 2;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 1, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
        }
        this.JumpTimer = 0;
        ++this.JumpTimer;
        return false;
      }
      if (npc.noTileCollide)
      {
        this.JumpTimer = 0;
        npc.noTileCollide = Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height);
        return false;
      }
      if (npc.HasValidTarget && (Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0) || npc.life < npc.lifeMax / 2))
        ++this.JumpTimer;
      if ((double) ((Entity) npc).velocity.Y == 0.0 && this.Jumped)
      {
        this.Jumped = false;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 670, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      return flag;
    }
  }
}
