// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.PlanterasTentacle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class PlanterasTentacle : PlanteraPart
  {
    public int ChangeDirectionTimer;
    public int RotationDirection;
    public int MaxDistanceFromPlantera;
    public int CanHitTimer;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(264);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.ChangeDirectionTimer);
      binaryWriter.Write7BitEncodedInt(this.RotationDirection);
      binaryWriter.Write7BitEncodedInt(this.MaxDistanceFromPlantera);
      binaryWriter.Write7BitEncodedInt(this.CanHitTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.ChangeDirectionTimer = binaryReader.Read7BitEncodedInt();
      this.RotationDirection = binaryReader.Read7BitEncodedInt();
      this.MaxDistanceFromPlantera = binaryReader.Read7BitEncodedInt();
      this.CanHitTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.MaxDistanceFromPlantera = 200;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return base.CanHitPlayer(npc, target, ref CooldownSlot) && this.CanHitTimer > 60;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      NPC npc1 = FargoSoulsUtil.NPCExists(NPC.plantBoss, new int[1]
      {
        262
      });
      if (npc1 != null)
      {
        NPC npc2 = npc;
        ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Division(((Entity) npc1).velocity, 3f));
        if ((double) ((Entity) npc).Distance(((Entity) npc1).Center) > (double) this.MaxDistanceFromPlantera)
        {
          Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) npc).Center), Vector2.op_Multiply((float) this.MaxDistanceFromPlantera, Utils.RotatedBy(((Entity) npc1).DirectionFrom(((Entity) npc).Center), (double) MathHelper.ToRadians(45f) * (double) this.RotationDirection, new Vector2())));
          ((Entity) npc).velocity = Vector2.Lerp(((Entity) npc).velocity, Vector2.op_Division(vector2, 15f), 0.05f);
        }
      }
      if (++this.ChangeDirectionTimer > 120)
      {
        this.ChangeDirectionTimer = Main.rand.Next(30);
        if (FargoSoulsUtil.HostCheck)
        {
          this.RotationDirection = Utils.NextBool(Main.rand) ? -1 : 1;
          this.MaxDistanceFromPlantera = 50 + Main.rand.Next(150);
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      ++this.CanHitTimer;
      return flag;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
