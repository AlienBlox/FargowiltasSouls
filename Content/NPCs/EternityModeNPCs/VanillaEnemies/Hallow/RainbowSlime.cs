// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow.RainbowSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow
{
  public class RainbowSlime : EModeNPCBehaviour
  {
    public int Counter;
    public bool SpawnedByOtherSlime;
    public bool DoStompAttack;
    public bool FinishedSpawning;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(244);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.SpawnedByOtherSlime);
      bitWriter.WriteBit(this.DoStompAttack);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.SpawnedByOtherSlime = bitReader.ReadBit();
      this.DoStompAttack = bitReader.ReadBit();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.Counter;
      if (!this.SpawnedByOtherSlime && Main.netMode == 2 && this.Counter <= 65 && this.Counter % 15 == 5)
      {
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (!this.SpawnedByOtherSlime && !this.FinishedSpawning)
      {
        this.FinishedSpawning = true;
        npc.lifeMax *= 5;
        npc.life = npc.lifeMax;
        npc.HealEffect(npc.lifeMax, true);
        ((Entity) npc).Center = ((Entity) npc).Bottom;
        npc.scale *= 3f;
        NPC npc1 = npc;
        ((Entity) npc1).width = ((Entity) npc1).width * 3;
        NPC npc2 = npc;
        ((Entity) npc2).height = ((Entity) npc2).height * 3;
        ((Entity) npc).Bottom = ((Entity) npc).Center;
      }
      npc.dontTakeDamage = this.Counter < 30;
      if (this.DoStompAttack)
      {
        if ((double) ((Entity) npc).velocity.Y != 0.0)
          return;
        this.DoStompAttack = false;
        if (!npc.HasPlayerTarget || !FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), Vector2.op_Multiply(((Entity) Main.player[npc.target]).velocity, 30f));
        vector2.X /= 120f;
        vector2.Y = (float) ((double) vector2.Y / 120.0 - 9.0);
        float num1 = (float) (this.SpawnedByOtherSlime ? 1 : 0);
        int num2 = this.SpawnedByOtherSlime ? 3 : 25;
        float num3 = this.SpawnedByOtherSlime ? 0.5f : 1.5f;
        for (int index = 0; index < num2; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(num3, Utils.NextVector2Circular(Main.rand, -1f, 1f))), ModContent.ProjectileType<RainbowSlimeSpike>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.5f), 0.0f, Main.myPlayer, num1, 0.0f, 0.0f);
      }
      else
      {
        if ((double) ((Entity) npc).velocity.Y <= 0.0)
          return;
        this.DoStompAttack = true;
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(137, 120, true, false);
      target.AddBuff(ModContent.BuffType<FlamesoftheUniverseBuff>(), 240, true, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!this.SpawnedByOtherSlime)
      {
        ((Entity) npc).active = false;
        if (npc.DeathSound.HasValue)
        {
          SoundStyle soundStyle = npc.DeathSound.Value;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        }
        if (FargoSoulsUtil.HostCheck)
        {
          for (int index1 = 0; index1 < 4; ++index1)
          {
            int index2 = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) ((double) ((Entity) npc).position.X + (double) (((Entity) npc).width / 2)), (int) ((double) ((Entity) npc).position.Y + (double) ((Entity) npc).height), 244, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            if (index2 != Main.maxNPCs)
            {
              NPC npc1 = Main.npc[index2];
              npc1.GetGlobalNPC<RainbowSlime>().SpawnedByOtherSlime = true;
              ((Entity) npc1).velocity = new Vector2((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 1));
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) npc).Center, ((Entity) npc).width, ((Entity) npc).height, 267, (float) (-(double) ((Entity) npc).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) npc).velocity.Y * 0.20000000298023224), 100, new Color(), 5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
          int index5 = Dust.NewDust(((Entity) npc).Center, ((Entity) npc).width, ((Entity) npc).height, 267, (float) (-(double) ((Entity) npc).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) npc).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        }
        return false;
      }
      if (FargoSoulsUtil.HostCheck)
      {
        int[] numArray = new int[4]{ 183, -4, 122, 81 };
        for (int index6 = 0; index6 < numArray.Length; ++index6)
        {
          if (Utils.NextBool(Main.rand, 3))
          {
            int index7 = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) ((double) ((Entity) npc).position.X + (double) (((Entity) npc).width / 2)), (int) ((double) ((Entity) npc).position.Y + (double) ((Entity) npc).height), 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            Main.npc[index7].SetDefaults(numArray[index6], new NPCSpawnParams());
            ((Entity) Main.npc[index7]).velocity.X = ((Entity) npc).velocity.X * 2f;
            ((Entity) Main.npc[index7]).velocity.Y = ((Entity) npc).velocity.Y;
            ((Entity) Main.npc[index7]).velocity.X += (float) ((double) Main.rand.Next(-20, 20) * 0.10000000149011612 + (double) (index6 * ((Entity) npc).direction) * 0.30000001192092896);
            ((Entity) Main.npc[index7]).velocity.Y -= (float) Main.rand.Next(0, 10) * 0.1f + (float) index6;
            Main.npc[index7].ai[0] = (float) (-1000 * Main.rand.Next(3));
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index7, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
      return base.CheckDead(npc);
    }
  }
}
