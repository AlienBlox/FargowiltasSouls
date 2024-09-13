// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.EModeGlobalTile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Tiles
{
  public class EModeGlobalTile : GlobalTile
  {
    public virtual void NearbyEffects(int i, int j, int type, bool closer)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      if (type == 226)
      {
        Tile tileSafely = Framing.GetTileSafely(i, j);
        if (((Tile) ref tileSafely).WallType == (ushort) 87 && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && (double) ((Entity) Main.LocalPlayer).Distance(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8))) < 3000.0)
          Main.LocalPlayer.AddBuff(ModContent.BuffType<LihzahrdCurseBuff>(), 10, true, false);
      }
      if (type != 237 || !((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost || (double) ((Entity) Main.LocalPlayer).Distance(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8))) >= 3000.0 || !Collision.CanHit(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8)), 0, 0, ((Entity) Main.LocalPlayer).Center, 0, 0))
        return;
      Tile tileSafely1 = Framing.GetTileSafely(((Entity) Main.LocalPlayer).Center);
      if (((Tile) ref tileSafely1).WallType != (ushort) 87)
        return;
      if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<LihzahrdBlessingBuff>()))
      {
        Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Buffs.LihzahrdBlessingBuff.Message"), new Color?(Color.Orange));
        SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 50; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) Main.LocalPlayer).position, ((Entity) Main.LocalPlayer).width, ((Entity) Main.LocalPlayer).height, 6, 0.0f, 0.0f, 0, new Color(), Utils.NextFloat(Main.rand, 3f, 6f));
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
        }
      }
      Main.LocalPlayer.AddBuff(ModContent.BuffType<LihzahrdBlessingBuff>(), 35999, true, false);
    }

    private static bool CanBreakTileMaso(int i, int j, int type)
    {
      if (type == 137 || type == 135)
      {
        Tile tileSafely1 = Framing.GetTileSafely(i, j);
        if (((Tile) ref tileSafely1).WallType == (ushort) 87)
        {
          int closest = (int) Player.FindClosest(new Vector2((float) (i * 16 + 8), (float) (j * 16 + 8)), 0, 0);
          if (closest != -1)
          {
            Tile tileSafely2 = Framing.GetTileSafely(((Entity) Main.player[closest]).Center);
            if (((Tile) ref tileSafely2).WallType == (ushort) 87 && !Main.player[closest].FargoSouls().LihzahrdCurse)
              return true;
          }
          return false;
        }
      }
      return true;
    }

    public virtual bool CanExplode(int i, int j, int type)
    {
      if (!WorldSavingSystem.EternityMode)
        return ((GlobalBlockType) this).CanExplode(i, j, type);
      return EModeGlobalTile.CanBreakTileMaso(i, j, type) && ((GlobalBlockType) this).CanExplode(i, j, type);
    }

    public virtual bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.CanKillTile(i, j, type, ref blockDamaged);
      return EModeGlobalTile.CanBreakTileMaso(i, j, type) && base.CanKillTile(i, j, type, ref blockDamaged);
    }

    public virtual void KillTile(
      int i,
      int j,
      int type,
      ref bool fail,
      ref bool effectOnly,
      ref bool noItem)
    {
      if (!WorldSavingSystem.EternityMode || type != 31 || Main.invasionType != 0 || NPC.downedGoblins || !WorldGen.shadowOrbSmashed)
        return;
      int closest = (int) Player.FindClosest(new Vector2((float) (i * 16), (float) (j * 16)), 0, 0);
      if (closest == -1 || Main.player[closest].statLifeMax2 < 200)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        Main.invasionDelay = 0;
        Main.StartInvasion(1);
      }
      else
        NetMessage.SendData(61, -1, -1, (NetworkText) null, closest, -1f, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
