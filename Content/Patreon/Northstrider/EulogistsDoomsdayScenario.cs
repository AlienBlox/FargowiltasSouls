// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Northstrider.EulogistsDoomsdayScenario
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Northstrider
{
  public class EulogistsDoomsdayScenario : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 1;
      this.Item.value = 100;
      this.Item.useStyle = 2;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
    }

    public virtual bool? UseItem(Player player)
    {
      Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), ((Entity) player).Center.X, ((Entity) player).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<Explosion>(), 0, 5f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      int num1 = 15;
      Vector2 center = ((Entity) player).Center;
      for (int index1 = -num1; index1 <= num1; ++index1)
      {
        for (int index2 = -num1; index2 <= num1; ++index2)
        {
          if (Math.Sqrt((double) (index1 * index1 + index2 * index2)) <= (double) num1)
          {
            int num2 = (int) ((double) index1 + (double) center.X / 16.0);
            int num3 = (int) ((double) index2 + (double) center.Y / 16.0);
            if (num2 >= 0 && num2 < Main.maxTilesX && num3 >= 0 && num3 < Main.maxTilesY)
            {
              Tile tile = ((Tilemap) ref Main.tile)[num2, num3];
              if (!Tile.op_Equality(tile, (ArgumentException) null) && WorldGen.InWorld(num2, num3, 0))
              {
                WorldGen.KillTile(num2, num3, false, false, true);
                ((Tile) ref tile).ClearEverything();
                Main.Map.Update(num2, num3, byte.MaxValue);
              }
            }
          }
        }
      }
      Main.refreshMap = true;
      if (!Main.dedServ)
      {
        SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      }
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && npc.townNPC && (double) Vector2.Distance(((Entity) player).Center, ((Entity) npc).Center) <= (double) (num1 * 14))
          npc.StrikeInstantKill();
      }
      player.KillMe(PlayerDeathReason.ByPlayerItem(((Entity) player).whoAmI, this.Item), 9999.0, 0, false);
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(257, 1).AddIngredient(167, 50).AddTile(18).Register();
    }
  }
}
