// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.BossRush
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class BossRush : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_454";

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        this.Projectile.timeLeft = 2;
        if ((double) --this.Projectile.ai[1] >= 0.0)
          return;
        this.Projectile.ai[1] = 180f;
        this.Projectile.netUpdate = true;
        switch ((int) this.Projectile.localAI[0]++)
        {
          case 0:
            NPC.SpawnOnPlayer(npc.target, 4);
            if (!Main.dayTime)
              break;
            Main.dayTime = false;
            Main.time = 0.0;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 1:
            NPC.SpawnOnPlayer(npc.target, 13);
            NPC.SpawnOnPlayer(npc.target, 266);
            break;
          case 2:
            NPC.SpawnOnPlayer(npc.target, 222);
            break;
          case 3:
            this.ManualSpawn(npc, 35);
            if (!Main.dayTime)
              break;
            Main.dayTime = false;
            Main.time = 0.0;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 4:
            NPC.SpawnOnPlayer(npc.target, 125);
            NPC.SpawnOnPlayer(npc.target, 126);
            if (!Main.dayTime)
              break;
            Main.dayTime = false;
            Main.time = 0.0;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 5:
            this.ManualSpawn(npc, (int) sbyte.MaxValue);
            if (!Main.dayTime)
              break;
            Main.dayTime = false;
            Main.time = 0.0;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 6:
            NPC.SpawnOnPlayer(npc.target, 262);
            break;
          case 7:
            this.ManualSpawn(npc, 245);
            break;
          case 8:
            this.ManualSpawn(npc, 551);
            break;
          case 9:
            this.ManualSpawn(npc, 370);
            break;
          case 10:
            this.ManualSpawn(npc, 398);
            break;
          default:
            if (!Main.dayTime)
            {
              Main.dayTime = true;
              Main.time = 27000.0;
              if (Main.netMode == 2)
                NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            this.Projectile.Kill();
            break;
        }
      }
    }

    private void ManualSpawn(NPC npc, int type)
    {
      if (!FargoSoulsUtil.HostCheck || FargoSoulsUtil.NewNPCEasy(Entity.InheritSource((Entity) this.Projectile), ((Entity) npc).Center, type, velocity: new Vector2()) == Main.maxNPCs)
        return;
      FargoSoulsUtil.PrintLocalization("Announcement.HasAwoken", new Color(175, 75, (int) byte.MaxValue), (object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".NPCs.MutantBoss.DisplayName"));
    }
  }
}
