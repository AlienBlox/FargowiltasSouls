// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.FargoSoulsUtil
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls
{
  public static class FargoSoulsUtil
  {
    private static readonly FieldInfo shaderTextureField = typeof (MiscShaderData).GetField("_uImage1", (BindingFlags) 36);
    private static readonly FieldInfo shaderTextureField2 = typeof (MiscShaderData).GetField("_uImage2", (BindingFlags) 36);
    [Obsolete("Use Luminance's Utilities.UniversalBindingFlags instead.", false)]
    public static readonly BindingFlags UniversalBindingFlags = Luminance.Common.Utilities.Utilities.UniversalBindingFlags;

    public static void CreatePerspectiveMatrixes(out Matrix view, out Matrix projection)
    {
      Viewport viewport = ((Game) Main.instance).GraphicsDevice.Viewport;
      int height = ((Viewport) ref viewport).Height;
      Vector2 zoom = Main.GameViewMatrix.Zoom;
      Matrix scale = Matrix.CreateScale(zoom.X, zoom.Y, 1f);
      view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);
      view = Matrix.op_Multiply(view, Matrix.CreateTranslation(0.0f, (float) -height, 0.0f));
      view = Matrix.op_Multiply(view, Matrix.CreateRotationZ(3.14159274f));
      if ((double) Main.LocalPlayer.gravDir == -1.0)
        view = Matrix.op_Multiply(view, Matrix.op_Multiply(Matrix.CreateScale(1f, -1f, 1f), Matrix.CreateTranslation(0.0f, (float) height, 0.0f)));
      view = Matrix.op_Multiply(view, scale);
      projection = Matrix.op_Multiply(Matrix.CreateOrthographicOffCenter(0.0f, (float) Main.screenWidth * zoom.X, 0.0f, (float) Main.screenHeight * zoom.Y, 0.0f, 1f), scale);
    }

    public static void SetShaderTexture(this MiscShaderData shader, Asset<Texture2D> texture)
    {
      FargoSoulsUtil.shaderTextureField.SetValue((object) shader, (object) texture);
    }

    public static void SetShaderTexture2(this MiscShaderData shader, Asset<Texture2D> texture)
    {
      FargoSoulsUtil.shaderTextureField2.SetValue((object) shader, (object) texture);
    }

    public static void EnterShaderRegion(this SpriteBatch spriteBatch)
    {
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
    }

    public static void ExitShaderRegion(this SpriteBatch spriteBatch)
    {
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
    }

    public static void SetTexture1(this Texture2D texture)
    {
      ((Game) Main.instance).GraphicsDevice.Textures[1] = (Texture) texture;
    }

    public static void SetTexture2(this Texture2D texture)
    {
      ((Game) Main.instance).GraphicsDevice.Textures[2] = (Texture) texture;
    }

    public static Vector4 ToVector4(this Rectangle rectangle)
    {
      return new Vector4((float) rectangle.X, (float) rectangle.Y, (float) rectangle.Width, (float) rectangle.Height);
    }

    public static string VanillaTextureProjectile(int projectileID)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
      interpolatedStringHandler.AppendLiteral("Terraria/Images/Projectile_");
      interpolatedStringHandler.AppendFormatted<int>(projectileID);
      return interpolatedStringHandler.ToStringAndClear();
    }

    public static string VanillaTextureNPC(int npcID)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 1);
      interpolatedStringHandler.AppendLiteral("Terraria/Images/NPC_");
      interpolatedStringHandler.AppendFormatted<int>(npcID);
      return interpolatedStringHandler.ToStringAndClear();
    }

    public static void GenericProjectileDraw(
      Projectile projectile,
      Color lightColor,
      Texture2D texture = null,
      Vector2? drawPos = null,
      float? rotation = null)
    {
      rotation.GetValueOrDefault();
      if (!rotation.HasValue)
        rotation = new float?(projectile.rotation);
      drawPos.GetValueOrDefault();
      if (!drawPos.HasValue)
        drawPos = new Vector2?(((Entity) projectile).Center);
      if (texture == null)
        texture = TextureAssets.Projectile[projectile.type].Value;
      int num1 = texture.Height / Main.projFrames[projectile.type];
      int num2 = projectile.frame * num1;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture, Vector2.op_Addition(Vector2.op_Subtraction(drawPos.Value, Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY)), new Rectangle?(rectangle), projectile.GetAlpha(lightColor), rotation.Value, vector2, projectile.scale, spriteEffects, 0.0f);
    }

    public static void ProjectileWithTrailDraw(
      Projectile projectile,
      Color lightColor,
      Texture2D texture = null,
      int? trailLength = null,
      bool additiveTrail = false,
      bool alsoAdditiveMainSprite = true)
    {
      if (texture == null)
        texture = TextureAssets.Projectile[projectile.type].Value;
      int num1 = texture.Height / Main.projFrames[projectile.type];
      int num2 = projectile.frame * num1;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      trailLength.GetValueOrDefault();
      if (!trailLength.HasValue)
        trailLength = new int?(ProjectileID.Sets.TrailCacheLength[projectile.type]);
      if (additiveTrail)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
      }
      float? nullable1;
      for (int index = 0; index < trailLength.Value; ++index)
      {
        Color color1 = Color.op_Multiply(lightColor, 0.75f);
        int? nullable2 = trailLength;
        int num3 = index;
        int? nullable3;
        int? nullable4;
        if (!nullable2.HasValue)
        {
          nullable3 = new int?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new int?(nullable2.GetValueOrDefault() - num3);
        nullable3 = nullable4;
        float num4 = (float) nullable3.Value;
        nullable3 = trailLength;
        float? nullable5 = nullable3.HasValue ? new float?((float) nullable3.GetValueOrDefault()) : new float?();
        nullable1 = nullable5.HasValue ? new float?(num4 / nullable5.GetValueOrDefault()) : new float?();
        Color? nullable6;
        Color? nullable7;
        if (!nullable1.HasValue)
        {
          nullable6 = new Color?();
          nullable7 = nullable6;
        }
        else
          nullable7 = new Color?(Color.op_Multiply(color1, nullable1.GetValueOrDefault()));
        nullable6 = nullable7;
        Color color2 = nullable6.Value;
        Vector2 vector2_2 = Vector2.op_Addition(projectile.oldPos[index], Vector2.op_Division(((Entity) projectile).Size, 2f));
        float num5 = projectile.oldRot[index];
        Main.spriteBatch.Draw(texture, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY)), new Rectangle?(rectangle), projectile.GetAlpha(color2), num5, vector2_1, projectile.scale, spriteEffects, 0.0f);
      }
      if (additiveTrail && !alsoAdditiveMainSprite)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      Projectile projectile1 = projectile;
      Color lightColor1 = lightColor;
      Texture2D texture1 = texture;
      Vector2? drawPos = new Vector2?();
      nullable1 = new float?();
      float? rotation = nullable1;
      FargoSoulsUtil.GenericProjectileDraw(projectile1, lightColor1, texture1, drawPos, rotation);
      if (!(additiveTrail & alsoAdditiveMainSprite))
        return;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
    }

    public static string EmptyTexture => "FargowiltasSouls/Content/Projectiles/Empty";

    public static bool WorldIsExpertOrHarder()
    {
      if (Main.expertMode)
        return true;
      GameModeData gameModeInfo = Main.GameModeInfo;
      return ((GameModeData) ref gameModeInfo).IsJourneyMode && (double) CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs >= 2.0;
    }

    public static bool WorldIsMaster()
    {
      if (Main.masterMode)
        return true;
      GameModeData gameModeInfo = Main.GameModeInfo;
      return ((GameModeData) ref gameModeInfo).IsJourneyMode && (double) CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs >= 3.0;
    }

    public static bool HostCheck => Main.netMode != 1;

    public static void AddDebuffFixedDuration(
      Player player,
      int buffID,
      int intendedTime,
      bool quiet = true)
    {
      if (FargoSoulsUtil.WorldIsExpertOrHarder() && BuffID.Sets.LongerExpertDebuff[buffID])
      {
        GameModeData gameModeInfo = Main.GameModeInfo;
        float debuffTimeMultiplier = ((GameModeData) ref gameModeInfo).DebuffTimeMultiplier;
        GameModeData gameModeData = Main.GameModeInfo;
        if (((GameModeData) ref gameModeData).IsJourneyMode)
        {
          if (Main.masterMode)
          {
            gameModeData = Main.RegisteredGameModes[2];
            debuffTimeMultiplier = ((GameModeData) ref gameModeData).DebuffTimeMultiplier;
          }
          else if (Main.expertMode)
          {
            gameModeData = Main.RegisteredGameModes[1];
            debuffTimeMultiplier = ((GameModeData) ref gameModeData).DebuffTimeMultiplier;
          }
        }
        player.AddBuff(buffID, (int) Math.Round((double) intendedTime / (double) debuffTimeMultiplier, (MidpointRounding) 0), quiet, false);
      }
      else
        player.AddBuff(buffID, intendedTime, quiet, false);
    }

    public static float ProjWorldDamage
    {
      get
      {
        GameModeData gameModeInfo1 = Main.GameModeInfo;
        if (((GameModeData) ref gameModeInfo1).IsJourneyMode)
          return CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs;
        GameModeData gameModeInfo2 = Main.GameModeInfo;
        return ((GameModeData) ref gameModeInfo2).EnemyDamageMultiplier;
      }
    }

    public static int ScaledProjectileDamage(
      int npcDamage,
      float modifier = 1f,
      int npcDamageCalculationsOffset = 2)
    {
      float projWorldDamage = FargoSoulsUtil.ProjWorldDamage;
      return (int) ((double) modifier * (double) npcDamage / 2.0 / (double) Math.Max((float) npcDamageCalculationsOffset, projWorldDamage));
    }

    public static void AllCritEquals(Player player, float crit)
    {
      player.GetCritChance(DamageClass.Generic) = crit;
      player.GetCritChance(DamageClass.Melee) = 0.0f;
      player.GetCritChance(DamageClass.Ranged) = 0.0f;
      player.GetCritChance(DamageClass.Magic) = 0.0f;
      player.GetCritChance(DamageClass.Summon) = 0.0f;
    }

    public static int HighestDamageTypeScaling(Player player, int dmg)
    {
      List<float> source = new List<float>();
      CollectionsMarshal.SetCount<float>(source, 4);
      Span<float> span = CollectionsMarshal.AsSpan<float>(source);
      int num1 = 0;
      span[num1] = player.ActualClassDamage(DamageClass.Melee);
      int num2 = num1 + 1;
      span[num2] = player.ActualClassDamage(DamageClass.Ranged);
      int num3 = num2 + 1;
      span[num3] = player.ActualClassDamage(DamageClass.Magic);
      int num4 = num3 + 1;
      span[num4] = player.ActualClassDamage(DamageClass.Summon);
      int num5 = num4 + 1;
      return (int) ((double) source.Max() * (double) dmg);
    }

    public static float HighestCritChance(Player player)
    {
      List<float> source = new List<float>();
      CollectionsMarshal.SetCount<float>(source, 4);
      Span<float> span = CollectionsMarshal.AsSpan<float>(source);
      int num1 = 0;
      span[num1] = player.ActualClassCrit(DamageClass.Melee);
      int num2 = num1 + 1;
      span[num2] = player.ActualClassCrit(DamageClass.Ranged);
      int num3 = num2 + 1;
      span[num3] = player.ActualClassCrit(DamageClass.Magic);
      int num4 = num3 + 1;
      span[num4] = player.ActualClassCrit(DamageClass.Summon);
      int num5 = num4 + 1;
      return source.Max();
    }

    public static Projectile[] XWay(
      int num,
      IEntitySource spawnSource,
      Vector2 pos,
      int type,
      float speed,
      int damage,
      float knockback)
    {
      Projectile[] projectileArray = new Projectile[num];
      double num1 = 2.0 * Math.PI / (double) num;
      for (int index = 0; index < num; ++index)
        projectileArray[index] = FargoSoulsUtil.NewProjectileDirectSafe(spawnSource, pos, Utils.RotatedBy(new Vector2(speed, speed), num1 * (double) index, new Vector2()), type, damage, knockback, Main.myPlayer);
      return projectileArray;
    }

    public static Projectile NewProjectileDirectSafe(
      IEntitySource spawnSource,
      Vector2 pos,
      Vector2 vel,
      int type,
      int damage,
      float knockback,
      int owner = 255,
      float ai0 = 0.0f,
      float ai1 = 0.0f)
    {
      int index = Projectile.NewProjectile(spawnSource, pos, vel, type, damage, knockback, owner, ai0, ai1, 0.0f);
      return index >= Main.maxProjectiles ? (Projectile) null : Main.projectile[index];
    }

    public static int GetProjectileByIdentity(
      int player,
      float projectileIdentity,
      params int[] projectileType)
    {
      return FargoSoulsUtil.GetProjectileByIdentity(player, (int) projectileIdentity, projectileType);
    }

    public static int GetProjectileByIdentity(
      int player,
      int projectileIdentity,
      params int[] projectileType)
    {
      for (int projectileByIdentity = 0; projectileByIdentity < Main.maxProjectiles; ++projectileByIdentity)
      {
        if (((Entity) Main.projectile[projectileByIdentity]).active && Main.projectile[projectileByIdentity].identity == projectileIdentity && Main.projectile[projectileByIdentity].owner == player && (projectileType.Length == 0 || ((IEnumerable<int>) projectileType).Contains<int>(Main.projectile[projectileByIdentity].type)))
          return projectileByIdentity;
      }
      return -1;
    }

    public static bool IsSummonDamage(
      Projectile projectile,
      bool includeMinionShot = true,
      bool includeWhips = true)
    {
      if (!includeWhips && ProjectileID.Sets.IsAWhip[projectile.type] || !includeMinionShot && (ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type]))
        return false;
      if (projectile.CountsAsClass(DamageClass.Summon) || projectile.minion || projectile.sentry || (double) projectile.minionSlots > 0.0 || ProjectileID.Sets.MinionSacrificable[projectile.type] || includeMinionShot && (ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type]))
        return true;
      return includeWhips && ProjectileID.Sets.IsAWhip[projectile.type];
    }

    public static bool CanDeleteProjectile(
      Projectile projectile,
      int deletionRank = 0,
      bool clearSummonProjs = false)
    {
      return ((Entity) projectile).active && projectile.damage > 0 && projectile.FargoSouls().DeletionImmuneRank <= deletionRank && (!projectile.friendly || ((Entity) projectile).whoAmI != Main.player[projectile.owner].heldProj && (!FargoSoulsUtil.IsSummonDamage(projectile, false) || clearSummonProjs));
    }

    public static Player PlayerExists(int whoAmI)
    {
      return whoAmI <= -1 || whoAmI >= (int) byte.MaxValue || !((Entity) Main.player[whoAmI]).active || Main.player[whoAmI].dead || Main.player[whoAmI].ghost ? (Player) null : Main.player[whoAmI];
    }

    public static Player PlayerExists(float whoAmI) => FargoSoulsUtil.PlayerExists((int) whoAmI);

    public static Projectile ProjectileExists(int whoAmI, params int[] types)
    {
      return whoAmI <= -1 || whoAmI >= Main.maxProjectiles || !((Entity) Main.projectile[whoAmI]).active || types.Length != 0 && !((IEnumerable<int>) types).Contains<int>(Main.projectile[whoAmI].type) ? (Projectile) null : Main.projectile[whoAmI];
    }

    public static Projectile ProjectileExists(float whoAmI, params int[] types)
    {
      return FargoSoulsUtil.ProjectileExists((int) whoAmI, types);
    }

    public static NPC NPCExists(int whoAmI, params int[] types)
    {
      return whoAmI <= -1 || whoAmI >= Main.maxNPCs || !((Entity) Main.npc[whoAmI]).active || types.Length != 0 && !((IEnumerable<int>) types).Contains<int>(Main.npc[whoAmI].type) ? (NPC) null : Main.npc[whoAmI];
    }

    public static NPC NPCExists(float whoAmI, params int[] types)
    {
      return FargoSoulsUtil.NPCExists((int) whoAmI, types);
    }

    public static bool OtherBossAlive(int npcID)
    {
      if (npcID > -1 && npcID < Main.maxNPCs)
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Main.npc[index].boss && index != npcID)
            return true;
        }
      }
      return false;
    }

    public static bool BossIsAlive(ref int bossID, int bossType)
    {
      if (bossID == -1)
        return false;
      if (((Entity) Main.npc[bossID]).active && Main.npc[bossID].type == bossType)
        return true;
      bossID = -1;
      return false;
    }

    public static void ClearFriendlyProjectiles(
      int deletionRank = 0,
      int bossNpc = -1,
      bool clearSummonProjs = false)
    {
      FargoSoulsUtil.ClearProjectiles(false, true, deletionRank, bossNpc, clearSummonProjs);
    }

    public static void ClearHostileProjectiles(int deletionRank = 0, int bossNpc = -1)
    {
      FargoSoulsUtil.ClearProjectiles(true, false, deletionRank, bossNpc);
    }

    public static void ClearAllProjectiles(int deletionRank = 0, int bossNpc = -1, bool clearSummonProjs = false)
    {
      FargoSoulsUtil.ClearProjectiles(true, true, deletionRank, bossNpc, clearSummonProjs);
    }

    private static void ClearProjectiles(
      bool clearHostile,
      bool clearFriendly,
      int deletionRank = 0,
      int bossNpc = -1,
      bool clearSummonProjs = false)
    {
      if (Main.netMode == 1)
        return;
      if (FargoSoulsUtil.OtherBossAlive(bossNpc))
        clearHostile = false;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        for (int index2 = 0; index2 < Main.maxProjectiles; ++index2)
        {
          Projectile projectile = Main.projectile[index2];
          if (((Entity) projectile).active && (projectile.hostile & clearHostile || projectile.friendly & clearFriendly) && FargoSoulsUtil.CanDeleteProjectile(projectile, deletionRank, clearSummonProjs))
            projectile.Kill();
        }
      }
    }

    public static void ReplaceItem(this Player player, Item itemToReplace, int itemIDtoReplaceWith)
    {
      bool flag = false;
      for (int index = 0; index < player.inventory.Length; ++index)
      {
        if (player.inventory[index] == itemToReplace)
        {
          Item obj = new Item(itemIDtoReplaceWith, itemToReplace.stack, itemToReplace.prefix);
          ((Entity) obj).active = true;
          obj.favorited = itemToReplace.favorited;
          player.inventory[index] = obj;
          ((Entity) itemToReplace).active = false;
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      Item.NewItem(player.GetSource_ItemUse(itemToReplace, (string) null), ((Entity) player).Center, itemIDtoReplaceWith, 1, false, itemToReplace.prefix, false, false);
    }

    public static bool NPCInAnyTiles(NPC npc, bool platforms)
    {
      bool flag = false;
      for (int index = 0; index < ((Entity) npc).width; index += 16)
      {
        for (float num = 0.0f; (double) num < (double) ((Entity) npc).height; num += 16f)
        {
          Tile tileSafely = Framing.GetTileSafely((int) ((double) ((Entity) npc).position.X + (double) index) / 16, (int) ((double) ((Entity) npc).position.Y + (double) num) / 16);
          if (((Tile) ref tileSafely).HasUnactuatedTile & platforms && (Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType] & platforms))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    public static void PrintAI(NPC npc)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 9);
      interpolatedStringHandler.AppendFormatted<int>(((Entity) npc).whoAmI);
      interpolatedStringHandler.AppendLiteral(" ai: ");
      interpolatedStringHandler.AppendFormatted<float>(npc.ai[0]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.ai[1]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.ai[2]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.ai[3]);
      interpolatedStringHandler.AppendLiteral(", local: ");
      interpolatedStringHandler.AppendFormatted<float>(npc.localAI[0]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.localAI[1]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.localAI[2]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(npc.localAI[3]);
      Main.NewText(interpolatedStringHandler.ToStringAndClear(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public static void PrintAI(Projectile projectile)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 6);
      interpolatedStringHandler.AppendFormatted<int>(((Entity) projectile).whoAmI);
      interpolatedStringHandler.AppendLiteral(" ai: ");
      interpolatedStringHandler.AppendFormatted<float>(projectile.ai[0]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(projectile.ai[1]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(projectile.ai[2]);
      interpolatedStringHandler.AppendLiteral(", local: ");
      interpolatedStringHandler.AppendFormatted<float>(projectile.localAI[0]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<float>(projectile.localAI[1]);
      Main.NewText(interpolatedStringHandler.ToStringAndClear(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public static void GrossVanillaDodgeDust(Entity entity)
    {
      for (int index1 = 0; index1 < 100; ++index1)
      {
        int index2 = Dust.NewDust(entity.position, entity.width, entity.height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].position.X += (float) Main.rand.Next(-20, 21);
        Main.dust[index2].position.Y += (float) Main.rand.Next(-20, 21);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.4f);
        Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(40) * 0.0099999997764825821);
        if (Utils.NextBool(Main.rand))
        {
          Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(40) * 0.0099999997764825821);
          Main.dust[index2].noGravity = true;
        }
      }
      int index3 = Gore.NewGore(entity.GetSource_FromThis((string) null), new Vector2(entity.Center.X - 24f, entity.Center.Y - 24f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index3].scale = 1.5f;
      Main.gore[index3].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
      Main.gore[index3].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
      Gore gore1 = Main.gore[index3];
      gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
      int index4 = Gore.NewGore(entity.GetSource_FromThis((string) null), new Vector2(entity.Center.X - 24f, entity.Center.Y - 24f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index4].scale = 1.5f;
      Main.gore[index4].velocity.X = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Main.gore[index4].velocity.Y = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Gore gore2 = Main.gore[index4];
      gore2.velocity = Vector2.op_Multiply(gore2.velocity, 0.4f);
      int index5 = Gore.NewGore(entity.GetSource_FromThis((string) null), new Vector2(entity.Center.X - 24f, entity.Center.Y - 24f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index5].scale = 1.5f;
      Main.gore[index5].velocity.X = (float) (-1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Main.gore[index5].velocity.Y = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Gore gore3 = Main.gore[index5];
      gore3.velocity = Vector2.op_Multiply(gore3.velocity, 0.4f);
      int index6 = Gore.NewGore(entity.GetSource_FromThis((string) null), new Vector2(entity.Center.X - 24f, entity.Center.Y - 24f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index6].scale = 1.5f;
      Main.gore[index6].velocity.X = (float) (1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Main.gore[index6].velocity.Y = (float) ((double) Main.rand.Next(-50, 51) * 0.0099999997764825821 - 1.5);
      Gore gore4 = Main.gore[index6];
      gore4.velocity = Vector2.op_Multiply(gore4.velocity, 0.4f);
      int index7 = Gore.NewGore(entity.GetSource_FromThis((string) null), new Vector2(entity.Center.X - 24f, entity.Center.Y - 24f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index7].scale = 1.5f;
      Main.gore[index7].velocity.X = (float) (-1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
      Main.gore[index7].velocity.Y = (float) ((double) Main.rand.Next(-50, 51) * 0.0099999997764825821 - 1.5);
      Gore gore5 = Main.gore[index7];
      gore5.velocity = Vector2.op_Multiply(gore5.velocity, 0.4f);
    }

    public static int FindClosestHostileNPC(
      Vector2 location,
      float detectionRange,
      bool lineCheck = false,
      bool prioritizeBoss = false)
    {
      NPC closestNpc = (NPC) null;
      if (prioritizeBoss)
        FindClosest(((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n.boss)));
      if (closestNpc == null)
        FindClosest((IEnumerable<NPC>) Main.npc);
      return closestNpc != null ? ((Entity) closestNpc).whoAmI : -1;

      void FindClosest(IEnumerable<NPC> npcs)
      {
        float num = detectionRange;
        foreach (NPC npc in npcs)
        {
          if (npc.CanBeChasedBy((object) null, false) && (double) ((Entity) npc).Distance(location) < (double) num && (!lineCheck || Collision.CanHitLine(location, 0, 0, ((Entity) npc).Center, 0, 0)))
          {
            num = ((Entity) npc).Distance(location);
            closestNpc = npc;
          }
        }
      }
    }

    public static int FindClosestHostileNPCPrioritizingMinionFocus(
      Projectile projectile,
      float detectionRange,
      bool lineCheck = false,
      Vector2 center = default (Vector2),
      bool prioritizeBoss = false)
    {
      if (Vector2.op_Equality(center, new Vector2()))
        center = ((Entity) projectile).Center;
      NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
      return minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy((object) null, false) && (double) ((Entity) minionAttackTargetNpc).Distance(center) < (double) detectionRange && (!lineCheck || Collision.CanHitLine(center, 0, 0, ((Entity) minionAttackTargetNpc).Center, 0, 0)) ? ((Entity) minionAttackTargetNpc).whoAmI : FargoSoulsUtil.FindClosestHostileNPC(center, detectionRange, lineCheck, prioritizeBoss);
    }

    public static void DustRing(
      Vector2 location,
      int max,
      int dust,
      float speed,
      Color color = default (Color),
      float scale = 1f,
      bool noLight = false)
    {
      for (int index1 = 0; index1 < max; ++index1)
      {
        Vector2 vector2 = Vector2.op_Multiply(speed, Utils.RotatedBy(Vector2.UnitY, 6.2831854820251465 / (double) max * (double) index1, new Vector2()));
        int index2 = Dust.NewDust(location, 0, 0, dust, 0.0f, 0.0f, 0, color, 1f);
        Main.dust[index2].noLight = noLight;
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2;
        Main.dust[index2].scale = scale;
      }
    }

    public static void PrintText(string text) => FargoSoulsUtil.PrintText(text, Color.White);

    public static void PrintLocalization(string localizationKey, Color color)
    {
      FargoSoulsUtil.PrintText(Language.GetTextValue(localizationKey), color);
    }

    public static void PrintLocalization(string localizationKey, int r, int g, int b)
    {
      FargoSoulsUtil.PrintLocalization(localizationKey, new Color(r, g, b));
    }

    public static void PrintLocalization(string localizationKey, Color color, params object[] args)
    {
      FargoSoulsUtil.PrintText(Language.GetTextValue(localizationKey, args), color);
    }

    public static void PrintText(string text, Color color)
    {
      if (Main.netMode == 0)
      {
        Main.NewText((object) text, new Color?(color));
      }
      else
      {
        if (Main.netMode != 2)
          return;
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
      }
    }

    public static void PrintText(string text, int r, int g, int b)
    {
      FargoSoulsUtil.PrintText(text, new Color(r, g, b));
    }

    public static Vector2 ClosestPointInHitbox(Rectangle hitboxOfTarget, Vector2 desiredLocation)
    {
      Vector2 vector2 = Vector2.op_Subtraction(desiredLocation, Utils.ToVector2(((Rectangle) ref hitboxOfTarget).Center));
      vector2.X = Math.Min(Math.Abs(vector2.X), (float) (hitboxOfTarget.Width / 2)) * (float) Math.Sign(vector2.X);
      vector2.Y = Math.Min(Math.Abs(vector2.Y), (float) (hitboxOfTarget.Height / 2)) * (float) Math.Sign(vector2.Y);
      return Vector2.op_Addition(Utils.ToVector2(((Rectangle) ref hitboxOfTarget).Center), vector2);
    }

    public static Vector2 ClosestPointInHitbox(Entity entity, Vector2 desiredLocation)
    {
      return FargoSoulsUtil.ClosestPointInHitbox(entity.Hitbox, desiredLocation);
    }

    public static float RotationDifference(Vector2 from, Vector2 to)
    {
      return (float) Math.Atan2((double) to.Y * (double) from.X - (double) to.X * (double) from.Y, (double) from.X * (double) to.X + (double) from.Y * (double) to.Y);
    }

    public static Vector2 PredictiveAim(
      Vector2 startingPosition,
      Vector2 targetPosition,
      Vector2 targetVelocity,
      float shootSpeed,
      int iterations = 4)
    {
      float num1 = 0.0f;
      Vector2 vector2 = targetPosition;
      for (int index = 0; index < iterations; ++index)
      {
        float num2 = Vector2.Distance(startingPosition, vector2) / shootSpeed;
        vector2 = Vector2.op_Addition(vector2, Vector2.op_Multiply(targetVelocity, num2 - num1));
        num1 = num2;
      }
      return Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(vector2, startingPosition), Vector2.UnitY), shootSpeed);
    }

    public static void HeartDust(
      Vector2 position,
      float rotationOffset = 1.57079637f,
      Vector2 addedVel = default (Vector2),
      float spreadModifier = 1f,
      float scaleModifier = 1f)
    {
      for (float num = 0.0f; (double) num < 6.2831854820251465; num += MathHelper.ToRadians(6f))
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(16f * (float) Math.Pow(Math.Sin((double) num), 3.0), (float) (13.0 * Math.Cos((double) num) - 5.0 * Math.Cos(2.0 * (double) num) - 2.0 * Math.Cos(3.0 * (double) num)) - (float) Math.Cos(4.0 * (double) num));
        vector2.Y *= -1f;
        vector2 = Utils.RotatedBy(vector2, (double) rotationOffset - 1.5707963705062866, new Vector2());
        int index = Dust.NewDust(position, 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(vector2, 0.25f), spreadModifier), addedVel);
        Main.dust[index].scale = 2f * scaleModifier;
        Main.dust[index].noGravity = true;
      }
    }

    public static int NewSummonProjectile(
      IEntitySource source,
      Vector2 spawn,
      Vector2 velocity,
      int type,
      int rawBaseDamage,
      float knockback,
      int owner = 255,
      float ai0 = 0.0f,
      float ai1 = 0.0f)
    {
      int index = Projectile.NewProjectile(source, spawn, velocity, type, rawBaseDamage, knockback, owner, ai0, ai1, 0.0f);
      if (index != Main.maxProjectiles)
      {
        Main.projectile[index].originalDamage = rawBaseDamage;
        Main.projectile[index].ContinuouslyUpdateDamageStats = true;
      }
      return index;
    }

    public static int NewNPCEasy(
      IEntitySource source,
      Vector2 spawnPos,
      int type,
      int start = 0,
      float ai0 = 0.0f,
      float ai1 = 0.0f,
      float ai2 = 0.0f,
      float ai3 = 0.0f,
      int target = 255,
      Vector2 velocity = default (Vector2))
    {
      if (Main.netMode == 1)
        return Main.maxNPCs;
      int index = NPC.NewNPC(source, (int) spawnPos.X, (int) spawnPos.Y, type, start, ai0, ai1, ai2, ai3, target);
      if (index != Main.maxNPCs)
      {
        if (Vector2.op_Inequality(velocity, new Vector2()))
          ((Entity) Main.npc[index]).velocity = velocity;
        if (Main.netMode == 2)
          NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      return index;
    }

    public static void AuraDust(
      Entity entity,
      float distance,
      int dustid,
      Color color = default (Color),
      bool reverse = false,
      float dustScaleModifier = 1f)
    {
      int num1 = (int) ((double) distance / 500.0 * 20.0);
      if (num1 < 10)
        num1 = 10;
      if (num1 > 40)
        num1 = 40;
      float num2 = distance / 500f;
      if ((double) num2 < 0.75)
        num2 = 0.75f;
      if ((double) num2 > 2.0)
        num2 = 2f;
      for (int index = 0; index < num1; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(entity.Center, Utils.NextVector2CircularEdge(Main.rand, distance, distance));
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
        if ((double) Math.Abs(vector2_2.X) <= (double) Main.screenWidth * 0.60000002384185791 && (double) Math.Abs(vector2_2.Y) <= (double) Main.screenHeight * 0.60000002384185791)
        {
          Dust dust1 = Main.dust[Dust.NewDust(vector2_1, 0, 0, dustid, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.scale = num2 * dustScaleModifier;
          dust1.velocity = entity.velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(entity.Center, dust1.position)), Utils.NextFloat(Main.rand, 5f)), reverse ? -1f : 1f));
            Dust dust3 = dust1;
            dust3.position = Vector2.op_Addition(dust3.position, Vector2.op_Multiply(dust1.velocity, 5f));
          }
          dust1.noGravity = true;
          if (Color.op_Inequality(color, new Color()))
            dust1.color = color;
        }
      }
    }

    public static void AuraParticles(
      Entity entity,
      float distance,
      Color color = default (Color),
      bool reverse = false,
      float scaleModifier = 1f,
      int particleType = 0)
    {
      int num1 = (int) ((double) distance / 500.0 * 20.0);
      if (num1 < 10)
        num1 = 10;
      if (num1 > 40)
        num1 = 40;
      float num2 = distance / 500f;
      if ((double) num2 < 0.75)
        num2 = 0.75f;
      if ((double) num2 > 2.0)
        num2 = 2f;
      for (int index = 0; index < num1; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(entity.Center, Utils.NextVector2CircularEdge(Main.rand, distance, distance));
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
        if ((double) Math.Abs(vector2_2.X) <= (double) Main.screenWidth * 0.60000002384185791 && (double) Math.Abs(vector2_2.Y) <= (double) Main.screenHeight * 0.60000002384185791)
        {
          Particle particle1 = particleType != 1 ? (Particle) new ExpandingBloomParticle(vector2_1, entity.velocity, Color.op_Inequality(color, new Color()) ? color : Color.White, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.One, num2), scaleModifier), Vector2.op_Multiply(Vector2.One, 0.0f), 30) : (Particle) new SmallSparkle(vector2_1, entity.velocity, Color.op_Inequality(color, new Color()) ? color : Color.White, num2 * scaleModifier, 30, Utils.NextFloat(Main.rand, 6.28318548f), Utils.NextFloat(Main.rand, -1f * (float) Math.PI / 32f, (float) Math.PI / 32f));
          particle1.Spawn();
          if (Utils.NextBool(Main.rand, 3))
          {
            Particle particle2 = particle1;
            particle2.Velocity = Vector2.op_Addition(particle2.Velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(entity.Center, particle1.Position)), Utils.NextFloat(Main.rand, 5f)), reverse ? -1f : 1f));
            Particle particle3 = particle1;
            particle3.Position = Vector2.op_Addition(particle3.Position, Vector2.op_Multiply(particle1.Velocity, 5f));
          }
        }
      }
    }

    public static bool OnSpawnEnchCanAffectProjectile(Projectile projectile, bool allowMinions)
    {
      return (allowMinions || !projectile.minion && !projectile.sentry && (double) projectile.minionSlots <= 0.0) && projectile.friendly && projectile.damage > 0 && !projectile.hostile && !projectile.npcProj && !projectile.trap;
    }

    public static void SpawnBossNetcoded(Player player, int bossType, bool obeyLocalPlayerCheck = true)
    {
      if (((Entity) player).whoAmI != Main.myPlayer && obeyLocalPlayerCheck)
        return;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      if (FargoSoulsUtil.HostCheck)
        NPC.SpawnOnPlayer(((Entity) player).whoAmI, bossType);
      else
        NetMessage.SendData(61, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, (float) bossType, 0.0f, 0.0f, 0, 0, 0);
    }

    public static bool IsProjSourceItemUseReal(Projectile proj, IEntitySource source)
    {
      return source is EntitySource_ItemUse entitySourceItemUse && entitySourceItemUse.Item.type == Main.player[proj.owner].HeldItem.type;
    }

    public static bool AprilFools => DateTime.Today.Month == 4 && DateTime.Today.Day <= 7;

    public static string TryAprilFoolsTexture => !FargoSoulsUtil.AprilFools ? "" : "_April";

    public static void ScreenshakeRumble(float strength)
    {
      if ((double) ScreenShakeSystem.OverallShakeIntensity >= (double) strength)
        return;
      ScreenShakeSystem.SetUniversalRumble(strength, 6.28318548f, new Vector2?(), 0.2f);
    }

    public static bool LockEarlyBirdDrop(NPCLoot npcLoot, IItemDropRule rule)
    {
      IItemDropRule iitemDropRule = (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) new EModeEarlyBirdLockDropCondition());
      Chains.OnSuccess(iitemDropRule, rule, false);
      ((NPCLoot) ref npcLoot).Add(iitemDropRule);
      return true;
    }

    public static void AddEarlyBirdDrop(NPCLoot npcLoot, IItemDropRule rule)
    {
      IItemDropRule iitemDropRule = (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) new EModeEarlyBirdRewardDropCondition());
      Chains.OnSuccess(iitemDropRule, rule, false);
      ((NPCLoot) ref npcLoot).Add(iitemDropRule);
    }

    public static void EModeDrop(NPCLoot npcLoot, IItemDropRule rule)
    {
      IItemDropRule iitemDropRule = (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) new EModeDropCondition());
      Chains.OnSuccess(iitemDropRule, rule, false);
      ((NPCLoot) ref npcLoot).Add(iitemDropRule);
    }

    public static IItemDropRule BossBagDropCustom(int itemType, int amount = 1)
    {
      return (IItemDropRule) new DropLocalPerClientAndResetsNPCMoneyTo0(itemType, 1, amount, amount, (IItemDropRuleCondition) null);
    }

    public static void DrawTexture(
      object sb,
      Texture2D texture,
      int shader,
      Entity codable,
      Color? overrideColor = null,
      bool drawCentered = false,
      Vector2 overrideOrigin = default (Vector2))
    {
      FargoSoulsUtil.DrawTexture(sb, texture, shader, codable, 1, overrideColor, drawCentered, overrideOrigin);
    }

    public static void DrawTexture(
      object sb,
      Texture2D texture,
      int shader,
      Entity codable,
      int framecountX,
      Color? overrideColor = null,
      bool drawCentered = false,
      Vector2 overrideOrigin = default (Vector2))
    {
      Color color1;
      if (!overrideColor.HasValue)
      {
        switch (codable)
        {
          case Item obj1:
            color1 = obj1.GetAlpha(FargoSoulsUtil.GetLightColor(codable.Center));
            break;
          case NPC npc1:
            color1 = FargoSoulsUtil.GetNPCColor(npc1, new Vector2?(codable.Center), false);
            break;
          case Projectile projectile:
            color1 = projectile.GetAlpha(FargoSoulsUtil.GetLightColor(codable.Center));
            break;
          default:
            color1 = FargoSoulsUtil.GetLightColor(codable.Center);
            break;
        }
      }
      else
        color1 = overrideColor.Value;
      Color color2 = color1;
      int num1;
      switch (codable)
      {
        case Item _:
          num1 = 1;
          break;
        case NPC npc2:
          num1 = Main.npcFrameCount[npc2.type];
          break;
        default:
          num1 = 1;
          break;
      }
      int framecount = num1;
      Rectangle frame = codable is NPC npc3 ? npc3.frame : new Rectangle(0, 0, texture.Width, texture.Height);
      double scale1;
      switch (codable)
      {
        case Item obj2:
          scale1 = (double) obj2.scale;
          break;
        case NPC npc4:
          scale1 = (double) npc4.scale;
          break;
        default:
          scale1 = (double) ((Projectile) codable).scale;
          break;
      }
      float scale2 = (float) scale1;
      double num2;
      switch (codable)
      {
        case Item _:
          num2 = 0.0;
          break;
        case NPC npc5:
          num2 = (double) npc5.rotation;
          break;
        default:
          num2 = (double) ((Projectile) codable).rotation;
          break;
      }
      float rotation = (float) num2;
      int num3;
      switch (codable)
      {
        case Item _:
          num3 = 1;
          break;
        case NPC npc6:
          num3 = npc6.spriteDirection;
          break;
        default:
          num3 = ((Projectile) codable).spriteDirection;
          break;
      }
      int direction = num3;
      float num4 = codable is NPC npc7 ? npc7.gfxOffY : 0.0f;
      FargoSoulsUtil.DrawTexture(sb, texture, shader, Vector2.op_Addition(codable.position, new Vector2(0.0f, num4)), codable.width, codable.height, scale2, rotation, direction, framecount, framecountX, frame, new Color?(color2), drawCentered, overrideOrigin);
    }

    public static void DrawTexture(
      object sb,
      Texture2D texture,
      int shader,
      Vector2 position,
      int width,
      int height,
      float scale,
      float rotation,
      int direction,
      int framecount,
      Rectangle frame,
      Color? overrideColor = null,
      bool drawCentered = false,
      Vector2 overrideOrigin = default (Vector2))
    {
      FargoSoulsUtil.DrawTexture(sb, texture, shader, position, width, height, scale, rotation, direction, framecount, 1, frame, overrideColor, drawCentered, overrideOrigin);
    }

    public static void DrawTexture(
      object sb,
      Texture2D texture,
      int shader,
      Vector2 position,
      int width,
      int height,
      float scale,
      float rotation,
      int direction,
      int framecount,
      int framecountX,
      Rectangle frame,
      Color? overrideColor = null,
      bool drawCentered = false,
      Vector2 overrideOrigin = default (Vector2))
    {
      Vector2 origin = Vector2.op_Inequality(overrideOrigin, new Vector2()) ? overrideOrigin : new Vector2((float) (frame.Width / framecountX / 2), (float) (texture.Height / framecount / 2));
      Color color = overrideColor.HasValue ? overrideColor.Value : FargoSoulsUtil.GetLightColor(Vector2.op_Addition(position, new Vector2((float) width * 0.5f, (float) height * 0.5f)));
      switch (sb)
      {
        case List<DrawData> drawDataList:
          DrawData drawData1;
          // ISSUE: explicit constructor call
          ((DrawData) ref drawData1).\u002Ector(texture, FargoSoulsUtil.GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), color, rotation, origin, scale, direction == 1 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
          drawData1.shader = shader;
          DrawData drawData2 = drawData1;
          drawDataList.Add(drawData2);
          break;
        case SpriteBatch spriteBatch:
          int num = shader > 0 ? 1 : 0;
          if (num != 0)
          {
            spriteBatch.End();
            spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend);
            GameShaders.Armor.ApplySecondary(shader, (Entity) Main.player[Main.myPlayer], new DrawData?());
          }
          spriteBatch.Draw(texture, FargoSoulsUtil.GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), color, rotation, origin, scale, direction == 1 ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
          if (num == 0)
            break;
          spriteBatch.End();
          spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend);
          break;
      }
    }

    public static Color GetNPCColor(
      NPC npc,
      Vector2? position = null,
      bool effects = true,
      float shadowOverride = 0.0f)
    {
      return npc.GetAlpha(FargoSoulsUtil.BuffEffects((Entity) npc, FargoSoulsUtil.GetLightColor(position.HasValue ? position.Value : ((Entity) npc).Center), (double) shadowOverride != 0.0 ? shadowOverride : 0.0f, effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, venom: npc.venom, midas: npc.midas, ichor: npc.ichor, onFrostBurn: npc.onFrostBurn, dripping: npc.dripping, drippingSlime: npc.drippingSlime, loveStruck: npc.loveStruck, stinky: npc.stinky));
    }

    public static Color GetLightColor(Vector2 position)
    {
      return Lighting.GetColor((int) ((double) position.X / 16.0), (int) ((double) position.Y / 16.0));
    }

    public static Vector2 GetDrawPosition(
      Vector2 position,
      Vector2 origin,
      int width,
      int height,
      int texWidth,
      int texHeight,
      int framecount,
      float scale,
      bool drawCentered = false)
    {
      return FargoSoulsUtil.GetDrawPosition(position, origin, width, height, texWidth, texHeight, framecount, 1, scale, drawCentered);
    }

    public static Vector2 GetDrawPosition(
      Vector2 position,
      Vector2 origin,
      int width,
      int height,
      int texWidth,
      int texHeight,
      int framecount,
      int framecountX,
      float scale,
      bool drawCentered = false)
    {
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) (int) Main.screenPosition.X, (float) (int) Main.screenPosition.Y);
      if (!drawCentered)
        return Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Subtraction(position, vector2_1), new Vector2((float) (width / 2), (float) height)), Vector2.op_Multiply(new Vector2((float) (texWidth / framecountX / 2), (float) (texHeight / framecount)), scale)), Vector2.op_Multiply(origin, scale)), new Vector2(0.0f, 5f));
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector((float) (texWidth / framecountX / 2), (float) (texHeight / framecount / 2));
      return Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(position, new Vector2((float) (width / 2), (float) (height / 2))), Vector2.op_Multiply(vector2_2, scale)), Vector2.op_Multiply(origin, scale)), vector2_1);
    }

    public static Color BuffEffects(
      Entity codable,
      Color lightColor,
      float shadow = 0.0f,
      bool effects = true,
      bool poisoned = false,
      bool onFire = false,
      bool onFire2 = false,
      bool hunter = false,
      bool noItems = false,
      bool blind = false,
      bool bleed = false,
      bool venom = false,
      bool midas = false,
      bool ichor = false,
      bool onFrostBurn = false,
      bool burned = false,
      bool honey = false,
      bool dripping = false,
      bool drippingSlime = false,
      bool loveStruck = false,
      bool stinky = false)
    {
      float num1 = 1f;
      float num2 = 1f;
      float num3 = 1f;
      float num4 = 1f;
      if (effects & honey && Utils.NextBool(Main.rand, 30))
      {
        int index = Dust.NewDust(codable.position, codable.width, codable.height, 152, 0.0f, 0.0f, 150, new Color(), 1f);
        Main.dust[index].velocity.Y = 0.3f;
        Main.dust[index].velocity.X *= 0.1f;
        Main.dust[index].scale += (float) Main.rand.Next(3, 4) * 0.1f;
        Main.dust[index].alpha = 100;
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Addition(dust.velocity, Vector2.op_Multiply(codable.velocity, 0.1f));
      }
      if (poisoned)
      {
        if (effects && Utils.NextBool(Main.rand, 30))
        {
          int index = Dust.NewDust(codable.position, codable.width, codable.height, 46, 0.0f, 0.0f, 120, new Color(), 0.2f);
          Main.dust[index].noGravity = true;
          Main.dust[index].fadeIn = 1.9f;
        }
        num1 *= 0.65f;
        num3 *= 0.75f;
      }
      if (venom)
      {
        if (effects && Utils.NextBool(Main.rand, 10))
        {
          int index = Dust.NewDust(codable.position, codable.width, codable.height, 171, 0.0f, 0.0f, 100, new Color(), 0.5f);
          Main.dust[index].noGravity = true;
          Main.dust[index].fadeIn = 1.5f;
        }
        num2 *= 0.45f;
        num1 *= 0.75f;
      }
      if (midas)
      {
        num3 *= 0.3f;
        num1 *= 0.85f;
      }
      if (ichor)
      {
        if (codable is NPC)
        {
          // ISSUE: explicit constructor call
          ((Color) ref lightColor).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
        }
        else
          num3 = 0.0f;
      }
      if (burned)
      {
        if (effects)
        {
          int index = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, new Color(), 2f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
          Main.dust[index].velocity.Y -= 0.75f;
        }
        if (codable is Player)
        {
          num1 = 1f;
          num3 *= 0.6f;
          num2 *= 0.7f;
        }
      }
      if (onFrostBurn)
      {
        if (effects)
        {
          if (Main.rand.Next(4) < 3)
          {
            int index = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 135, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, new Color(), 3.5f);
            Main.dust[index].noGravity = true;
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
            Main.dust[index].velocity.Y -= 0.5f;
            if (Utils.NextBool(Main.rand, 4))
            {
              Main.dust[index].noGravity = false;
              Main.dust[index].scale *= 0.5f;
            }
          }
          Lighting.AddLight((int) ((double) codable.position.X / 16.0), (int) ((double) codable.position.Y / 16.0 + 1.0), 0.1f, 0.6f, 1f);
        }
        if (codable is Player)
        {
          num1 *= 0.5f;
          num2 *= 0.7f;
        }
      }
      if (onFire)
      {
        if (effects)
        {
          if (!Utils.NextBool(Main.rand, 4))
          {
            int index = Dust.NewDust(Vector2.op_Subtraction(codable.position, new Vector2(2f, 2f)), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, new Color(), 3.5f);
            Main.dust[index].noGravity = true;
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
            Main.dust[index].velocity.Y -= 0.5f;
            if (Utils.NextBool(Main.rand, 4))
            {
              Main.dust[index].noGravity = false;
              Main.dust[index].scale *= 0.5f;
            }
          }
          Lighting.AddLight((int) ((double) codable.position.X / 16.0), (int) ((double) codable.position.Y / 16.0 + 1.0), 1f, 0.3f, 0.1f);
        }
        if (codable is Player)
        {
          num3 *= 0.6f;
          num2 *= 0.7f;
        }
      }
      if (dripping && (double) shadow == 0.0 && !Utils.NextBool(Main.rand, 4))
      {
        Vector2 position = codable.position;
        position.X -= 2f;
        position.Y -= 2f;
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(position, codable.width + 4, codable.height + 2, 211, 0.0f, 0.0f, 50, new Color(), 0.8f);
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          Main.dust[index].noLight = true;
          Dust dust1 = Main.dust[index];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.2f);
          Main.dust[index].velocity.Y += 0.2f;
          Dust dust2 = Main.dust[index];
          dust2.velocity = Vector2.op_Addition(dust2.velocity, codable.velocity);
        }
        else
        {
          int index = Dust.NewDust(position, codable.width + 8, codable.height + 8, 211, 0.0f, 0.0f, 50, new Color(), 1.1f);
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          Main.dust[index].noLight = true;
          Main.dust[index].noGravity = true;
          Dust dust3 = Main.dust[index];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 0.2f);
          ++Main.dust[index].velocity.Y;
          Dust dust4 = Main.dust[index];
          dust4.velocity = Vector2.op_Addition(dust4.velocity, codable.velocity);
        }
      }
      if (drippingSlime && (double) shadow == 0.0)
      {
        int num5 = 175;
        Color color;
        // ISSUE: explicit constructor call
        ((Color) ref color).\u002Ector(0, 80, (int) byte.MaxValue, 100);
        if (!Utils.NextBool(Main.rand, 4) && Utils.NextBool(Main.rand))
        {
          Vector2 position = codable.position;
          position.X -= 2f;
          position.Y -= 2f;
          int index = Dust.NewDust(position, codable.width + 4, codable.height + 2, 4, 0.0f, 0.0f, num5, color, 1.4f);
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          if (Utils.NextBool(Main.rand))
            Main.dust[index].alpha += 25;
          Main.dust[index].noLight = true;
          Dust dust5 = Main.dust[index];
          dust5.velocity = Vector2.op_Multiply(dust5.velocity, 0.2f);
          Main.dust[index].velocity.Y += 0.2f;
          Dust dust6 = Main.dust[index];
          dust6.velocity = Vector2.op_Addition(dust6.velocity, codable.velocity);
        }
        num1 *= 0.8f;
        num2 *= 0.8f;
      }
      if (onFire2)
      {
        if (effects)
        {
          if (!Utils.NextBool(Main.rand, 4))
          {
            int index = Dust.NewDust(Vector2.op_Subtraction(codable.position, new Vector2(2f, 2f)), codable.width + 4, codable.height + 4, 75, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, new Color(), 3.5f);
            Main.dust[index].noGravity = true;
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
            Main.dust[index].velocity.Y -= 0.5f;
            if (Utils.NextBool(Main.rand, 4))
            {
              Main.dust[index].noGravity = false;
              Main.dust[index].scale *= 0.5f;
            }
          }
          Lighting.AddLight((int) ((double) codable.position.X / 16.0), (int) ((double) codable.position.Y / 16.0 + 1.0), 1f, 0.3f, 0.1f);
        }
        if (codable is Player)
        {
          num3 *= 0.6f;
          num2 *= 0.7f;
        }
      }
      if (noItems)
      {
        num1 *= 0.65f;
        num2 *= 0.8f;
      }
      if (blind)
      {
        num1 *= 0.7f;
        num2 *= 0.65f;
      }
      if (bleed)
      {
        int num6;
        switch (codable)
        {
          case Player player:
            num6 = player.dead ? 1 : 0;
            break;
          case NPC npc:
            num6 = npc.life <= 0 ? 1 : 0;
            break;
          default:
            num6 = 0;
            break;
        }
        bool flag = num6 != 0;
        if (effects && !flag && Utils.NextBool(Main.rand, 30))
        {
          int index = Dust.NewDust(codable.position, codable.width, codable.height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index].velocity.Y += 0.5f;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.25f);
        }
        num2 *= 0.9f;
        num3 *= 0.9f;
      }
      if (loveStruck & effects && (double) shadow == 0.0 && ((Game) Main.instance).IsActive && !Main.gamePaused && Utils.NextBool(Main.rand, 5))
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
        ((Vector2) ref vector2).Normalize();
        vector2.X *= 0.66f;
        int index = Gore.NewGore(codable.GetSource_FromThis((string) null), Vector2.op_Addition(codable.position, new Vector2((float) Main.rand.Next(codable.width + 1), (float) Main.rand.Next(codable.height + 1))), Vector2.op_Multiply(Vector2.op_Multiply(vector2, (float) Main.rand.Next(3, 6)), 0.33f), 331, (float) Main.rand.Next(40, 121) * 0.01f);
        Main.gore[index].sticky = false;
        Gore gore = Main.gore[index];
        gore.velocity = Vector2.op_Multiply(gore.velocity, 0.4f);
        Main.gore[index].velocity.Y -= 0.6f;
      }
      if (stinky && (double) shadow == 0.0)
      {
        num1 *= 0.7f;
        num3 *= 0.55f;
        if (effects && Utils.NextBool(Main.rand, 5) && ((Game) Main.instance).IsActive && !Main.gamePaused)
        {
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
          ((Vector2) ref vector2_1).Normalize();
          vector2_1.X *= 0.66f;
          vector2_1.Y = Math.Abs(vector2_1.Y);
          Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(vector2_1, (float) Main.rand.Next(3, 5)), 0.25f);
          int index = Dust.NewDust(codable.position, codable.width, codable.height, 188, vector2_2.X, vector2_2.Y * 0.5f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index].velocity.Y -= 0.5f;
        }
      }
      ((Color) ref lightColor).R = (byte) ((double) ((Color) ref lightColor).R * (double) num1);
      ((Color) ref lightColor).G = (byte) ((double) ((Color) ref lightColor).G * (double) num2);
      ((Color) ref lightColor).B = (byte) ((double) ((Color) ref lightColor).B * (double) num3);
      ((Color) ref lightColor).A = (byte) ((double) ((Color) ref lightColor).A * (double) num4);
      if (hunter && (!(codable is NPC) || ((NPC) codable).lifeMax > 1))
      {
        if (effects && !Main.gamePaused && ((Game) Main.instance).IsActive && Utils.NextBool(Main.rand, 50))
        {
          int index = Dust.NewDust(codable.position, codable.width, codable.height, 15, 0.0f, 0.0f, 150, new Color(), 0.8f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index].noLight = true;
        }
        byte num7 = 50;
        byte num8 = byte.MaxValue;
        byte num9 = 50;
        if (codable is NPC npc && !npc.friendly && npc.catchItem <= 0 && (npc.damage != 0 || npc.lifeMax != 5))
        {
          num7 = byte.MaxValue;
          num8 = (byte) 50;
        }
        if (!(codable is NPC) && ((Color) ref lightColor).R < (byte) 150)
          ((Color) ref lightColor).A = Main.mouseTextColor;
        if ((int) ((Color) ref lightColor).R < (int) num7)
          ((Color) ref lightColor).R = num7;
        if ((int) ((Color) ref lightColor).G < (int) num8)
          ((Color) ref lightColor).G = num8;
        if ((int) ((Color) ref lightColor).B < (int) num9)
          ((Color) ref lightColor).B = num9;
      }
      return lightColor;
    }

    public static float SineInOut(float value)
    {
      return (float) ((0.0 - ((double) MathF.Cos(value * 3.14159274f) - 1.0)) / 2.0);
    }
  }
}
