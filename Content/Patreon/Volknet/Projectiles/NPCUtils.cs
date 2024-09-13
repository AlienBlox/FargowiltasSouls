// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.NPCUtils
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  public static class NPCUtils
  {
    public static bool AnyBosses()
    {
      foreach (NPC npc in Main.npc)
      {
        if (((Entity) npc).active && (npc.boss || npc.type >= 14 && npc.type <= 15))
          return true;
      }
      return false;
    }

    public static bool AnyProj(int type)
    {
      foreach (Projectile projectile in Main.projectile)
      {
        if (((Entity) projectile).active && projectile.type == type)
          return true;
      }
      return false;
    }

    public static bool AnyProj(int type, int owner)
    {
      foreach (Projectile projectile in Main.projectile)
      {
        if (((Entity) projectile).active && projectile.type == type && projectile.owner == owner)
          return true;
      }
      return false;
    }
  }
}
